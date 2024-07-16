using Sirenix.OdinInspector;

namespace Suture
{
    /// <summary>
    /// 技能消耗类型
    /// </summary>
    [System.Flags]
    public enum SkillCostTypes
    {
        [LabelText("耗魔")]
        MagicValue = 1 << 1,

        [LabelText("耗血")]
        HPValue = 1 << 2,

        [LabelText("其他")]
        Other = 1 << 3,

        [LabelText("无消耗")]
        None = 1 << 5,
    }
}