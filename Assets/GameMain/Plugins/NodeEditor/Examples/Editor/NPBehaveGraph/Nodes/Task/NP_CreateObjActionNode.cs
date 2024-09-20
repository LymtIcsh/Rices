//------------------------------------------------------------
// 代码由工具自动生成，请勿手动修改
// 代码由工具自动生成，请勿手动修改
// 代码由工具自动生成，请勿手动修改
//------------------------------------------------------------


using GraphProcessor;
using Plugins.NodeEditor;

namespace Suture
{
    [NodeMenuItem("NPBehave行为树/Task/创建物体", typeof(SkillGraph))]
    [NodeMenuItem("NPBehave行为树/Task/创建物体", typeof(NPBehaveGraph))]
    public class NP_CreateObjActionNode : NP_TaskNodeBase
    {
        /// <summary>
        /// 内部ID
        /// </summary>
        public override string name => "创建物体";
        
        public NP_ActionNodeData NP_ActionNodeData =
                new NP_ActionNodeData() { NpClassForStoreAction = new NP_CreateObjAction() };

        public override NP_NodeDataBase NP_GetNodeData()
        {
            return NP_ActionNodeData;
        }
    }
}
