using NPBehave;
using Sirenix.OdinInspector;
using Unity.VisualScripting;

namespace Suture
{
    /// <summary>
    /// 重复执行结点数据
    /// </summary>
    public class NP_RepeaterNodeData:NP_NodeDataBase
    {
        [HideInEditorMode]
        public Repeater m_Repeater;
        
        public override Node NP_GetNode()
        {
            return this.m_Repeater;
        }

        public override Decorator CreateDecoratorNode(TargetableObject unit, NP_RuntimeTree runtimeTree, Node node)
        {
            this.m_Repeater = new Repeater(node);
            return this.m_Repeater;
        }
    }
}