using System;
using GameFramework;
using GameFramework.DataTable;
using UnityEngine;
using UnityGameFramework.Runtime;

namespace Suture
{
    [Serializable]
    public class MyPetData : PetData
    {
      //  [SerializeField] private string m_Name = null;

        public MyPetData(int entityId, int typeId) : base(entityId, typeId, CampType.Player)
        {
        }

        // public MyPetData()
        // {
        //     
        // }
        //
        // public static MyPetData Create(int entityId, int typeId,Vector3 position,CampType camp,object userData = null)
        // {
        //     MyPetData entityData = ReferencePool.Acquire<MyPetData>();
        //     
        //     entityData.Id = entityId;
        //     entityData.TypeId = typeId;
        //     entityData.Position = position;
        //     entityData.UserData = userData;
        //     
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
        /// 角色名称。
        /// </summary>
        public string Name
        {
            get => m_unitAttributesNodeDataBase.UnitName;
            //set => m_unitAttributesNodeDataBase.UnitName = value;
        }
    }
}