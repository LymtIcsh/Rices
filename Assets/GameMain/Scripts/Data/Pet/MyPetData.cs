using System;
using UnityEngine;

namespace Suture
{
    [Serializable]
    public class MyPetData : PetData
    {
      //  [SerializeField] private string m_Name = null;

        public MyPetData(int entityId, int typeId) : base(entityId, typeId, CampType.Player)
        {
        }

        /// <summary>
        /// 角色名称。
        /// </summary>
        public string Name
        {
            get => m_unitAttributesNodeDataBase.UnitName;
          //  set => m_unitAttributesNodeDataBase.UnitName = value;
        }
    }
}