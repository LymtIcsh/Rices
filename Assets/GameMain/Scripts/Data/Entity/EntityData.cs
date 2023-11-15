using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Suture
{
    [SerializeField]
    public abstract class EntityData
    {
        [SerializeField] private int m_Id = 0;

        [SerializeField] private int m_TypeId = 0;

        [SerializeField] private Vector3 m_Position = Vector3.zero;

        [SerializeField] private Vector3 m_Scale = Vector3.one;

        [SerializeField] private Quaternion m_Rotation = Quaternion.identity;

        public EntityData(int entityId, int typeId)
        {
            m_Id = entityId;
            m_TypeId = typeId;
        }

        /// <summary>
        /// 实体编号。
        /// </summary>
        public int Id => m_Id;

        /// <summary>
        /// 实体类型编号。
        /// </summary>
        public int TypeId => m_TypeId;

        /// <summary>
        /// 实体位置。
        /// </summary>
        public Vector3 Position
        {
            get => m_Position;
            set => m_Position = value;
        }

        /// <summary>
        /// 实体朝向。
        /// </summary>
        public Quaternion Rotation
        {
            get => m_Rotation;
            set => m_Rotation = value;
        }

        /// <summary>
        /// 实体缩放
        /// </summary>
        public Vector3 Scale
        {
            get => m_Scale;
            set => m_Scale = value;
        }
    }
}