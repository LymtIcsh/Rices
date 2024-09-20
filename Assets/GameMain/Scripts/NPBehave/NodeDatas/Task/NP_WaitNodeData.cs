using NPBehave;
using Sirenix.OdinInspector;

namespace Suture
{
    [BoxGroup("等待结点数据")]
    [HideLabel]
    public class NP_WaitNodeData:NP_NodeDataBase
    {
        [HideInEditorMode]  private Wait m_WaitNode;

        public NP_BlackBoardRelationData NPBalckBoardRelationData = new NP_BlackBoardRelationData();

        public override Task CreateTask(Pet unit, NP_RuntimeTree runtimeTree)
        {
            this.m_WaitNode = new Wait(this.NPBalckBoardRelationData.BBkey);
            return this.m_WaitNode;
        }

        public override Node NP_GetNode()
        {
            return this.m_WaitNode;
        }
    }
}