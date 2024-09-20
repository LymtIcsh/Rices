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
            Pet targetableObject = numericComponent.GetComponent<Pet>();

            targetableObject._petData.m_unitAttributesNodeDataBase.OriHP += (int)value;
            
            Log.Info($"HP 改变:{value}");
        }
    }
}