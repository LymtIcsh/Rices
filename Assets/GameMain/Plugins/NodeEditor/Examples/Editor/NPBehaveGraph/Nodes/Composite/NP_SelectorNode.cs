using GraphProcessor;
using Sirenix.OdinInspector;

namespace Suture
{
    /// <summary>
    /// NP选择节点
    /// </summary>
    [NodeMenuItem("NPBehave行为树/Composite/Selector",typeof(NPBehaveGraph))]
    [NodeMenuItem("NPBehave行为树/Composite/Selector",typeof(SkillGraph))]
    public class NP_SelectorNode:NP_CompositeNodeBase
    {
        public override string name => "选择结点";

        [BoxGroup("Selector选择结点数据")]
        [HideReferenceObjectPicker]
        [HideLabel]
        public NP_SelectorNodeData NP_SelectorNodeData = new NP_SelectorNodeData{ NodeDes = "选择组合器" };

        public override NP_NodeDataBase NP_GetNodeData()
        {
            return NP_SelectorNodeData;
        }
    }
}