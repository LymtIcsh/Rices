using GraphProcessor;
using Sirenix.OdinInspector;

namespace Suture
{
    /// <summary>
    /// NP序列节点
    /// </summary>
      [NodeMenuItem("NPBehave行为树/Composite/Sequence",typeof(NPBehaveGraph))]
    // [NodeMenuItem("NPBehave行为树/Composite/Sequence",typeof(SkillGraph))]
    public class NP_SequenceNode:NP_CompositeNodeBase
    {
        public override string name  => "序列结点";

        [BoxGroup("Sequence序列结点")]
        [HideReferenceObjectPicker]
        [HideLabel]
        public NP_SequenceNodeData NP_SequenceNodeData = new NP_SequenceNodeData{ NodeDes = "序列组合器" };

        public override NP_NodeDataBase NP_GetNodeData()
        {
            return NP_SequenceNodeData;
        }
    }
}