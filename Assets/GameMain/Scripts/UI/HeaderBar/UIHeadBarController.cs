using UnityGameFramework.Runtime;

namespace Suture.HeaderBar
{
    [NumericWatcher(NumericType.MaxHp)]
    public class ChangeHPBar_Density:INumericWatcher
    {
        public void Run(NumericComponent numericComponent, NumericType numericType, float value)
        {
            
        }
    }
    
    [NumericWatcher(NumericType.Hp)]
    public class ChangeHPValue:INumericWatcher
    {
        public void Run(NumericComponent numericComponent, NumericType numericType, float value)
        {
            TargetableObject targetableObject = numericComponent.GetComponent<TargetableObject>();

            targetableObject.m_TargetableObjectData.m_unitAttributesNodeDataBase.OriHP += (int)value;
            
            Log.Info($"HP 改变:{value}");
        }
    }
}