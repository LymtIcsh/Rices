namespace Suture
{
    /// <summary>
    /// 百分比修改器
    /// </summary>
    public class PercentageModifier:ADataModifier
    {
        /// <summary>
        /// 百分比
        /// </summary>
        public float Percentage;
        
        public override ModifierType ModifierType => ModifierType.Percentage;
        public override float GetModifierValue()
        {
            return Percentage;
        }

        public override void Clear()
        {
           this.Percentage = 0;
        }
    }
}