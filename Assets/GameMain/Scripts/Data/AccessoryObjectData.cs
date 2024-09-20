using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Suture
{
    /// <summary>
    /// 附件对象数据
    /// </summary>
    [Serializable]
    public abstract class AccessoryObjectData : EntityData
    {
        [SerializeField]
        private int m_OwnerId = 0;

        [SerializeField]
        private CampType m_OwnerCamp = CampType.Unknown;

        protected AccessoryObjectData(int entityId, int typeId, int ownerId, CampType ownerCamp)
            : base()
        {
            m_OwnerId = ownerId;
            m_OwnerCamp = ownerCamp;
        }

        /// <summary>
        /// 拥有者编号。
        /// </summary>
        public int OwnerId
        {
            get
            {
                return m_OwnerId;
            }
        }

        /// <summary>
        /// 拥有者阵营。
        /// </summary>
        public CampType OwnerCamp
        {
            get
            {
                return m_OwnerCamp;
            }
        }
    }
}
