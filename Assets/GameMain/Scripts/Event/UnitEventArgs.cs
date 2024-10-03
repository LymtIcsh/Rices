using System;
using GameFramework;
using GameFramework.Event;

namespace Suture
{
    public class UnitEventArgs : GameEventArgs
    {    
        /// <summary>
             /// 显示实体成功事件编号。
             /// </summary>
        public static readonly int EventId = typeof(UnitEventArgs).GetHashCode();
     

        public override int Id => EventId;
        
        /// <summary>
        /// 初始化显示实体成功事件的新实例。
        /// </summary>
        public UnitEventArgs()
        {
            EntityLogicType = null;
            EntityBase = null;
            Duration = 0f;
            UserData = null;
        }



        /// <summary>
        /// 获取实体逻辑类型。
        /// </summary>
        public Type EntityLogicType
        {
            get;
            private set;
        }

        /// <summary>
        /// 获取显示成功的实体。
        /// </summary>
        public EntityBase EntityBase
        {
            get;
            private set;
        }

        /// <summary>
        /// 获取加载持续时间。
        /// </summary>
        public float Duration
        {
            get;
            private set;
        }

        /// <summary>
        /// 获取用户自定义数据。
        /// </summary>
        public object UserData
        {
            get;
            private set;
        }

        /// <summary>
        /// 创建显示实体成功事件。
        /// </summary>
        /// <param name="e">内部事件。</param>
        /// <returns>创建的显示实体成功事件。</returns>
        public static UnitEventArgs Create(Type entityLogicType,object UserData=null)
        {
            UnitEventArgs showEntitySuccessEventArgs = ReferencePool.Acquire<UnitEventArgs>();
            showEntitySuccessEventArgs.EntityLogicType = entityLogicType;
            showEntitySuccessEventArgs.UserData =UserData;
            return showEntitySuccessEventArgs;
        }

        /// <summary>
        /// 清理显示实体成功事件。
        /// </summary>
        public override void Clear()
        {
            EntityLogicType = null;
            EntityBase = null;
            Duration = 0f;
            UserData = null;
        }
    }
}