using System;
using System.Collections.Generic;

namespace Suture
{
    public class NumericComponent : Entity
    {
// #if SERVER
//         /// <summary>
//         /// 每帧Attribute的结果
//         /// </summary>
//         public Dictionary<uint, Dictionary<int, float>> AttributeReusltFrameSnap =
//             new Dictionary<uint, Dictionary<int, float>>();
//         
//         /// <summary>
//         /// 每帧变化量
//         /// </summary>
//         public Dictionary<uint, Dictionary<int, float>> AttributeChangeFrameSnap =
//             new Dictionary<uint, Dictionary<int, float>>();
// #endif
        public Dictionary<int, float> NumericDic = new Dictionary<int, float>();

        public Dictionary<int, float> OriNumericDic = new Dictionary<int, float>();


        public void Destroy()
        {
            NumericDic.Clear();
            OriNumericDic.Clear();
        }
        
        /// <summary>
        /// 初始化初始值字典，用于值回退，比如一个buff加50ad，buff移除后要减去这个50ad，就需要用到OriNumericDic里的值
        /// </summary>
        public  void InitOriNumerDic()
        {
            OriNumericDic = new Dictionary<int, float>(NumericDic);
        }
        
        public  float GetOriData(NumericType numericType)
        {
            return OriNumericDic[(int) numericType];
        }
        
        /// <summary>
        /// 适配变化
        /// </summary>
        public  void ApplyChange(NumericType numericType, float changedValue)
        {
            TargetableObject unit = GetComponent<TargetableObject>();
            // Game.EventSystem.Publish(new EventType.NumericApplyChangeValue()
            //     {ChangedValue = changedValue, NumericType = numericType, Unit = unit}).Coroutine();
            // this[numericType] += changedValue;
            
#if SERVER
            uint currentFrame = self.GetParent<Unit>().BelongToRoom.GetComponent<LSF_Component>().CurrentFrame;
            self.AttributeChangeFrameSnap[currentFrame][(int)numericType] = changedValue;
#endif
        }


        
        /// <summary>
        /// 设置不带广播的值
        /// </summary>
        /// <param name="numericType"></param>
        /// <param name="value"></param>
        public void SetValueWithoutBroadCast(NumericType numericType, float value)
        {
            NumericDic[(int)numericType] = value;
        }

        public float this[NumericType numericType]
        {
            get { return this.GetByKey((int)numericType); }
            set
            {
                float v = this.GetByKey((int)numericType);
                if (Math.Abs(v - value) <= 0.00001f)
                    return;

                NumericDic[(int)numericType] = value;
                UpdateNumericType(numericType);
            }
        }

        public float GetByKey(int key)
        {
            float value = 0;
            this.NumericDic.TryGetValue(key, out value);
            return value;
        }

        public void UpdateNumericType(NumericType numericType)
        {
            int final = (int)numericType;
            float result = this.NumericDic[final];

// #if SERVER
//             uint currentFrame = this.GetParent<Unit>().BelongToRoom.GetComponent<LSF_Component>().CurrentFrame;
//             this.AttributeReusltFrameSnap[currentFrame][(int)numericType] = result;
// #endif

            //如果不是直接操作最终值，需要发送两次事件，一次是修改的值，一次是最终值
            if (numericType > NumericType.Min)
            {
                final = (int)numericType / 10;
                int bas = final * 10 + 1;
                int add = final * 10 + 2;

                //取得最终值，由基础xxx+额外xxx值组成
                float finalResult = this.GetByKey(bas) + this.GetByKey(add);
                //更新最终值
                this[(NumericType)final] = this.Entity.transform.parent.GetComponent<DataModifierComponent>()
                    .BaptismData(numericType.ToString(), finalResult);
            }

            //将改变的值以事件的形式发送出去
            GameEntry.Event.Fire(this, NumericChangeEventArgs.Create(this, numericType, result));
        }
        
        public Dictionary<int, float> GetOriNum()
        {
            return this.OriNumericDic;
        }
        
    }
}