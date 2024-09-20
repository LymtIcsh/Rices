using System;
using System.Collections.Generic;
using GameFramework.Fsm;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityGameFramework.Runtime;

namespace Suture
{
    /// <summary>
    /// 可作为目标的实体类。
    /// </summary>
    public abstract class TargetableObject : Entity
    {
        private TargetableObjectData m_TargetableObjectData = null;


        private Quaternion rotation = Quaternion.identity;

        public Quaternion Rotation
        {
            get => this.rotation;
            set { this.rotation = value; }
        }


        /// <summary>
        /// 归属的房间
        /// </summary>
        public Room BelongToRoom;

        public abstract ImpactData GetImpactData();


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
            if (GameEntry.Entity.HasEntity(attacker.Id))
            {
                GameEntry.Entity.HideEntity(attacker);
            }
        }
    }
}