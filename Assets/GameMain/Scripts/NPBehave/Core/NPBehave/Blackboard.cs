using UnityEngine;
using System.Collections.Generic;
using Suture;
using UnityGameFramework.Runtime;

namespace NPBehave
{
    public class Blackboard
    {
        public enum Type
        {
            ADD,
            REMOVE,
            CHANGE
        }

        private struct Notification
        {
            public string key;
            public Type type;
            public ANP_BBValue value;

            public Notification(string key, Type type, ANP_BBValue value)
            {
                this.key = key;
                this.type = type;
                this.value = value;
            }
        }

        private Clock m_Clock;
        private Dictionary<string, ANP_BBValue> m_Data = new Dictionary<string, ANP_BBValue>();

        private Dictionary<string, List<System.Action<Type, ANP_BBValue>>> m_Observers =
            new Dictionary<string, List<System.Action<Type, ANP_BBValue>>>();

        private bool m_IsNotifiyng = false;

        private Dictionary<string, List<System.Action<Type, ANP_BBValue>>> m_AddObservers =
            new Dictionary<string, List<System.Action<Type, ANP_BBValue>>>();

        private Dictionary<string, List<System.Action<Type, ANP_BBValue>>> m_RemoveObservers =
            new Dictionary<string, List<System.Action<Type, ANP_BBValue>>>();

        private List<Notification> m_Notifications = new List<Notification>();
        private List<Notification> m_NotificationsDispatch = new List<Notification>();
        private Blackboard m_ParentBlackboard;
        private HashSet<Blackboard> m_Children = new HashSet<Blackboard>();

        private System.Action NotifiyObserversActionCache;
        private long TimerId;

        public Blackboard(Blackboard mParent, Clock mClock)
        {
            this.m_Clock = mClock;
            this.m_ParentBlackboard = mParent;
            NotifiyObserversActionCache = NotifiyObservers;
        }

        public Blackboard(Clock mClock)
        {
            this.m_ParentBlackboard = null;
            this.m_Clock = mClock;
            NotifiyObserversActionCache = NotifiyObservers;
        }

        public void Enable()
        {
            if (this.m_ParentBlackboard != null)
            {
                this.m_ParentBlackboard.m_Children.Add(this);
            }
        }

        /// <summary>
        /// 获取所有键值对,注意，请不要通过这个API获取的data进行黑板数据的修改，否则不会正常触发相关黑板监控行为
        /// </summary>
        /// <returns></returns>
        public Dictionary<string, ANP_BBValue> GetDatas()
        {
            return this.m_Data;
        }

        public void Disable()
        {
            if (this.m_ParentBlackboard != null)
            {
                this.m_ParentBlackboard.m_Children.Remove(this);
            }

            if (this.m_Clock != null)
            {
                this.m_Clock.RemoveTimer(TimerId);
            }
        }

        public object this[string key]
        {
            get { return Get(key); }
            set { Set(key, value); }
        }

        #region 原生Set

        // public void Set(string key)
        // {
        //     if (!Isset(key))
        //     {
        //         Set(key, null);
        //     }
        // }

        // public void Set(string key, object value)
        // {
        //     if (this.m_ParentBlackboard != null && this.m_ParentBlackboard.Isset(key))
        //     {
        //         this.m_ParentBlackboard.Set(key, value);
        //     }
        //     else
        //     {
        //         if (!this.m_Data.ContainsKey(key))
        //         {
        //             this.m_Data[key] = value;
        //             this.m_Notifications.Add(new Notification(key, Type.ADD, value));
        //             this.clock.AddTimer(0f, 0, NotifiyObservers);
        //         }
        //         else
        //         {
        //             if ((this.m_Data[key] == null && value != null) ||
        //                 (this.m_Data[key] != null && !this.m_Data[key].Equals(value)))
        //             {
        //                 this.m_Data[key] = value;
        //                 this.m_Notifications.Add(new Notification(key, Type.CHANGE, value));
        //                 this.clock.AddTimer(0f, 0, NotifiyObservers);
        //             }
        //         }
        //     }
        // }

        #endregion


