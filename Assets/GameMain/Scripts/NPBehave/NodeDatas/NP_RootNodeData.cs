using NPBehave;
using Sirenix.OdinInspector;
using Unity.VisualScripting;

namespace Suture
{
    public class NP_RootNodeData : NP_NodeDataBase
    {
        [HideInEditorMode] public Root m_Root;

        public override Decorator CreateDecoratorNode(TargetableObject unit, NP_RuntimeTree runtimeTree, Node node)
        {
            //这里 GetClock 用通过 UnityContext.GetClock(); 烟雨大佬是 NpSyncComponent.SyncContext.GetClock();
            this.m_Root = new Root(node, runtimeTree.GetClock());
            return this.m_Root;
        }

        public override Node NP_GetNode()
        {
            return this.m_Root;
        }
    }
}
