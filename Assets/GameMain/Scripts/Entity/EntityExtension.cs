﻿using GameFramework.DataTable;
using System;
using UnityGameFramework.Runtime;

namespace Suture
{
    /// <summary>
    /// 实体拓展类
    /// </summary>
    public static class EntityExtension
    {
        // 关于 EntityId 的约定：
        // 0 为无效
        // 正值用于和服务器通信的实体（如玩家角色、NPC、怪等，服务器只产生正值）
        // 负值用于本地生成的临时实体（如特效、FakeObject等）
        private static int s_SerialId = 0;

        /// <summary>
        /// 获取游戏实体
        /// </summary>
        /// <param name="entityComponent"></param>
        /// <param name="entityId">实体Id</param>
        /// <returns></returns>
        public static Entity GetGameEntity(this EntityComponent entityComponent, int entityId)
        {
            UnityGameFramework.Runtime.Entity entity = entityComponent.GetEntity(entityId);
            if (entity == null)
            {
                return null;
            }

            return (Entity)entity.Logic;
        }

        /// <summary>
        /// 隐藏实体
        /// </summary>
        /// <param name="entityComponent"></param>
        /// <param name="entity"></param>
        public static void HideEntity(this EntityComponent entityComponent, Entity entity)
        {
            entityComponent.HideEntity(entity.Entity);
        }

        private static void ShowEntity(this EntityComponent entityComponent, Type logicType, string entityGroup,
            int priority, EntityData data)
        {
            if (data == null)
            {
                Log.Warning("Data is invalid.");
                return;
            }

            IDataTable<DREntity> dtEtity = GameEntry.DataTable.GetDataTable<DREntity>();
            DREntity drEntity = dtEtity.GetDataRow(data.TypeId);
            if (drEntity == null)
            {
                Log.Warning("Can not load entity id '{0}' from data table.", data.TypeId.ToString());
                return;
            }

            entityComponent.ShowEntity(data.Id, logicType, AssetUtility.GetEntityAsset(drEntity.AssetName), entityGroup,
                priority, data);
        }

        public static void ShowMyPet(this EntityComponent entityComponent, MyPetData data)
        {
            entityComponent.ShowEntity(typeof(MyPet), "Pet", Constant.AssetPriority.ArmorAsset, data);
        }
        
        public static void ShowArmor(this EntityComponent entityComponent, EquipData equipData)
        {
            entityComponent.ShowEntity(typeof(Equip), "Equip", Constant.AssetPriority.ArmorAsset, equipData);
        }

        /// <summary>
        /// 使用材质ID
        /// </summary>
        public static int GenerateSerialId(this EntityComponent entityComponent)
        {
            return --s_SerialId;
        }
    }
}