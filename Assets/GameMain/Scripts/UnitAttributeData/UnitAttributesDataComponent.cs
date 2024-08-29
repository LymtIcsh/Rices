//------------------------------------------------------------
// Author: 烟雨迷离半世殇
// Mail: 1778139321@qq.com
// Data: 2019年8月14日 21:37:53
//------------------------------------------------------------

using System;
using UnityGameFramework.Runtime;

namespace Suture
{
    /// <summary>
    /// 英雄数据组件，负责管理英雄数据
    /// </summary>
    public class UnitAttributesDataComponent : Entity
    {
        public UnitAttributesNodeDataBase UnitAttributesNodeDataBase;

        public NumericComponent NumericComponent;

        private void Awake()
        {
            
            UnitAttributesNodeDataBase = GameEntry.UnitAttributesDataRepository
                .GetUnitAttributesDataById_DeepCopy<HeroAttributesNodeData>(10002, GetComponent<MyPet>().Id);
        
            NumericComponent = GetComponent<NumericComponent>();
            
            NumericComponent.SetValueWithoutBroadCast(NumericType.Level, 1);
            NumericComponent.SetValueWithoutBroadCast(NumericType.MaxLevel, 18);
           NumericComponent.SetValueWithoutBroadCast(NumericType.MaxHpBase, UnitAttributesNodeDataBase
                .OriHP);
           NumericComponent.SetValueWithoutBroadCast(NumericType.MaxHpAdd, UnitAttributesNodeDataBase
                .GroHP);
            NumericComponent.SetValueWithoutBroadCast(NumericType.MaxHp,
               UnitAttributesNodeDataBase.OriHP +UnitAttributesNodeDataBase.GroHP);
            NumericComponent.SetValueWithoutBroadCast(NumericType.Hp,
                UnitAttributesNodeDataBase.OriHP + UnitAttributesNodeDataBase.GroHP);

           NumericComponent.SetValueWithoutBroadCast(NumericType.MaxMpBase,
                UnitAttributesNodeDataBase.OriMagicValue);
            NumericComponent.SetValueWithoutBroadCast(NumericType.MaxMpAdd,
                UnitAttributesNodeDataBase.GroMagicValue);
           NumericComponent.SetValueWithoutBroadCast(NumericType.MaxMp,
                UnitAttributesNodeDataBase.OriMagicValue + UnitAttributesNodeDataBase.GroMagicValue);
            NumericComponent.SetValueWithoutBroadCast(NumericType.Mp,
                UnitAttributesNodeDataBase.OriMagicValue + UnitAttributesNodeDataBase.GroMagicValue);

          NumericComponent.SetValueWithoutBroadCast(NumericType.AttackBase,
              UnitAttributesNodeDataBase.OriAttackValue);
            NumericComponent.SetValueWithoutBroadCast(NumericType.AttackAdd,
              UnitAttributesNodeDataBase.GroAttackValue);
           NumericComponent.SetValueWithoutBroadCast(NumericType.Attack,
                UnitAttributesNodeDataBase.OriAttackValue + UnitAttributesNodeDataBase.GroAttackValue);

           NumericComponent.SetValueWithoutBroadCast(NumericType.SpeedBase,
                UnitAttributesNodeDataBase.OriMoveSpeed);
           NumericComponent.SetValueWithoutBroadCast(NumericType.SpeedAdd,
             UnitAttributesNodeDataBase.GroMoveSpeed);
            NumericComponent.SetValueWithoutBroadCast(NumericType.Speed,
                UnitAttributesNodeDataBase.OriMoveSpeed + UnitAttributesNodeDataBase.GroMoveSpeed);

            NumericComponent.SetValueWithoutBroadCast(NumericType.ArmorBase,
              UnitAttributesNodeDataBase.OriArmor);
         NumericComponent.SetValueWithoutBroadCast(NumericType.ArmorAdd,
             UnitAttributesNodeDataBase.GroArmor);
          NumericComponent.SetValueWithoutBroadCast(NumericType.Armor,
               UnitAttributesNodeDataBase.OriArmor + UnitAttributesNodeDataBase.GroArmor);

            NumericComponent.SetValueWithoutBroadCast(NumericType.MagicResistanceBase,
               UnitAttributesNodeDataBase.OriMagicResistance);
            NumericComponent.SetValueWithoutBroadCast(NumericType.MagicResistanceAdd,
               UnitAttributesNodeDataBase.GroMagicResistance);
           NumericComponent.SetValueWithoutBroadCast(NumericType.MagicResistance,
                UnitAttributesNodeDataBase.OriMagicResistance +
               UnitAttributesNodeDataBase.GroMagicResistance);

          NumericComponent.SetValueWithoutBroadCast(NumericType.HPRecBase,
               UnitAttributesNodeDataBase.OriHPRec);
           NumericComponent.SetValueWithoutBroadCast(NumericType.HPRecAdd,
               UnitAttributesNodeDataBase.GroHPRec);
        NumericComponent.SetValueWithoutBroadCast(NumericType.HPRec,
                UnitAttributesNodeDataBase.OriHPRec + UnitAttributesNodeDataBase.GroHPRec);

         NumericComponent.SetValueWithoutBroadCast(NumericType.MPRecBase,
              UnitAttributesNodeDataBase.OriMagicRec);
          NumericComponent.SetValueWithoutBroadCast(NumericType.MPRecAdd,
              UnitAttributesNodeDataBase.GroMagicRec);
          NumericComponent.SetValueWithoutBroadCast(NumericType.MPRec,
               UnitAttributesNodeDataBase.OriMagicRec + UnitAttributesNodeDataBase.GroMagicRec);

           NumericComponent.SetValueWithoutBroadCast(NumericType.AttackSpeedBase,
                UnitAttributesNodeDataBase.OriAttackSpeed);
            NumericComponent.SetValueWithoutBroadCast(NumericType.AttackSpeedAdd,
               UnitAttributesNodeDataBase.GroAttackSpeed);
           NumericComponent.SetValueWithoutBroadCast(NumericType.AttackSpeed,
                UnitAttributesNodeDataBase.OriAttackSpeed +UnitAttributesNodeDataBase.GroAttackSpeed);

           NumericComponent.SetValueWithoutBroadCast(NumericType.AttackSpeedIncome,
                UnitAttributesNodeDataBase.OriAttackIncome);

           NumericComponent.SetValueWithoutBroadCast(NumericType.CriticalStrikeProbability,
                UnitAttributesNodeDataBase.OriCriticalStrikeProbability);

           NumericComponent.SetValueWithoutBroadCast(NumericType.CriticalStrikeHarm,
                UnitAttributesNodeDataBase.OriCriticalStrikeHarm);

            //法术穿透
            NumericComponent.SetValueWithoutBroadCast(NumericType.MagicPenetrationBase, 0);
            NumericComponent.SetValueWithoutBroadCast(NumericType.MagicPenetrationAdd, 0);

           NumericComponent.SetValueWithoutBroadCast(NumericType.AttackRangeBase,
              UnitAttributesNodeDataBase.OriAttackRange);
            NumericComponent.SetValueWithoutBroadCast(NumericType.AttackRangeAdd, 0);
          NumericComponent.SetValueWithoutBroadCast(NumericType.AttackRange,
                UnitAttributesNodeDataBase.OriAttackRange);

           NumericComponent.InitOriNumerDic();
        }

        public T GetAttributeDataAs<T>() where T : UnitAttributesNodeDataBase
        {
            return UnitAttributesNodeDataBase as T;
        }
        
        public float GetAttribute(NumericType numericType)
        {
            return NumericComponent[numericType];
        }

        protected override void OnDetached(EntityLogic childEntity, object userData)
        {
            base.OnDetached(childEntity, userData);
            this.UnitAttributesNodeDataBase = null;
            NumericComponent = null;
        }
    }
}