using GameFramework.DataTable;
using System;
using GameFramework.Entity;
using UnityEngine;
using UnityEngine.EventSystems;
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


        /// <summary>
        /// 显示实体。
        /// </summary>
        /// <param name="entityGroup">实体组名称</param>
        /// <param name="ArmorAsset">加载实体资源的优先级。 【从 Constant.AssetPriority.ArmorAsset 】 </param>
        /// <param name="entityData">用户自定义实体数据。</param>
        /// <typeparam name="T">实体逻辑类型。</typeparam>
        public static void ShowEntity<T>(this EntityComponent entityComponent,string entityGroup,int ArmorAsset, EntityData entityData)
        {
            entityComponent.ShowEntity(typeof(T), entityGroup, ArmorAsset, entityData);
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
        /// 创建实体。
        /// </summary>
        /// <param name="entityName">实体名字。</param>
        /// <param name="entityGroupName">实体所属的实体组。</param>
        /// <param name="userData">用户自定义数据。</param>
        /// <returns>实体。</returns>
        public static T CreateEntity<T>(this EntityComponent entityComponent,string entityName, string entityGroupName, object userData) where T : Entity
        {
            GameObject gameObject = new GameObject();
            if (gameObject == null)
            {
                Log.Error("Entity instance is invalid.");
                return null;
            }

            gameObject.name = entityName;

            Transform transform = gameObject.transform;
            IEntityGroup entityGroup = GameEntry.Entity.GetEntityGroup(entityGroupName);
            transform.SetParent(((MonoBehaviour)entityGroup.Helper).transform);

            return gameObject.GetOrAddComponent<T>();
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