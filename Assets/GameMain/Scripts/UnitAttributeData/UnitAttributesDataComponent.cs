//------------------------------------------------------------
// Author: 烟雨迷离半世殇
// Mail: 1778139321@qq.com
// Data: 2019年8月14日 21:37:53
//------------------------------------------------------------

using UnityGameFramework.Runtime;

namespace Suture
{
    /// <summary>
    /// 英雄数据组件，负责管理英雄数据
    /// </summary>
    public class UnitAttributesDataComponent: Entity
    {
        public UnitAttributesNodeDataBase UnitAttributesNodeDataBase;



        public T GetAttributeDataAs<T>() where T : UnitAttributesNodeDataBase
        {
            return UnitAttributesNodeDataBase as T;
        }

        protected override void OnDetached(EntityLogic childEntity, object userData)
        {
            base.OnDetached(childEntity, userData);
            this.UnitAttributesNodeDataBase = null;
        }
    }
}