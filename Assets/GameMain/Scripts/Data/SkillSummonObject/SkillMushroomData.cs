
using GameFramework;
using UnityEngine;

namespace Suture
{
    public class SkillMushroomData:SkillObjDataBase
    {
        public static  SkillMushroomData Create(int entityId, int typeId,string AssetName,Vector3 position,TargetableObject belongPet,object userData = null)
        {
            SkillMushroomData entityData = ReferencePool.Acquire<SkillMushroomData>();
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