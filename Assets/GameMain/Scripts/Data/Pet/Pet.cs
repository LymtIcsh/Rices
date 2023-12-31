﻿using System.Collections.Generic;
using GameFramework;
using UnityEngine;
using UnityGameFramework.Runtime;

namespace Suture
{
    public class Pet : TargetableObject
    {
        [SerializeField] private PetData _petData = null;



        [SerializeField] protected List<Equip> _armors = new List<Equip>();

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

            Name = Utility.Text.Format("Rices", Id);


            List<EquipData> armorDatas = _petData.GetAllArmorDatas();
            foreach (var t in armorDatas)
            {
                GameEntry.Entity.ShowArmor(t);
            }
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

        protected override void OnDead(Entity attacker)
        {
            base.OnDead(attacker);
            
            
        }

        public override ImpactData GetImpactData()
        {
            return new ImpactData(_petData.Camp, _petData.HP, _petData.ASK, _petData.Defense);
        }
    }
}