using GameFramework.DataTable;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Suture
{
    public class WeaponData : AccessoryObjectData
    {
        [SerializeField]
        private int m_Attack =0;

        [SerializeField]
        private float m_AttackInterval = 0f;

        [SerializeField]
        private float m_WeaponSpeed = 0f;

        [SerializeField]
        private int m_WeaponSoundId = 0;

        public WeaponData(int entityId, int typeId, int ownerId, CampType ownerCamp)
            : base(entityId, typeId, ownerId, ownerCamp)
        {
            IDataTable<DRWeapon> dtWeapons = GameEntry.DataTable.GetDataTable<DRWeapon>();
            DRWeapon drWeapons = dtWeapons.GetDataRow(TypeId);
            if(drWeapons == null)
            {
                return;
            }

            m_Attack = drWeapons.Attack;
            m_AttackInterval = drWeapons.AttackInterval;
            m_WeaponSpeed = drWeapons.WeaponSpeed;
            m_WeaponSoundId = drWeapons.WeaponSoundId;
        }

        /// <summary>
        /// 攻击力。
        /// </summary>
        public int Attack
        {
            get
            {
                return m_Attack;
            }
        }

        /// <summary>
        /// 攻击间隔。
        /// </summary>
        public float AttackInterval
        {
            get
            {
                return m_AttackInterval;
            }
        }

        /// <summary>
        /// 武器速度
        /// </summary>
        public float WeaponSpeed
        {
            get
            {
                return m_WeaponSpeed;
            }
        }

        /// <summary>
        /// 武器音效
        /// </summary>
        public int WeaponSoundId
        {
            get
            {
                return m_WeaponSoundId;
            }
        }
    }
}
