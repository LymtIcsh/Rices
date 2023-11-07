using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Suture
{
    [Serializable]
    public abstract class TargetableObjectData : EntityData
    {
        [SerializeField] private CampType m_Camp = CampType.Unknown;

        [SerializeField] private int m_HP = 0;

        [SerializeField] private int m_ASK = 0;
        
        [SerializeField] private int m_Defense = 0;

        protected TargetableObjectData(int entityId, int typeId, CampType camp) : base(entityId, typeId)
        {
            m_Camp = camp;
            m_HP = 0;
            m_ASK = 0;
        }

        /// <summary>
        /// 角色阵营
        /// </summary>
        public CampType Camp
        {
            get { return m_Camp; }
        }

        /// <summary>
        /// 当前生命
        /// </summary>
        public int HP
        {
            get { return m_HP; }
            set { m_HP = value; }
        }

        /// <summary>
        /// 当前攻击力
        /// </summary>
        public int ASK
        {
            get { return m_ASK; }
            set { m_ASK = value; }
        }
        
        /// <summary>
        /// 当前攻击力
        /// </summary>
        public int Defense
        {
            get { return m_Defense; }
            set { m_Defense = value; }
        }

        /// <summary>
        /// 最大生命值
        /// </summary>
        public abstract int MaxHP { get; }

        /// <summary>
        /// 生命百分比。
        /// </summary>
        public float HPRatio
        {
            get { return MaxHP > 0 ? (float)HP / MaxHP : 0f; }
        }
    }
}