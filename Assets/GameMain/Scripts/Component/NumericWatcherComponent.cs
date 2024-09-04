using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using GameFramework.Event;
using UnityGameFramework.Runtime;

namespace Suture
{
    /// <summary>
    /// 监视数值变化组件,分发监听
    /// </summary>
    public class NumericWatcherComponent : GameFrameworkComponent
    {
        private Dictionary<NumericType, List<INumericWatcher>> allWatchers;

        protected override void Awake()
        {
            base.Awake();

            this.Load();
        }

        public void NumericWatcherSubscribe()
        {
              GameEntry.Event.Subscribe(NumericChangeEventArgs.EventID, Run);
        }

        public void Load()
        {
            this.allWatchers = new Dictionary<NumericType, List<INumericWatcher>>();

            //// 找出了实现 INumericWatcher 接口的所有类
            var types = AppDomain.CurrentDomain.GetAssemblies()
                .SelectMany(a => a.GetTypes())
                .Where(t => t.GetInterfaces().Contains(typeof(INumericWatcher))).ToArray();

            foreach (Type type in types)
            {
                object[] attrs = type.GetCustomAttributes(typeof(NumericWatcherAttribute), false);

                foreach (object attr in attrs)
                {
                    NumericWatcherAttribute numericWatcherAttribute = (NumericWatcherAttribute)attr;
                    INumericWatcher obj = (INumericWatcher)Activator.CreateInstance(type);
                    if (!this.allWatchers.ContainsKey(numericWatcherAttribute.NumericType))
                    {
                        this.allWatchers.Add(numericWatcherAttribute.NumericType, new List<INumericWatcher>());
                    }

                    this.allWatchers[numericWatcherAttribute.NumericType].Add(obj);
                }
            }
        }


        private void Run(object sender, GameEventArgs e)
        {
            NumericChangeEventArgs ne = (NumericChangeEventArgs)e;
            if (ne == null)
                return;

            List<INumericWatcher> list;
            if (!this.allWatchers.TryGetValue(ne.NumericType, out list))
            {
                return;
            }

            foreach (INumericWatcher numericWatcher in list)
            {
                numericWatcher.Run(ne.NumericComponent, ne.NumericType, ne.Result);
            }
        }

        public void OnDisable()
        {
            GameEntry.Event.Unsubscribe(NumericChangeEventArgs.EventID, Run);
        }
    }
}