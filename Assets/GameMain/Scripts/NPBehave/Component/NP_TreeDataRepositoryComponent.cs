using System.Collections.Generic;
using UnityGameFramework.Runtime;

namespace Suture
{
    /// <summary>
    /// 行为树数据仓库组件
    /// </summary>
    public class NP_TreeDataRepositoryComponent
    {
        public const string NPDataServerPath = "../Config/SkillConfigs/";

        /// <summary>
        /// 运行时的行为树仓库，注意，一定不能对这些数据做修改
        /// </summary>
        public Dictionary<long, NP_DataSupportor> NpRuntimeTreesDatas = new Dictionary<long, NP_DataSupportor>();

        
        /// <summary>
        /// 获取一棵树的所有数据（默认形式）
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public  NP_DataSupportor GetNP_TreeData(long id)
        {
            if (NpRuntimeTreesDatas.ContainsKey(id))
            {
                return NpRuntimeTreesDatas[id];
            }

            Log.Error($"请求的行为树id不存在，id为{id}");
            return null;
        }
    }
}