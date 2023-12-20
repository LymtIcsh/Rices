using System;
using Sirenix.OdinInspector;

namespace Suture
{
    /// <summary>
    /// Buff的添加目标
    /// </summary>
    [Flags]   //上面附上Flags特性后，用该枚举变量是既可以象整数一样进行按位的“|”或者按位的“&”操作了。
    public enum BuffTargetTypes
    {
        /// <summary>
        /// 自己
        /// </summary>
        [LabelText("自己")] Self = 1 << 1,

        /// <summary>
        /// 别人
        /// </summary>
        [LabelText("别人")]  Others = 1 << 2,
    }
}