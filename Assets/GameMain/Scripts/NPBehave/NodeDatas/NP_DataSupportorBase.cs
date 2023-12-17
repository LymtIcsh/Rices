using System.Collections.Generic;
using Sirenix.OdinInspector;

namespace Suture
{
    /// <summary>
    /// NP数据支持库
    /// </summary>
    public class NP_DataSupportorBase
    {
        [LabelText("此行为树Id，也是根节点Id")]
        public long NPBehaveTreeDataId;

        public Dictionary<long, NP_NodeDataBase> NP_DataSupportorDic = new Dictionary<long, NP_NodeDataBase>();
        
        [LabelText("黑板数据")]
        public Dictionary<string, ANP_BBValue> NP_BBValueManager = new Dictionary<string, ANP_BBValue>();
    }
}