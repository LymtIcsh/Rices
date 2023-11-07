using GameFramework.DataTable;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Suture
{
    public class EquipData : AccessoryObjectData
    {
        [SerializeField] private int m_MaxHP = 0;

        [SerializeField] private int m_Defense = 0;

        [SerializeField] private int m_Ask = 0;

        [SerializeField] public int m_sellingPrice = 0;

        public EquipData(int entityId, int typeId, int ownerId, CampType ownerCamp)
            : base(entityId, typeId, ownerId, ownerCamp)
        {
            IDataTable<DREquip> drEquips = GameEntry.DataTable.GetDataTable<DREquip>();
            DREquip drEquip = drEquips.GetDataRow(TypeId);
            if (drEquip == null)
            {
                return;
            }

            m_MaxHP = drEquip.OfferHP;
            m_Ask = drEquip.OfferAsk;
            m_Defense = drEquip.OfferDefense;
            m_sellingPrice = drEquip.SellingPrice;
        }


        /// <summary>
        /// 最大生命。
        /// </summary>
        public int MaxHP
        {
            get { return m_MaxHP; }
        }

        /// <summary>
        /// 防御力。
        /// </summary>
        public int Defense
        {
            get { return m_Defense; }
        }

        /// <summary>
        /// 攻击力。
        /// </summary>
        public int Ask
        {
            get { return m_Ask; }
        }

        /// <summary>
        /// 销售价格。
        /// </summary>
        public int SellingPrice
        {
            get { return m_sellingPrice; }
        }
    }
}