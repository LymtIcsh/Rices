using System.Collections;
using System.Collections.Generic;
using GameFramework;
using UnityEngine;

namespace Suture
{
    [SerializeField]
    public class EntityData : IReference
    {
        [SerializeField] private int m_Id = 0;

        [SerializeField] private int m_TypeId = 0;

        [SerializeField] private Vector3 m_Position = Vector3.zero;

        [SerializeField] private Vector3 m_Scale = Vector3.one;

        [SerializeField] private Quaternion m_Rotation = Quaternion.identity;


        public EntityData()
        {
            m_Position = Vector3.zero;
            m_Rotation = Quaternion.identity;
            UserData = null;
        }

        /// <summary>
        /// 实体编号。
        /// </summary>
        public int Id
        {
            get => m_Id;
            set => m_Id = value;
        }

        /// <summary>
        /// 实体类型编号。
        /// </summary>
        public int TypeId
        {
            get => m_TypeId;
            set => m_TypeId = value;
        }

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

        public object UserData { get; protected set; }

        public static EntityData Create(object userData = null)
        {
            EntityData entityData = ReferencePool.Acquire<EntityData>();
            entityData.Position = Vector3.zero;
            entityData.Rotation = Quaternion.identity;
            entityData.UserData = userData;
            return entityData;
        }

        public static EntityData Create(Vector3 position, object userData = null)
        {
            EntityData entityData = ReferencePool.Acquire<EntityData>();
            entityData.Position = position;
            entityData.Rotation = Quaternion.identity;
            entityData.UserData = userData;
            return entityData;
        }

        public static EntityData Create(Vector3 position, Quaternion quaternion, object userData = null)
        {
            EntityData entityData = ReferencePool.Acquire<EntityData>();
            entityData.Position = position;
            entityData.Rotation = quaternion;
            entityData.UserData = userData;
            return entityData;
        }

        public void Clear()
        {
            m_Position = Vector3.zero;
            m_Rotation = Quaternion.identity;
            UserData = null;
        }
    }
}