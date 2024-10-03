//------------------------------------------------------------
// 代码由工具自动生成，请勿手动修改
// 代码由工具自动生成，请勿手动修改
// 代码由工具自动生成，请勿手动修改
//------------------------------------------------------------


using GraphProcessor;
using Plugins.NodeEditor;

namespace Suture
{
    [NodeMenuItem("NPBehave行为树/Task/离开检测范围", typeof(SkillGraph))]
    [NodeMenuItem("NPBehave行为树/Task/离开检测范围", typeof(NPBehaveGraph))]
    public class NP_ExitTriggerRadiusActionNode : NP_TaskNodeBase
    {
        /// <summary>
        /// 内部ID
        /// </summary>
        public override string name => "离开检测范围";
        
        public NP_ActionNodeData NP_ActionNodeData =
                new NP_ActionNodeData() { NpClassForStoreAction = new NP_ExitTriggerRadiusAction() };

        public override NP_NodeDataBase NP_GetNodeData()
        {
            return NP_ActionNodeData;
        }
    }
}
