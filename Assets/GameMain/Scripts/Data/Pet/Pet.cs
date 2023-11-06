using System.Collections.Generic;
using GameFramework;
using UnityEngine;
using UnityGameFramework.Runtime;

namespace Suture
{
    public class Pet : TargetableObject
    {
        [SerializeField] private PetData _petData = null;

        [SerializeField] protected List<Weapon> _weapons = new List<Weapon>();

        [SerializeField] protected List<Armor> _armors = new List<Armor>();

#if UNITY_2017_3_OR_NEWER
        protected override void OnShow(object userData)
#else
        protected internal override void OnShow(object userData)
#endif
        {
            base.OnShow(userData);

            if (_petData == null)
            {
                Log.Error("Aircraft data is invalid.");
                return;
            }

            Name = Utility.Text.Format("Rices", Id);

            List<WeaponData> weaponDatas = _petData.GetAllWeaponDatas();
            for (int i = 0; i < weaponDatas.Count; i++)
            {
                GameEntry.Entity.ShowWeapon(weaponDatas[i]);
            }

            List<ArmorData> armorDatas = _petData.GetAllArmorDatas();
            for (int i = 0; i < armorDatas.Count; i++)
            {
                GameEntry.Entity.ShowArmor(armorDatas[i]);
            }
        }

        public override ImpactData GetImpactData()
        {
            return new ImpactData(_petData.Camp, _petData.HP, 0, _petData.Defense);
        }
    }
}