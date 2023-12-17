using GraphProcessor;
using UnityEngine;

namespace Suture
{
    /// <summary>
    /// NP装饰节点库
    /// </summary>
    public class NP_DecoratorNodeBase:NP_NodeBase
    {
        [Input("NPBehave_PreNode"),Vertical]
        [HideInInspector]
        public NP_NodeBase PrevNode;

        [Output("NPBehave_NextNode"),Vertical]
        [HideInInspector]
        public NP_NodeBase NextNode;
    }
}