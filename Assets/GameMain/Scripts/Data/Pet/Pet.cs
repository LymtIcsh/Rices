using System.Collections.Generic;
using GameFramework;
using GameFramework.Fsm;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityGameFramework.Runtime;

namespace Suture
{
    public class Pet : TargetableObject
    {
       public PetData _petData = null;

        [SerializeField] protected List<Equip> _armors = new List<Equip>();
        
        /// <summary>
        /// 当前持有的状态，用于外部获取对比，减少遍历次数
        /// Key为状态类型，V为具体状态类
        /// </summary>
        protected List<FsmState<Pet>>
            m_States = new List<FsmState<Pet>>();


        /// <summary>
        /// 互斥的状态，如果当前身上有这些状态，将无法切换至此状态
        /// </summary>
        [LabelText("互斥的状态")] protected StateTypes ConflictState =
            StateTypes.RePluse | StateTypes.Dizziness | StateTypes.Striketofly | StateTypes.Sneer | StateTypes.Fear;


        protected IFsm<Pet> m_fsm;
        

        /// <summary>
        /// 闪避类型
        /// </summary>
        public EvadeEnum _evadeEnum = EvadeEnum.Evade_Front;

        /// <summary>
        /// 是否死亡
        /// </summary>
        public bool IsDead
        {
            get { return _petData.m_unitAttributesNodeDataBase.GroHP <= 0; }
        }
  
        

#if UNITY_2017_3_OR_NEWER
        protected override void OnShow(object userData)
#else
        protected internal override void OnShow(object userData)
#endif
        {
            base.OnShow(userData);
            
            

            _petData = userData as PetData;
            if (_petData == null)
            {
                Log.Error("petData data is invalid.");
                return;
            }

            Name = Utility.Text.Format(_petData.m_unitAttributesNodeDataBase.UnitName, Id);

            List<EquipData> armorDatas = _petData.GetAllArmorDatas();
            
            foreach (var t in armorDatas)
            {
                GameEntry.Entity.ShowArmor(t);
            }
            
            CreateFsm();
        }
        
#if UNITY_2017_3_OR_NEWER
        protected override void OnHide(bool isShutdown, object userData)
#else
        protected internal override void OnHide(bool isShutdown, object userData)
#endif
        {
            base.OnHide(isShutdown,userData);
        }
        
#if UNITY_2017_3_OR_NEWER
        protected override void OnAttached(EntityLogic childEntity, Transform parentTransform, object userData)
#else
        protected internal override void OnAttached(EntityLogic childEntity, Transform parentTransform, object userData)
#endif
        {
            base.OnAttached(childEntity,parentTransform,userData);
            

            if (childEntity is Equip armor)
            {
                _armors.Add(armor);
                return;
            }
        }

#if UNITY_2017_3_OR_NEWER
        protected override void OnDetached(EntityLogic childEntity, object userData)
#else
        protected internal override void OnDetached(EntityLogic childEntity, object userData)
#endif
        {
            base.OnDetached(childEntity,userData);
            

            if (childEntity is Equip armor)
            {
                _armors.Remove(armor);
                return;
            }
        }
        
        public void ApplyDamage(Entity attacker, int damageHP)
        {
            float fromHPRatio = _petData.HPRatio;
            _petData.m_unitAttributesNodeDataBase.OriHP -= damageHP;
            float toHPRatio = _petData.HPRatio;
            if (fromHPRatio > toHPRatio)
            {
                //原demo应该是，受伤显示血条
            }

            if (_petData.m_unitAttributesNodeDataBase.OriHP <= 0)
            {
                OnDead(attacker);
            }
        }

        protected override void OnDead(Entity attacker)
        {
            base.OnDead(attacker);
            
            GameEntry.Fsm.DestroyFsm(m_fsm);
            m_fsm = null;
        }
        

        public override ImpactData GetImpactData()
        {
            return new ImpactData(_petData.Camp/*, _petData.HP, _petData.ASK, _petData.Defense*/);
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

        private void CreateFsm()
        {
            AddFsmState();

            m_fsm = GameEntry.Fsm.CreateFsm<Pet>(gameObject.name, this, m_States);
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
    }
    
}