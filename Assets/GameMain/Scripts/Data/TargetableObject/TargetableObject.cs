

using System;
using UnityEngine;
using UnityGameFramework.Runtime;

namespace Suture
{
    /// <summary>
    /// 可作为目标的实体类。
    /// </summary>
    public abstract class TargetableObject:Entity
    {
        [SerializeField]
        private TargetableObjectData m_TargetableObjectData = null;

        /// <summary>
        /// 是否死亡
        /// </summary>
        public bool IsDead
        {
            get
            {
                return m_TargetableObjectData.HP <= 0;
            }
        }
        
        
        /// <summary>
        /// 归属的房间
        /// </summary>
        public Room BelongToRoom;

        public abstract ImpactData GetImpactData();

        public void ApplyDamage(Entity attacker, int damageHP)
        {
            float fromHPRatio = m_TargetableObjectData.HPRatio;
            m_TargetableObjectData.HP-= damageHP;
            float toHPRatio = m_TargetableObjectData.HPRatio;
            if (fromHPRatio > toHPRatio)
            {
               //原demo应该是，受伤显示血条
            }
            if(m_TargetableObjectData.HP <= 0)
            {
                OnDead(attacker);
            }
        }

#if UNITY_2017_3_OR_NEWER
        protected override void OnInit(object userData)
#else
        protected internal override void OnInit(object userData)
#endif
        {
            base.OnInit(userData);
            gameObject.SetLayerRecursively(Constant.Layer.TargetableObjectLayerId);
        }

#if UNITY_2017_3_OR_NEWER
        protected override void OnShow(object userData)
#else
        protected internal override void OnShow(object userData)
#endif
        {
            base.OnShow(userData);

            m_TargetableObjectData = userData as TargetableObjectData;
            if (m_TargetableObjectData == null)
            {
                Log.Error("Targetable object data is invalid.");
                return;
            }
        }

        /// <summary>
        /// 隐藏实体
        /// </summary>
        /// <param name="attacker"></param>
        protected virtual void OnDead(Entity attacker)
        {
            GameEntry.Entity.HideEntity(this);
        }

        
    }
}
