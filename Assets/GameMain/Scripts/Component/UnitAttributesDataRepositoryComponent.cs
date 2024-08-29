using System.Collections.Generic;
using System.IO;
using GameFramework.Resource;
using MongoDB.Bson.Serialization;
using UnityEditor;
using UnityEngine;
using UnityGameFramework.Runtime;

namespace Suture
{
    public class UnitAttributesDataRepositoryComponent : GameFrameworkComponent
    {
        public Dictionary<long, UnitAttributesDataSupportor> AllUnitAttributesBaseDataDic =
            new Dictionary<long, UnitAttributesDataSupportor>();


        protected override void Awake()
        {
            base.Awake();
// #if !SERVER
            //TODO 这里是所有英雄的属性数据，后续需要拓展小兵，野怪属性

            byte[] mClientfile = File.ReadAllBytes("Assets/GameMain/Configs/UnitAttributesDatas/AllHeroAttributesData.bytes");

            if (mClientfile.Length == 0) Log.Info("没有读取到文件");


            UnitAttributesDataSupportor unitAttributesDataSupportor =
                BsonSerializer.Deserialize<UnitAttributesDataSupportor>(mClientfile);
            AllUnitAttributesBaseDataDic[unitAttributesDataSupportor.SupportId] = unitAttributesDataSupportor;


// #else
//             DirectoryInfo directoryInfo = new DirectoryInfo("../Config/UnitAttributesDatas/");
//             foreach (var unitAttributesDataConfigFile in directoryInfo.GetFiles())
//             {
//                 byte[] mfile = File.ReadAllBytes(unitAttributesDataConfigFile.FullName);
//                 UnitAttributesDataSupportor unitAttributesDataSupportor =
//                     BsonSerializer.Deserialize<UnitAttributesDataSupportor>(mfile);
//                 self.AllUnitAttributesBaseDataDic[unitAttributesDataSupportor.SupportId] = unitAttributesDataSupportor;
//             }
// #endif
        }

        // <summary>
        /// 根据id来获取指定Unit属性数据(通过深拷贝的形式获得，对数据的更改不会影响到原本的数据)
        /// </summary>
        /// <param name="dataSupportId">数据载体Id</param>
        /// <param name="nodeDataId">数据载体中的节点Id</param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public T GetUnitAttributesDataById_DeepCopy<T>(long dataSupportId, long nodeDataId)
            where T : UnitAttributesNodeDataBase
        {
            if (AllUnitAttributesBaseDataDic.TryGetValue(dataSupportId, out var unitAttributesDataSupportor))
            {
                if (unitAttributesDataSupportor.UnitAttributesDataSupportorDic.TryGetValue(nodeDataId,
                        out var unitAttributesNodeDataBase))
                {
                    return unitAttributesNodeDataBase.DeepCopy() as T;
                }
            }

            Log.Error($"查询Unit属性数据失败，数据载体Id为{dataSupportId}，数据载体中的节点Id为{nodeDataId}");
            return null;
        }
    }
}