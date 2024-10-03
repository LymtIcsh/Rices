using NPBehave;
using Sirenix.OdinInspector;
using Unity.VisualScripting;

namespace Suture
{
    /// <summary>
    /// 重复执行结点数据
    /// </summary>
    public class NP_RepeaterNodeData : NP_NodeDataBase
    {
        [HideInEditorMode] public Repeater m_Repeater;

        [LabelText("是否关闭无限循环")] public bool NoLoop ;
        
        [ShowIf("NoLoop")][LabelText("循环次数，-1为无限")] public int loopCount = -1;

        public override Node NP_GetNode()
        {
            return this.m_Repeater;
        }

        public override Decorator CreateDecoratorNode(TargetableObject unit, NP_RuntimeTree runtimeTree, Node node)
        {
            this.m_Repeater = new Repeater(loopCount, node);
            return this.m_Repeater;
        }
    }
}