//------------------------------------------------------------
// 代码由工具自动生成，请勿手动修改
// 代码由工具自动生成，请勿手动修改
// 代码由工具自动生成，请勿手动修改
//------------------------------------------------------------

using UnityGameFramework.Runtime;

namespace Suture
{
    public class TreatmentBuffSystem : ABuffSystemBase<TreatmentBuffData>
    {
#if SERVER
   public override void OnExecute(uint currentFrame)
        {
            float finalTreatValue;
            finalTreatValue = BuffDataCalculateHelper.CalculateCurrentData(this);

            //TODO:进行相关治疗影响操作，例如减疗，增疗等，应该和伤害计算差不多处理（比如香炉会增加治疗量），这里暂时先只考虑受方
            //TODO:同样的治疗量这一数据也要做一个类似DamageData的数据体
            finalTreatValue =
                this.TheUnitBelongto.GetComponent<DataModifierComponent>().BaptismData("Treat", finalTreatValue);

            this.TheUnitBelongto.GetComponent<UnitAttributesDataComponent>().NumericComponent
                .ApplyChange(NumericType.Hp, finalTreatValue);
            //抛出治疗事件，需要监听治疗的Buff需要监听此事件
            this.GetBuffTarget().BelongToRoom.GetComponent<BattleEventSystemComponent>()
                .Run($"ExcuteTreate{this.TheUnitFrom.Id}", finalTreatValue);
            this.GetBuffTarget().BelongToRoom.GetComponent<BattleEventSystemComponent>()
                .Run($"TakeTreate{this.GetBuffTarget()}", finalTreatValue);
        }
#else
        public override void OnExecute(uint currentFrame)
        {
            float finalTreatValue;
            finalTreatValue = BuffDataCalculateHelper.CalculateCurrentData(this);
            
            //TODO:进行相关治疗影响操作，例如减疗，增疗等，应该和伤害计算差不多处理（比如香炉会增加治疗量），这里暂时先只考虑受方
            //TODO:同样的治疗量这一数据也要做一个类似DamageData的数据体
            finalTreatValue =
                this.TheUnitBelongto.GetComponent<DataModifierComponent>().BaptismData("Treat", finalTreatValue);

            this.TheUnitBelongto.GetComponent<UnitAttributesDataComponent>().NumericComponent
                .ApplyChange(NumericType.Hp, finalTreatValue);
            //抛出治疗事件，需要监听治疗的Buff需要监听此事件
            this.GetBuffTarget().BelongToRoom.GetComponent<BattleEventSystemComponent>()
                .Run($"ExcuteTreate{this.TheUnitFrom.Id}", finalTreatValue);
            this.GetBuffTarget().BelongToRoom.GetComponent<BattleEventSystemComponent>()
                .Run($"TakeTreate{this.GetBuffTarget()}", finalTreatValue);
            
            Log.Info($"治疗buff:   ExcuteTreate{this.TheUnitFrom.Id}     TakeTreate{this.GetBuffTarget()}   最终值：{finalTreatValue}");

        }
#endif
    }
}