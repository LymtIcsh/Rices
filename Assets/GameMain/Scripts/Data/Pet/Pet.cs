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
        
        public void ApplyDamage(EntityBase attacker, int damageHP)
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

        protected override void OnDead(EntityBase attacker)
        {
            base.OnDead(attacker);
            
            GameEntry.Fsm.DestroyFsm(m_fsm);
            m_fsm = null;
        }
        

        public override ImpactData GetImpactData()
        {
            return new ImpactData(_petData.Camp/*, _petData.HP, _petData.ASK, _petData.Defense*/);
        }
     
        
    }
    
}