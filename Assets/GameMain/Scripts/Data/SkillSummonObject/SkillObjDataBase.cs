using GameFramework;
using UnityEngine;

namespace Suture
{
    public class SkillObjDataBase:TargetableObjectData
    {
        public string AssetName;
        
        public Pet belongPet;

        public static  SkillObjDataBase Create(int entityId, int typeId,string AssetName,Vector3 position,Pet belongPet,object userData = null)
        {
            SkillObjDataBase entityData = ReferencePool.Acquire<SkillObjDataBase>();
            entityData.Id = entityId;
            entityData.TypeId = typeId;
            entityData.Position = position;
            entityData.AssetName = AssetName;
            entityData.belongPet = belongPet;
            entityData.UserData = userData;
            return entityData;
        }
    }
}