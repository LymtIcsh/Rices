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
    /// <summary>
    /// 此节点包含一个每帧或定时调用委托，开启后，系统会按照一定间隔时间调用委托
    /// </summary>
    [NodeMenuItem("NPBehave行为树/Decorator/Service", typeof(SkillGraph))]
    [NodeMenuItem("NPBehave行为树/Decorator/Service", typeof(NPBehaveGraph))]
    public class NP_ServiceNodeDataNode : NP_DecoratorNodeBase
    {
        /// <summary>
        /// 内部ID
        /// </summary>
        public override string name => "服务结点";
        
        [BoxGroup("服务结点数据")]
        [HideReferenceObjectPicker]
        [HideLabel]
        public NP_ServiceNodeData NP_ActionNodeData =
                new NP_ServiceNodeData() { };

        public override NP_NodeDataBase NP_GetNodeData()
        {
            return NP_ActionNodeData;
        }
    }
}
