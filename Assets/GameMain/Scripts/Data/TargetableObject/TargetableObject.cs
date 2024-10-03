using System;
using System.Collections.Generic;
using GameFramework.Fsm;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityGameFramework.Runtime;

namespace Suture
{
    
    /// <summary>
    /// 可作为目标的实体类。
    /// </summary>
    public abstract class TargetableObject : EntityBase
    {
        private EntityLoader entityLoader;
        
        private TargetableObjectData m_TargetableObjectData = null;

        private Quaternion rotation = Quaternion.identity;
        
        /// <summary>
        /// 当前持有的状态，用于外部获取对比，减少遍历次数
        /// Key为状态类型，V为具体状态类
        /// </summary>
        protected List<FsmState<TargetableObject>>
            m_States = new List<FsmState<TargetableObject>>();


        /// <summary>
        /// 互斥的状态，如果当前身上有这些状态，将无法切换至此状态
        /// </summary>
        [LabelText("互斥的状态")] protected StateTypes ConflictState =
            StateTypes.RePluse | StateTypes.Dizziness | StateTypes.Striketofly | StateTypes.Sneer | StateTypes.Fear;


        protected IFsm<TargetableObject> m_fsm;

        public Quaternion Rotation
        {
            get => this.rotation;
            set { this.rotation = value; }
        }


        /// <summary>
        /// 归属的房间
        /// </summary>
        public Room BelongToRoom;

        public abstract ImpactData GetImpactData();


#if UNITY_2017_3_OR_NEWER
        protected override void OnInit(object userData)
#else
        protected internal override void OnInit(object userData)
#endif
        {
            base.OnInit(userData);
            gameObject.SetLayerRecursively(Constant.Layer.TargetableObjectLayerId);
        }

#if UNITY_2017_3_OR_NEWER
        protected override void OnShow(object userData)
#else
        protected internal override void OnShow(object userData)
#endif
        {
            base.OnShow(userData);

            m_TargetableObjectData = userData as TargetableObjectData;
            if (m_TargetableObjectData == null)
            {
                Log.Error("Targetable object data is invalid.");
                return;
            }
        }

        /// <summary>
        /// 隐藏实体
        /// </summary>
        /// <param name="attacker"></param>
        protected virtual void OnDead(EntityBase attacker)
        {
            if (GameEntry.Entity.HasEntity(attacker.Id))
            {
                GameEntry.Entity.HideEntity(attacker);
            }
        }
        
        #region Fsm

        /// <summary>
        /// 是否会发生状态互斥，只要包含了conflictStateTypes的子集，就返回true
        /// </summary>
        /// <param name="conflictStateTypes">互斥的状态</param>
        /// <returns></returns>
        public bool CheckConflictState(StateTypes conflictStateTypes)
        {
            //TODO  判断特定的枚举值是否在选中的枚举值中   
            //应该 当前状态 是否 排斥状态中 
            if (conflictStateTypes.HasFlag(((AFsmStateBase)m_fsm.CurrentState).StateTypes))
            {
                return true;
            }

            return false;
        }

        #region 移除状态
        

        #endregion


  

 


        #region 添加状态

        protected void CreateFsm()
        {
            AddFsmState();

            m_fsm = GameEntry.Fsm.CreateFsm<TargetableObject>(gameObject.name, this, m_States);
            StartFsm();
        }


        protected virtual void StartFsm()
        {
            // m_fsm.Start<IdleState>();
        }

        protected virtual void AddFsmState()
        {
            // m_States.Add( IdleState.Create());
            // m_States.Add( RunState.Create());
            // m_States.Add( RunEndState.Create());
        }
        
        #endregion
        #endregion
        protected int ShowEntity<T>(Action<Entity> onShowSuccess,string entityGroup,
            int ArmorAsset, EntityData entityData, object userData = null) where T : EntityLogic
        {
            entityLoader ??= EntityLoader.Create(this);

            return entityLoader.ShowEntity<T>( onShowSuccess,entityGroup,ArmorAsset,entityData, userData);
        }
    }
}