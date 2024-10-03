using NPBehave;
using Sirenix.OdinInspector;

namespace Suture
{
    public class NP_RandomSequenceNodeData:NP_NodeDataBase
    {
        [HideInEditorMode]
        private RandomSequence m_RandomSequenceNode;

        public override Composite CreateComposite(Node[] nodes)
        {
            this.m_RandomSequenceNode = new RandomSequence(nodes);
            return this.m_RandomSequenceNode;
        }

        public override Node NP_GetNode()
        {
            return this.m_RandomSequenceNode;
        }
    }
}