using NPBehave;
using Sirenix.OdinInspector;

namespace Suture
{
    [BoxGroup("行为结点数据")]
    [HideLabel]
    public class NP_ActionNodeData:NP_NodeDataBase
    {
        [HideInEditorMode] private Action m_ActionNode;

        public NP_ClassForStoreAction NpClassForStoreAction;

        public override Task CreateTask(Unit unit, NP_RuntimeTree runtimeTree)
        {
            this.NpClassForStoreAction.BelongToUnit = unit;
            this.NpClassForStoreAction.BelongtoRuntimeTree = runtimeTree;
            this.m_ActionNode = this.NpClassForStoreAction._CreateNPBehaveAction();
            return this.m_ActionNode;
        }

        public override Node NP_GetNode()
        {
            return this.m_ActionNode;
        }
    }
}