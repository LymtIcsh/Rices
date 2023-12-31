﻿

using System;
using Cysharp.Threading.Tasks;
using NPBehave;
using UnityGameFramework.Runtime;

namespace Suture
{
    /// <summary>
    /// NP运行时树
    /// </summary>
    public class NP_RuntimeTree :Entity
    {
        /// <summary>
        /// NP行为树根结点
        /// </summary>
        private Root m_RootNode;

        /// <summary>
        /// 所归属的数据块
        /// </summary>
        public NP_DataSupportor BelongNP_DataSupportor;
        
        /// <summary>
        /// 所归属的Unit
        /// </summary>
        public Unit BelongToUnit;
        
        
        public Clock GetClock()
        {
            return UnityContext.GetClock();
        }
        
        /// <summary>
        /// 开始运行行为树
        /// </summary>
        public void Start()
        {
            this.m_RootNode.Start();
        }

        /// <summary>
        /// 设置根结点
        /// </summary>
        /// <param name="rootNode"></param>
        public void SetRootNode(Root rootNode)
        {
            this.m_RootNode = rootNode;
        }

        /// <summary>
        /// 获取黑板
        /// </summary>
        /// <returns></returns>
        public Blackboard GetBlackboard()
        {
            if (m_RootNode==null)
            {
                Log.Error($"行为树{this.Id}的根节点为空");
            }

            if (m_RootNode.Blackboard==null)
            {
                Log.Error($"行为树{this.Id}的黑板实例为空");
            }

            return this.m_RootNode.Blackboard;
        }

        /// <summary>
        /// 终止行为树
        /// </summary>
        public async UniTaskVoid Finish()
        {
            await UniTask.Delay(TimeSpan.FromMilliseconds(1), ignoreTimeScale: false);
            
            this.m_RootNode.CancelWithoutReturnResult();
            BelongToUnit = null;
            this.m_RootNode = null;
            this.BelongNP_DataSupportor = null;
        }
    }
}