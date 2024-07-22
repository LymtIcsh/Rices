//------------------------------------------------------------
// 代码由工具自动生成，请勿手动修改
// 代码由工具自动生成，请勿手动修改
// 代码由工具自动生成，请勿手动修改
//------------------------------------------------------------

using System.Collections.Generic;
using Suture;
using Sirenix.OdinInspector;
using UnityEditor;
using GraphProcessor;
using UnityEngine;
using Node = NPBehave.Node;

namespace Suture
{
    [NodeMenuItem("NPBehave行为树/Task/添加BuffAction", typeof(SkillGraph))]
    [NodeMenuItem("NPBehave行为树/Task/添加BuffAction", typeof(NPBehaveGraph))]
    public class NP_AddBuffActionNode : NP_TaskNodeBase
    {
        /// <summary>
        /// 内部ID
        /// </summary>
        public override string name => "添加BuffAction";
        
        public NP_ActionNodeData NP_ActionNodeData =
                new NP_ActionNodeData() { NpClassForStoreAction = new NP_AddBuffAction() };

        public override NP_NodeDataBase NP_GetNodeData()
        {
            return NP_ActionNodeData;
        }
    }
}