        /// <summary>
        /// 设置黑板值，
        /// 注意此处的T需要是被已注册的黑板类型包裹的，例如这里的T为int，那么必须存在一个例如NP_BBValue_Int
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        /// <typeparam name="T"></typeparam>
        /// <param name="addIfNotExit">如果不存在这个键值的话，就添加一个</param>
        public void Set<T>(string key, T value, bool isLocalPlayer = true, bool addIfNotExit = false)
        {
            if (this.m_ParentBlackboard != null && this.m_ParentBlackboard.Isset(key))
            {
                this.m_ParentBlackboard.Set(key, value);
            }
            else
            {
                if (!this.m_Data.ContainsKey(key))
                {
                    if (!addIfNotExit)
                        return;
                    ANP_BBValue newBBValue = NP_BBValueHelper.AutoCreateNPBBValueFromTValue(value);
                    this.m_Data.Add(key, newBBValue);
                    this.m_Notifications.Add(new Notification(key, Type.ADD, newBBValue));
                    this.m_Clock.AddTimer((uint)(isLocalPlayer ? 1 : 0), NotifiyObserversActionCache);
                }
                else
                {
                    NP_BBValueBase<T> targetBBValue = this.m_Data[key] as NP_BBValueBase<T>;
                    if ((targetBBValue == null && value != null) ||
                        (targetBBValue != null &&
                         (targetBBValue.GetValue() == null || !targetBBValue.GetValue().Equals(value))))
                    {
                        targetBBValue.SetValueFrom(value);
                        this.m_Notifications.Add(new Notification(key, Type.CHANGE, targetBBValue));
                        TimerId = this.m_Clock.AddTimer((uint)(isLocalPlayer ? 1 : 0), NotifiyObserversActionCache);
                    }
                }
            }
        }

        public void Unset(string key)
        {
            if (this.m_Data.ContainsKey(key))
            {
                this.m_Data.Remove(key);
                this.m_Notifications.Add(new Notification(key, Type.REMOVE, null));
                this.m_Clock.AddTimer(0f, 0, NotifiyObservers);
            }
        }

        [System.Obsolete("Use Get<T> instead")]
        public bool GetBool(string key)
        {
            return Get<bool>(key);
        }

        // [System.Obsolete(
        //     "Use Get<T> instead - WARNING: return value for non-existant key will be 0.0f instead of float.NaN")]
        // public float GetFloat(string key)
        // {
        //     object result = Get(key);
        //     if (result == null)
        //     {
        //         return float.NaN;
        //     }
        //
        //     return (float)Get(key);
        // }

        [System.Obsolete("Use Get<T> instead")]
        public Vector3 GetVector3(string key)
        {
            return Get<Vector3>(key);
        }

        [System.Obsolete("Use Get<T> instead")]
        public int GetInt(string key)
        {
            return Get<int>(key);
        }

        public T Get<T>(string key)
        {
            ANP_BBValue result = Get(key);
            if (result == null)
            {
                return default;
            }

            NP_BBValueBase<T> finalResult = result as NP_BBValueBase<T>;
            if (finalResult == null)
            {
                Log.Error($"黑板获取值转型失败，Key：{key}，Type：{typeof(NP_BBValueBase<T>)}");
                return default;
            }
            else
            {
                return finalResult.GetValue();
            }
        }

        public ANP_BBValue Get(string key)
        {
            if (this.m_Data.ContainsKey(key))
            {
                return m_Data[key];
            }
            else if (this.m_ParentBlackboard != null)
            {
                return this.m_ParentBlackboard.Get(key);
            }
            else
            {
                return null;
            }
        }

        public bool Isset(string key)
        {
            return this.m_Data.ContainsKey(key) ||
                   (this.m_ParentBlackboard != null && this.m_ParentBlackboard.Isset(key));
        }

