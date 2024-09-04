using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Suture
{
    [Serializable]
    public abstract class TargetableObjectData : EntityData
    {
        [SerializeField] private CampType m_Camp = CampType.Unknown;

        //[SerializeField] private int m_HP = 0;
        //
        // [SerializeField] private int m_ASK = 0;
        //
        // [SerializeField] private int m_Defense = 0;

        /// <summary>
        /// 最大生命值
        /// </summary>
        public int MaxHP;

        /// <summary>
        /// 当前等级
        /// </summary>
        public int Level=1;

        [LabelText("英雄数据")] [ShowInInspector]
        public UnitAttributesNodeDataBase m_unitAttributesNodeDataBase;

        protected TargetableObjectData(int entityId, int typeId, CampType camp) : base(entityId, typeId)
        {
            m_Camp = camp;
            // m_HP = 0;
            // m_ASK = 0;
            
            m_unitAttributesNodeDataBase = GameEntry.UnitAttributesDataRepository
                .GetUnitAttributesDataById_DeepCopy<HeroAttributesNodeData>(TypeId, entityId);

            MaxHP = (int)(m_unitAttributesNodeDataBase.OriHP + m_unitAttributesNodeDataBase.GroHP * Level);
        }

        /// <summary>
        /// 角色阵营
        /// </summary>
        public CampType Camp
        {
            get { return m_Camp; }
        }

        // /// <summary>
        // /// 当前生命
        // /// </summary>
        // public int HP
        // {
        //     get { return m_HP; }
        //     set { m_HP = value; }
        // }
        //
        // /// <summary>
        // /// 当前攻击力
        // /// </summary>
        // public int ASK
        // {
        //     get { return m_ASK; }
        //     set { m_ASK = value; }
        // }
        //
        // /// <summary>
        // /// 当前防御力
        // /// </summary>
        // public int Defense
        // {
        //     get { return m_Defense; }
        //     set { m_Defense = value; }
        // }
        //
        // /// <summary>
        // /// 最大生命值
        // /// </summary>
        // public abstract int MaxHP { get; }
        

        
      
        /// <summary>
        /// 生命百分比。
        /// </summary>
        public float HPRatio
        {
            get { return  MaxHP> 0 ? (float)m_unitAttributesNodeDataBase.OriHP / MaxHP : 0f; }
        }
    }
}