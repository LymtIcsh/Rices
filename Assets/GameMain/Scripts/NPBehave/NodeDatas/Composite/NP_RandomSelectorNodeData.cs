using NPBehave;
using Sirenix.OdinInspector;

namespace Suture
{
    public class NP_RandomSelectorNodeData:NP_NodeDataBase
    {
        [HideInEditorMode] private RandomSelector m_RandomSelectorNode;

        public override Composite CreateComposite(Node[] nodes)
        {
            this.m_RandomSelectorNode = new RandomSelector(nodes);
            return this.m_RandomSelectorNode;
        }

        public override Node NP_GetNode()
        {
            return this.m_RandomSelectorNode;
        }
    }
}