using GameFramework.DataTable;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Suture
{
    [Serializable]
    public abstract class PetData : TargetableObjectData
    {
        [SerializeField] private List<WeaponData> m_WeaponDatas = new List<WeaponData>();

        [SerializeField] private List<ArmorData> m_ArmorDatas = new List<ArmorData>();

        [SerializeField] private int m_MaxHP = 0;
        
        [SerializeField] private int m_Ask = 0;

        [SerializeField] private int m_Defense = 0;

        protected PetData(int entityId, int typeId, CampType camp) : base(entityId, typeId, camp)
        {
            IDataTable<DRPet> dtPets = GameEntry.DataTable.GetDataTable<DRPet>();
            DRPet drPets = dtPets.GetDataRow(TypeId);
            if (drPets == null)
            {
                return;
            }

            m_MaxHP = drPets.InitHp;
            m_Defense = drPets.InitDefense;
            ASK = drPets.InitAsk;
            HP = m_MaxHP;
        }

        /// <summary>
        /// 最大生命。
        /// </summary>
        public override int MaxHP
        {
            get { return m_MaxHP; }
        }

        /// <summary>
        /// 防御。
        /// </summary>
        public int Defense
        {
            get { return m_Defense; }
        }

        public List<WeaponData> GetAllWeaponDatas()
        {
            return m_WeaponDatas;
        }

        public void AttachWeaponData(WeaponData weaponData)
        {
            if (weaponData == null)
            {
                return;
            }

            if (m_WeaponDatas.Contains(weaponData))
            {
                return;
            }

            m_WeaponDatas.Add(weaponData);
        }

        public void DetachWeaponData(WeaponData weaponData)
        {
            if (weaponData == null)
            {
                return;
            }

            m_WeaponDatas.Remove(weaponData);
        }


        public List<ArmorData> GetAllArmorDatas()
        {
            return m_ArmorDatas;
        }

        public void AttachArmorData(ArmorData armorData)
        {
            if (armorData == null)
            {
                return;
            }

            if (m_ArmorDatas.Contains(armorData))
            {
                return;
            }

            m_ArmorDatas.Add(armorData);
            RefreshData();
        }

        public void DetachArmorData(ArmorData armorData)
        {
            if (armorData == null)
            {
                return;
            }

            m_ArmorDatas.Remove(armorData);
            RefreshData();
        }

        /// <summary>
        /// 刷新护甲装备数据
        /// </summary>
        private void RefreshData()
        {
            m_MaxHP = 0;
            m_Defense = 0;
            for (int i = 0; i < m_ArmorDatas.Count; i++)
            {

                m_MaxHP += m_ArmorDatas[i].MaxHP;
                m_Defense += m_ArmorDatas[i].Defense;
            }

            if (HP > m_MaxHP)
            {
                HP = m_MaxHP;
            }
        }
    }
}