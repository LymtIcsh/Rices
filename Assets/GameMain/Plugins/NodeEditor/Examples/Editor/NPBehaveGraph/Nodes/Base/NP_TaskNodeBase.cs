using GraphProcessor;
using UnityEngine;

namespace Suture
{
    /// <summary>
    /// NP任务节点库
    /// </summary>
    public abstract class NP_TaskNodeBase:NP_NodeBase
    {
        [Input("NPBehave_PreNode"),Vertical]
        [HideInInspector]
        public NP_NodeBase PrevNode;
    }
}