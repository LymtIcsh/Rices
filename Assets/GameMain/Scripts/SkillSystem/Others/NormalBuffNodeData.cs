using Sirenix.OdinInspector;

namespace Suture
{
    [Title("Buff节点数据块",TitleAlignment = TitleAlignments.Centered)]
    [HideLabel]
    public class NormalBuffNodeData:BuffNodeDataBase
    {
        [LabelText("Buff描述")]
        // [BsonIgnore]
        public string BuffDes;
        
        /// <summary>
        /// Buff数据
        /// </summary>
        public BuffDataBase BuffData;
    }
}