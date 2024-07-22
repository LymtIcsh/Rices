//------------------------------------------------------------
// Author: 烟雨迷离半世殇
// Mail: 1778139321@qq.com
// Data: 2021年6月18日 20:19:14
//------------------------------------------------------------


using GraphProcessor;

namespace Suture
{
    public class UnitAttributesNodeBase: BaseNode
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