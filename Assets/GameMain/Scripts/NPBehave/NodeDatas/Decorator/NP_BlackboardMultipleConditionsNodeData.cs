using System.Collections.Generic;
using NPBehave;
using Sirenix.OdinInspector;
using Unity.VisualScripting;

namespace Suture
{
    /// <summary>
    /// 黑板多条件节点数据
    /// </summary>
    [BoxGroup("黑板多条件节点配置"), GUIColor(0.961f, 0.902f, 0.788f, 1f)]
    [HideLabel]
    public class NP_BlackboardMultipleConditionsNodeData:NP_NodeDataBase
    {

        private BlackboardMultipleConditions m_BlackboardMultipleConditions;
        
        [LabelText("对比内容")]
        public List<MatchInfo> MatchInfos = new List<MatchInfo>();

        [LabelText("逻辑类型")]
        public MatchType MatchType;

        [LabelText("终止条件")]
        public Stops Stop = Stops.IMMEDIATE_RESTART;

        public override Decorator CreateDecoratorNode(Unit unit, NP_RuntimeTree runtimeTree, Node node)
        {
            this.m_BlackboardMultipleConditions =
                new BlackboardMultipleConditions(this.MatchInfos, this.MatchType, this.Stop, node);

            return this.m_BlackboardMultipleConditions;
        }

        public override Node NP_GetNode()
        {
            return this.m_BlackboardMultipleConditions;
        }
    }
}