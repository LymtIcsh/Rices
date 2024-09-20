using NPBehave;
using Sirenix.OdinInspector;

namespace Suture
{
    [BoxGroup("等待到停止节点数据")]
    [HideLabel]
    public class NP_WaitUntilStoppedData:NP_NodeDataBase
    {
        [HideInEditorMode] private WaitUntilStopped m_WaitUntilStopped;

        public override Task CreateTask(Pet unit, NP_RuntimeTree runtimeTree)
        {
            this.m_WaitUntilStopped = new WaitUntilStopped();
            return this.m_WaitUntilStopped;
        }

        public override Node NP_GetNode()
        {
            return this.m_WaitUntilStopped;
        }
    }
}