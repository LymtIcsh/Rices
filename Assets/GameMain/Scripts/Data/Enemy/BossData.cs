using GameFramework;
using UnityEngine;

namespace Suture
{
    public class BossData:EnemyData
    {
        public static  BossData Create(int entityId, int typeId,string AssetName,Vector3 position,object userData = null)
        {
            BossData entityData = ReferencePool.Acquire<BossData>();
            entityData.Id = entityId;
            entityData.TypeId = typeId;
            entityData.Position = position;
            entityData.AssetName = AssetName;
            entityData.UserData = userData;
            return entityData;
        }
    }
}