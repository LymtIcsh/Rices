using System.Collections.Generic;
using UnityGameFramework.Runtime;

namespace Suture
{
    /// <summary>
    /// NP运行时树管理器
    /// </summary>
    public class NP_RuntimeTreeManager:Entity
    {
        public Dictionary<long, NP_RuntimeTree> RuntimeTrees = new Dictionary<long, NP_RuntimeTree>();

        /// <summary>
        /// 已经添加过的行为树，第一个id为配置id，第二个id为运行时id
        /// </summary>
        public Dictionary<long, long> m_HasAddedTrees = new Dictionary<long, long>();


        /// <summary>
        /// 添加行为树
        /// </summary>
        /// <param name="runTimeID">行为树运行时ID</param>
        /// <param name="rootId">行为树在预配置中的id，即根节点id</param>
        /// <param name="npRuntimeTree">要添加的行为树</param>
        public void AddTree(long runTimeID, long rootId, NP_RuntimeTree npRuntimeTree)
        {
            RuntimeTrees.Add(runTimeID,npRuntimeTree);
            this.m_HasAddedTrees.Add(rootId,runTimeID);
        }


        
        /// <summary>
        /// 通过运行时ID请求行为树
        /// </summary>
        /// <param name="runTimeid">运行时ID</param>
        /// <returns></returns>
        public NP_RuntimeTree GetTreeByRuntimeID(long runTimeid)
        {
            if (RuntimeTrees.ContainsKey(runTimeid))
            {
                return RuntimeTrees[runTimeid];
            }
            
            Log.Error($"通过运行时ID请求行为树请求的ID不存在，id是{runTimeid}");
            return null;
        }

        public void RemoveTree(long id)
        {
            if (RuntimeTrees.ContainsKey(id))
            {
                RuntimeTrees[id].Finish().Forget();
                RuntimeTrees.Remove(id);
            }
        }
        
        public  void Dispose()
        {
            // if(IsDisposed)
            //     return;
            foreach (var runtimeTree in RuntimeTrees)
            {
                runtimeTree.Value.Finish().Forget();
            }
            RuntimeTrees.Clear();
            this.m_HasAddedTrees.Clear();
        }
    }
}