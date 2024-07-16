using Sirenix.OdinInspector;

namespace Suture
{
    /// <summary>
    /// 技能指示器类型
    /// </summary>
    public enum SkillReleaseMode
    {
        /// <summary>
        /// 无
        /// </summary>
        [LabelText("无")] None = 0,

        /// <summary>
        /// 圆环指示器
        /// </summary>
        [LabelText("圆环指示器")] ARange = 1,

        /// <summary>
        /// 箭头指示器
        /// </summary>
        [LabelText("箭头指示器")] AArrow = 2,

        /// <summary>
        /// 使用鼠标选择
        /// </summary>
        [LabelText("使用鼠标选择")] ATarget = 3,

        /// <summary>
        /// 使用鼠标选择
        /// </summary>
        [LabelText("扇形指示器")] Sector = 4,
    }
}