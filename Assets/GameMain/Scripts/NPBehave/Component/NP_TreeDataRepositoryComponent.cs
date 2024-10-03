using System;
using System.Collections.Generic;
using System.IO;
using GameFramework.FileSystem;
using MongoDB.Bson.Serialization;
using UnityEngine;
using UnityGameFramework.Runtime;

namespace Suture
{
    /// <summary>
    /// 行为树数据仓库组件
    /// </summary>
    public class NP_TreeDataRepositoryComponent : GameFrameworkComponent
    {
        public const string NPDataServerPath = "../Config/SkillConfigs/";

        public static readonly string[] SkillConfigNames = new string[]
        {
            "ETreatment",
            "EMushroom",
            "E_Teleport",
            "BossAI"
        };

        /// <summary>
        /// 运行时的行为树仓库，注意，一定不能对这些数据做修改
        /// </summary>
        public Dictionary<long, NP_DataSupportor> NpRuntimeTreesDatas = new Dictionary<long, NP_DataSupportor>();

        protected override void Awake()
        {
            base.Awake();

// #if SERVER
//             DirectoryInfo directory = new DirectoryInfo(NP_TreeDataRepositoryComponent.NPDataServerPath);
//             FileInfo[] fileInfos = directory.GetFiles();
//
//             foreach (var fileInfo in fileInfos)
//             {
//                 try
//                 {
//                     byte[] mfile = File.ReadAllBytes(fileInfo.FullName);
//
//                     if (mfile.Length == 0) Log.Info("没有读取到文件");
//                     NP_DataSupportor MnNpDataSupportor = BsonSerializer.Deserialize<NP_DataSupportor>(mfile);
//
//                     Log.Info($"反序列化行为树：id：{MnNpDataSupportor.NpDataSupportorBase.NPBehaveTreeDataId} {fileInfo.FullName}完成");
//
//                     self.NpRuntimeTreesDatas.Add(MnNpDataSupportor.NpDataSupportorBase.NPBehaveTreeDataId, MnNpDataSupportor);
//                 }
//                 catch (Exception e)
//                 {
//                     Console.WriteLine(e);
//                     throw;
//                 }
//             }
// #else
//             foreach (var skillConfigName in SkillConfigNames)
//             {
//                 // TextAsset textAsset =
//                 //     AssetDatabase.LoadAssetAtPath<TextAsset>(
//                 //       "Assets/GameMain/Configs/SkillConfig/S.bytes");
//
//               //  byte[] mClientfile = File.ReadAllBytes("Assets/GameMain/Configs/SkillConfig/E_Teleport.bytes");
//
//
//               byte[] mClientfile = GameEntry.Resource.LoadBinaryFromFileSystem(skillConfigName);
//                 
//                 if (mClientfile.Length == 0) Log.Info("没有读取到文件");
//                 try
//                 {
//                     NP_DataSupportor MnNpDataSupportor = BsonSerializer.Deserialize<NP_DataSupportor>(mClientfile);
//
//                     Log.Info($"反序列化行为树:{{skillCanvasConfig.Value.SkillConfigName}}完成");
//
//                     NpRuntimeTreesDatas.Add(MnNpDataSupportor.NpDataSupportorBase.NPBehaveTreeDataId, MnNpDataSupportor);
//                 }
//                 catch (Exception e)
//                 {
//                     Log.Error(e);
//                     throw;
//                 }
//             }
// #endif
        }

        public void OnInit()
        {

#if SERVER
            DirectoryInfo directory = new DirectoryInfo(NP_TreeDataRepositoryComponent.NPDataServerPath);
            FileInfo[] fileInfos = directory.GetFiles();

            foreach (var fileInfo in fileInfos)
            {
                try
                {
                    byte[] mfile = File.ReadAllBytes(fileInfo.FullName);

                    if (mfile.Length == 0) Log.Info("没有读取到文件");
                    NP_DataSupportor MnNpDataSupportor = BsonSerializer.Deserialize<NP_DataSupportor>(mfile);

                    Log.Info($"反序列化行为树：id：{MnNpDataSupportor.NpDataSupportorBase.NPBehaveTreeDataId} {fileInfo.FullName}完成");

                    self.NpRuntimeTreesDatas.Add(MnNpDataSupportor.NpDataSupportorBase.NPBehaveTreeDataId, MnNpDataSupportor);
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                    throw;
                }
            }
#else
            foreach (var skillConfigName in SkillConfigNames)
            {
                // TextAsset textAsset =
                //     AssetDatabase.LoadAssetAtPath<TextAsset>(
                //       "Assets/GameMain/Configs/SkillConfig/S.bytes");

                //  byte[] mClientfile = File.ReadAllBytes("Assets/GameMain/Configs/SkillConfig/E_Teleport.bytes");


                byte[] mClientfile =
                    File.ReadAllBytes(AssetUtility.GetConfigAsset($"SkillConfig/{skillConfigName}", true));

                if (mClientfile.Length == 0) Log.Info("没有读取到文件");
                try
                {
                    NP_DataSupportor MnNpDataSupportor = BsonSerializer.Deserialize<NP_DataSupportor>(mClientfile);

                    Log.Info($"反序列化行为树:{{skillCanvasConfig.Value.SkillConfigName}}完成");

                    NpRuntimeTreesDatas.Add(MnNpDataSupportor.NpDataSupportorBase.NPBehaveTreeDataId,
                        MnNpDataSupportor);
                }
                catch (Exception e)
                {
                    Log.Error(e);
                    throw;
                }
            }
#endif
        }

        /// <summary>
        /// 获取一棵树的所有数据（默认形式）
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public NP_DataSupportor GetNP_TreeData(long id)
        {
            if (NpRuntimeTreesDatas.ContainsKey(id))
            {
                return NpRuntimeTreesDatas[id];
            }

            Log.Error($"请求的行为树id不存在，id为{id}");
            return null;
        }


        /// <summary>
        /// 获取一棵树的所有数据（仅深拷贝黑板数据内容，而忽略例如BuffNodeDataDic的数据内容）
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public NP_DataSupportor GetNP_TreeData_DeepCopyBBValuesOnly(long id)
        {
            NP_DataSupportor result = new NP_DataSupportor();
            if (NpRuntimeTreesDatas.ContainsKey(id))
            {
                result.BuffNodeDataDic = NpRuntimeTreesDatas[id].BuffNodeDataDic;
                result.NpDataSupportorBase = NpRuntimeTreesDatas[id].NpDataSupportorBase.DeepCopy();
                return result;
            }

            Log.Error($"请求的行为树id不存在，id为{id}");
            return null;
        }

        /// <summary>
        /// 获取一棵树的所有数据（通过深拷贝形式）
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public NP_DataSupportor GetNP_TreeData_DeepCopy(long id)
        {
            if (NpRuntimeTreesDatas.ContainsKey(id))
            {
                return NpRuntimeTreesDatas[id].DeepCopy();
            }

            Log.Error($"请求的行为树id不存在，id为{id}");
            return null;
        }
    }
}