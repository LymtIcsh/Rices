using System.Collections.Generic;
using Sirenix.OdinInspector;

namespace Suture
{
    /// <summary>
    /// 单位属性数据支持
    /// </summary>
    public class UnitAttributesDataSupportor
    {
        [LabelText("此数据载体ID")]
        public int SupportId;


        public Dictionary<long, UnitAttributesNodeDataBase> UnitAttributesDataSupportorDic =
            new Dictionary<long, UnitAttributesNodeDataBase>();
    }
}