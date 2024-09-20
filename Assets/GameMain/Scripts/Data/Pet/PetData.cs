using GameFramework.DataTable;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GameFramework;
using UnityEngine;
using Sirenix.OdinInspector;
using UnityGameFramework.Runtime;

namespace Suture
{
    [Serializable]
    public  class PetData : TargetableObjectData
    {
        [ShowInInspector] private List<EquipData> m_ArmorDatas = new List<EquipData>();

        // [SerializeField] private int m_MaxHP = 0;

        [SerializeField] private int m_Money = 0;

        [SerializeField] private CampType m_Camp = CampType.Unknown;
        
        /// <summary>
        /// 最大生命值
        /// </summary>
        public int MaxHP;

        /// <summary>
        /// 当前等级
        /// </summary>
        public int Level=1;

        [LabelText("英雄数据")] [ShowInInspector]
        public HeroAttributesNodeData m_unitAttributesNodeDataBase;

        protected PetData(int entityId, int typeId, CampType camp) : base()
        {     
            TypeId = typeId;
            Id = entityId;
            
            IDataTable<DRPet> dtPets = GameEntry.DataTable.GetDataTable<DRPet>();
            DRPet drPets = dtPets.GetDataRow(TypeId);
            if (drPets == null)
            {
                return;
            }
            
            m_Camp = camp;
            // m_HP = 0;
            // m_ASK = 0;
            
            m_unitAttributesNodeDataBase = GameEntry.UnitAttributesDataRepository
                .GetUnitAttributesDataById_DeepCopy<HeroAttributesNodeData>(TypeId, TypeId);

            MaxHP = (int)(m_unitAttributesNodeDataBase.OriHP + m_unitAttributesNodeDataBase.GroHP * Level);
            // Defense = drPets.InitDefense;
            // ASK = drPets.InitAsk;
            // HP = m_MaxHP = drPets.InitHp;
        }

        // public PetData()
        // {
        //     IDataTable<DRPet> dtPets = GameEntry.DataTable.GetDataTable<DRPet>();
        //     DRPet drPets = dtPets.GetDataRow(TypeId);
        //     
        //     if (drPets == null)
        //     {
        //         Log.Info("drPets 为空");
        //     }
        // }


        // public static PetData Create(int entityId, int typeId,Vector3 position,CampType camp,object userData = null)
        // {
        //     PetData entityData = ReferencePool.Acquire<PetData>();
        //     
        //     entityData.Id = entityId;
        //     entityData.TypeId = typeId;
        //     entityData.Position = position;
        //     entityData.m_Camp = camp;
        //     entityData.UserData = userData;
        //     
        //     IDataTable<DRPet> dtPets = GameEntry.DataTable.GetDataTable<DRPet>();
        //     DRPet drPets = dtPets.GetDataRow(typeId);
        //     
        //     if (drPets == null)
        //     {
        //         Log.Info("drPets 为空");
        //     }
        //     // m_HP = 0;
        //     // m_ASK = 0;
        //     
        //     entityData. m_unitAttributesNodeDataBase = GameEntry.UnitAttributesDataRepository
        //         .GetUnitAttributesDataById_DeepCopy<HeroAttributesNodeData>(typeId, entityId);
        //
        //     entityData.MaxHP = (int)(entityData.m_unitAttributesNodeDataBase.OriHP + entityData.m_unitAttributesNodeDataBase.GroHP * entityData.Level);
        //     return entityData;
        // }
        
        /// <summary>
        /// 角色阵营
        /// </summary>
        public CampType Camp
        {
            get { return m_Camp; }
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
        
        /// <summary>
        /// 生命百分比。
        /// </summary>
        public float HPRatio
        {
            get { return  MaxHP> 0 ? (float)m_unitAttributesNodeDataBase.OriHP / MaxHP : 0f; }
        }
    }
}