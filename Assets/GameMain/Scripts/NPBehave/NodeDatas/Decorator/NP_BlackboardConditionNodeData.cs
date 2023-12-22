using NPBehave;
using Sirenix.OdinInspector;

namespace Suture
{
    [BoxGroup("黑板条件节点配置"), GUIColor(0.961f, 0.902f, 0.788f, 1f)]
    [HideLabel]
    public class NP_BlackboardConditionNodeData:NP_NodeDataBase
    {
        [HideInEditorMode]
        private BlackboardCondition m_BlackboardConditionNode;

        [LabelText("运算符号")]
        public Operator Ope = Operator.IS_EQUAL;

        [LabelText("终止条件")]
        public Stops Stop = Stops.IMMEDIATE_RESTART;
        
        public NP_BlackBoardRelationData
        
        public override Node NP_GetNode()
        {
            throw new System.NotImplementedException();
        }
    }
}