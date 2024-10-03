//------------------------------------------------------------
// 代码由工具自动生成，请勿手动修改
// 代码由工具自动生成，请勿手动修改
// 代码由工具自动生成，请勿手动修改
//------------------------------------------------------------


using GraphProcessor;
using Plugins.NodeEditor;

namespace Suture
{
    [NodeMenuItem("NPBehave行为树/Task/WaitUntilStopped", typeof(SkillGraph))]
    [NodeMenuItem("NPBehave行为树/Task/WaitUntilStopped", typeof(NPBehaveGraph))]
    public class NP_WaitUntilStoppedDataNode : NP_TaskNodeBase
    {
        /// <summary>
        /// 内部ID
        /// </summary>
        public override string name => "一直等待，直到Stopped";
        
        public NP_WaitUntilStoppedData NP_ActionNodeData =
                new NP_WaitUntilStoppedData() { };

        public override NP_NodeDataBase NP_GetNodeData()
        {
            return NP_ActionNodeData;
        }
    }
}
