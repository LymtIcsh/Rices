using NPBehave;
using Sirenix.OdinInspector;

namespace Suture
{
    /// <summary>
    /// 此节点包含一个每帧或定时调用委托，开启后，系统会按照一定间隔时间调用委托
    /// </summary>
    public class NP_ServiceNodeData:NP_NodeDataBase
    {
        
        [HideInEditorMode] public Service m_Service;

        [LabelText("委托执行时间间隔")]
        public float interval;

        public NP_ClassForStoreAction NpClassForStoreAction;
        
        public override Node NP_GetNode()
        {
            return this.m_Service;
        }

        public override Decorator CreateDecoratorNode(TargetableObject unit, NP_RuntimeTree runtimeTree, Node node)
        {
            this.NpClassForStoreAction.BelongToUnit = unit;
            this.NpClassForStoreAction.BelongtoRuntimeTree = runtimeTree;
            this.m_Service = new Service(interval,this.NpClassForStoreAction.GetActionToBeDone(),node);
            return this.m_Service;
        }
    }
}