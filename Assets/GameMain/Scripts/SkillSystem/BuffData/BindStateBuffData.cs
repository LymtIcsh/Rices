using System.Collections.Generic;
using Sirenix.OdinInspector;

namespace Suture
{
    public class BindStateBuffData:BuffDataBase
    {
        //TODO BindStateBuffSystem 脚本里 处理buff状态 
        [BoxGroup("自定义项")]
        [HideReferenceObjectPicker]
        [LabelText("此状态自带的状态数据")]
        public CustomState OriState;
        
        [BoxGroup("自定义项")]
        [InfoBox("注意，是在节点编辑器中的Buff节点Id，而不是Buff自身的Id，别搞错了！")]
        [LabelText("此状态自带的Buff节点Id")]
        public List<VTD_BuffInfo> OriBuff = new List<VTD_BuffInfo>();
    }
}