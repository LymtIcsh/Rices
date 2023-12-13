using GraphProcessor;

namespace Suture
{
    /// <summary>
    /// 
    /// </summary>
    public class UnitAttributesNodeBase:BaseNode
    {
        /// <summary>
        /// 节点重命名开启
        /// </summary>
        public override bool isRenamable => true;

        /// <summary>
        /// 单位属性数据获取节点数据
        /// </summary>
        /// <returns></returns>
        public virtual UnitAttributesNodeDataBase UnitAttributesData_GetNodeData()
        {
            return null;
        }
    }
}