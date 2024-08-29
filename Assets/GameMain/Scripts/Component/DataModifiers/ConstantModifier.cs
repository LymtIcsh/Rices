namespace Suture
{
    /// <summary>
    /// 常数修改器
    /// </summary>
    public class ConstantModifier:ADataModifier
    {
        /// <summary>
        /// 修改的值
        /// </summary>
        public float ChangeValue;
        
        public override ModifierType ModifierType => ModifierType.Constant;
        
        public override float GetModifierValue()
        {
            return ChangeValue;
        }

        public override void Clear()
        {
            this.ChangeValue = 0;
        }
    }
}