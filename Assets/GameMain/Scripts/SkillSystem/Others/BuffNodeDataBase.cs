using Sirenix.OdinInspector;

namespace Suture
{
    /// <summary>
    /// Buff节点数据库
    /// </summary>
    public class BuffNodeDataBase
    {
        [LabelText("节点Id")]
        [BoxGroup, GUIColor(1, 140 / 255f, 0)]
        public VTD_Id NodeId;
    }
}