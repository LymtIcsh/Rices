//------------------------------------------------------------
// 代码由工具自动生成，请勿手动修改
// 代码由工具自动生成，请勿手动修改
// 代码由工具自动生成，请勿手动修改
//------------------------------------------------------------


using GraphProcessor;
using Plugins.NodeEditor;
using Sirenix.OdinInspector;

namespace Suture
{
    [NodeMenuItem("NPBehave行为树/Composite/随机选择节点", typeof(SkillGraph))]
    [NodeMenuItem("NPBehave行为树/Composite/随机选择节点", typeof(NPBehaveGraph))]
    public class NP_RandomSelectorNode : NP_CompositeNodeBase
    {
        /// <summary>
        /// 内部ID
        /// </summary>
        public override string name => "随机选择";
        
        [BoxGroup("RandomSelector结点数据")] [HideReferenceObjectPicker] [HideLabel]
        public NP_RandomSelectorNodeData NP_ActionNodeData =
                new NP_RandomSelectorNodeData() {  NodeDes = "随机选择组合器"};

        public override NP_NodeDataBase NP_GetNodeData()
        {
            return NP_ActionNodeData;
        }
    }
}
