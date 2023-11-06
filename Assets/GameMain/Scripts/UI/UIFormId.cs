
namespace Suture
{
    /// <summary>
    /// 界面编号
    /// </summary>
    public enum UIFormId : byte
    {
        Undefined = 0,

        /// <summary>
        /// 弹出框。
        /// </summary>
        DialogForm = 1,

        /// <summary>
        /// 主菜单。
        /// </summary>
        MenuForm = 100,

        /// <summary>
        /// 设置。
        /// </summary>
        SettingForm = 101,

        /// <summary>
        /// 模式选择
        /// </summary>
        UIPatternSelectForm = 102,

        /// <summary>
        /// 宠物选择
        /// </summary>
        UIPetSelectForm = 103
    }
}

