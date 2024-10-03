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
    [NodeMenuItem("NPBehave行为树/Composite/随机序列节点", typeof(SkillGraph))]
    [NodeMenuItem("NPBehave行为树/Composite/随机序列节点", typeof(NPBehaveGraph))]
    public class NP_RandomSequenceNode : NP_CompositeNodeBase
    {
        /// <summary>
        /// 内部ID
        /// </summary>
        public override string name => "随机序列节点";

        [BoxGroup("RandomSelector结点数据")] [HideReferenceObjectPicker] [HideLabel]
        public NP_RandomSequenceNodeData NP_ActionNodeData =
            new NP_RandomSequenceNodeData() { NodeDes = "随机序列组合器" };

        public override NP_NodeDataBase NP_GetNodeData()
        {
            return NP_ActionNodeData;
        }
    }
}