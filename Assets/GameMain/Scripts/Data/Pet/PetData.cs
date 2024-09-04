using GameFramework.DataTable;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using Sirenix.OdinInspector;

namespace Suture
{
    [Serializable]
    public abstract class PetData : TargetableObjectData
    {
        [ShowInInspector] private List<EquipData> m_ArmorDatas = new List<EquipData>();

        // [SerializeField] private int m_MaxHP = 0;

        [SerializeField] private int m_Money = 0;

       

        protected PetData(int entityId, int typeId, CampType camp) : base(entityId, typeId, camp)
        {
            IDataTable<DRPet> dtPets = GameEntry.DataTable.GetDataTable<DRPet>();
            DRPet drPets = dtPets.GetDataRow(TypeId);
            if (drPets == null)
            {
                return;
            }
            
            
            // Defense = drPets.InitDefense;
            // ASK = drPets.InitAsk;
            // HP = m_MaxHP = drPets.InitHp;
        }

        // /// <summary>
        // /// 最大生命。
        // /// </summary>
        // public override int MaxHP
        // {
        //     get { return m_MaxHP; }
        // }


        public List<EquipData> GetAllArmorDatas()
        {
            return m_ArmorDatas;
        }

        public void AttachArmorData(EquipData equipData)
        {
            if (equipData == null)
            {
                return;
            }

            if (m_ArmorDatas.Contains(equipData))
            {
                return;
            }

            m_ArmorDatas.Add(equipData);
            RefreshData();
        }

        public void DetachArmorData(EquipData equipData)
        {
            if (equipData == null)
            {
                return;
            }

            m_ArmorDatas.Remove(equipData);
            RefreshData();
        }

        /// <summary>
        /// 刷新护甲装备数据
        /// </summary>
        private void RefreshData()
        {
            // m_MaxHP = 0;
            // Defense = 0;

            for (int i = 0; i < m_ArmorDatas.Count; i++)
            {
                m_unitAttributesNodeDataBase.OriAttackValue += m_ArmorDatas[i].Ask;
                MaxHP += m_ArmorDatas[i].MaxHP;
                m_unitAttributesNodeDataBase.OriArmor += m_ArmorDatas[i].Defense;
            }

            if (m_unitAttributesNodeDataBase.OriHP > MaxHP)
            {
                m_unitAttributesNodeDataBase.OriHP = MaxHP;
            }
        }
    }
}