        public void AddObserver(string key, System.Action<Type, ANP_BBValue> observer)
        {
            List<System.Action<Type, ANP_BBValue>> observers = GetObserverList(this.m_Observers, key);
            if (!m_IsNotifiyng)
            {
                if (!observers.Contains(observer))
                {
                    observers.Add(observer);
                }
            }
            else
            {
                if (!observers.Contains(observer))
                {
                    List<System.Action<Type, ANP_BBValue>> addObservers = GetObserverList(this.m_AddObservers, key);
                    if (!addObservers.Contains(observer))
                    {
                        addObservers.Add(observer);
                    }
                }

                List<System.Action<Type, ANP_BBValue>> removeObservers = GetObserverList(this.m_RemoveObservers, key);
                if (removeObservers.Contains(observer))
                {
                    removeObservers.Remove(observer);
                }
            }
        }

        public void RemoveObserver(string key, System.Action<Type, ANP_BBValue> observer)
        {
            List<System.Action<Type, ANP_BBValue>> observers = GetObserverList(this.m_Observers, key);
            if (!m_IsNotifiyng)
            {
                if (observers.Contains(observer))
                {
                    observers.Remove(observer);
                }
            }
            else
            {
                List<System.Action<Type, ANP_BBValue>> removeObservers = GetObserverList(this.m_RemoveObservers, key);
                if (!removeObservers.Contains(observer))
                {
                    if (observers.Contains(observer))
                    {
                        removeObservers.Add(observer);
                    }
                }

                List<System.Action<Type, ANP_BBValue>> addObservers = GetObserverList(this.m_AddObservers, key);
                if (addObservers.Contains(observer))
                {
                    addObservers.Remove(observer);
                }
            }
        }


#if UNITY_EDITOR
        public List<string> Keys
        {
            get
            {
                if (this.m_ParentBlackboard != null)
                {
                    List<string> keys = this.m_ParentBlackboard.Keys;
                    keys.AddRange(m_Data.Keys);
                    return keys;
                }
                else
                {
                    return new List<string>(m_Data.Keys);
                }
            }
        }

        public int NumObservers
        {
            get
            {
                int count = 0;
                foreach (string key in m_Observers.Keys)
                {
                    count += m_Observers[key].Count;
                }

                return count;
            }
        }
#endif


        private void NotifiyObservers()
        {
            if (this.m_Notifications.Count == 0)
            {
                return;
            }

            this.m_NotificationsDispatch.Clear();
            this.m_NotificationsDispatch.AddRange(this.m_Notifications);
            foreach (Blackboard child in this.m_Children)
            {
                child.m_Notifications.AddRange(this.m_Notifications);
                child.m_Clock.AddTimer(1, child.NotifiyObservers);
            }

            m_Notifications.Clear();

            this.m_Notifications.Clear();

            this.m_IsNotifiyng = true;
            foreach (Notification notification in this.m_NotificationsDispatch)
            {
                if (!this.m_Observers.ContainsKey(notification.key))
                {
                    //                Debug.Log("1 do not notify for key:" + notification.key + " value: " + notification.value);
                    continue;
                }

                List<System.Action<Type, ANP_BBValue>> observers = GetObserverList(this.m_Observers, notification.key);
                foreach (System.Action<Type, ANP_BBValue> observer in observers)
                {
                    if (this.m_RemoveObservers.ContainsKey(notification.key) &&
                        this.m_RemoveObservers[notification.key].Contains(observer))
                    {
                        continue;
                    }

                    observer(notification.type, notification.value);
                }
            }

            foreach (string key in this.m_AddObservers.Keys)
            {
                GetObserverList(this.m_Observers, key).AddRange(this.m_AddObservers[key]);
            }

            foreach (string key in this.m_RemoveObservers.Keys)
            {
                foreach (System.Action<Type, ANP_BBValue> action in this.m_RemoveObservers[key])
                {
                    GetObserverList(this.m_Observers, key).Remove(action);
                }
            }

            this.m_AddObservers.Clear();
            this.m_RemoveObservers.Clear();

            this.m_IsNotifiyng = false;
        }

        private List<System.Action<Type, ANP_BBValue>> GetObserverList(
            Dictionary<string, List<System.Action<Type, ANP_BBValue>>> target, string key)
        {
            List<System.Action<Type, ANP_BBValue>> observers;
            if (target.ContainsKey(key))
            {
                observers = target[key];
            }
            else
            {
                observers = new List<System.Action<Type, ANP_BBValue>>();
                target[key] = observers;
            }

            return observers;
        }
    }
}