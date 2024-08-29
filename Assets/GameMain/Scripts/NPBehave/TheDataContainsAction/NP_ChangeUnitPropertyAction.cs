//此文件格式由工具自动生成
using System;
using Sirenix.OdinInspector;
using UnityGameFramework.Runtime;

namespace Suture
{
    [Title("改变Unit属性",TitleAlignment = TitleAlignments.Centered)]
    public class NP_ChangeUnitPropertyAction:NP_ClassForStoreAction
    {        
        public NP_BlackBoardRelationData NPBalckBoardRelationData = new NP_BlackBoardRelationData();

        [LabelText("要更改的Unit属性为")]
        public BuffWorkTypes BuffWorkTypes;
        
        public override Action GetActionToBeDone()
        {
            this.Action = this.ChangeUnitProperty;
            return this.Action;
        }

        public void ChangeUnitProperty()
        {
            TargetableObject unit = BelongToUnit;
         //   UnitAttributesDataComponent unitAttributesDataComponent = unit.GetComponent<UnitAttributesDataComponent>();
            DataModifierComponent dataModifierComponent = unit.GetComponent<DataModifierComponent>();
            float oriValue, finalValue;

            switch (BuffWorkTypes)
            {
             case   BuffWorkTypes.ChangeMagic:
                 oriValue = this.BelongtoRuntimeTree.GetBlackboard().Get<float>(this.NPBalckBoardRelationData.BBkey);
                 finalValue = dataModifierComponent.BaptismData("CostMP", oriValue);
                 
                 unit.GetComponent<NumericComponent>()[NumericType.Hp] += finalValue;
                 // Log.Info(
                 //     $"减少了蓝：{((float) UnitComponent.Instance.Get(this.Unitid).GetComponent<NP_RuntimeTreeManager>().GetTreeByRuntimeID(this.RuntimeTreeID).GetBlackboard()[m_NPBalckBoardRelationData.DicKey]).ToString()}");
                 break;
             case BuffWorkTypes.ChangeHP:
                 oriValue = this.BelongtoRuntimeTree.GetBlackboard().Get<float>(this.NPBalckBoardRelationData.BBkey);
                 finalValue = dataModifierComponent.BaptismData("CostHP", oriValue);
                 unit.GetComponent<NumericComponent>()[NumericType.Hp] += finalValue;
                 break;
            }
        }
    }
}
