//------------------------------------------------------------
// 代码由工具自动生成，请勿手动修改
// 代码由工具自动生成，请勿手动修改
// 代码由工具自动生成，请勿手动修改
//------------------------------------------------------------

using UnityGameFramework.Runtime;

namespace Suture
{
 /// <summary>
    /// 刷新某个或某几个Buff的持续时间
    /// </summary>
    public class RefreshTargetBuffTimeBuffSystem: ABuffSystemBase<RefreshTargetBuffTimeBuffData>
    {
    #if SERVER
    
    
        #else
                public override void OnExecute(uint currentFrame)
                {
                    BuffManagerComponent buffManagerComponent =
                        this.GetBuffTarget().GetComponent<BuffManagerComponent>();

                    foreach (var buffNodeId in this.GetBuffDataWithTType.TheBuffNodeIdToBeRefreshed)
                    {
                        Log.Info($"准备刷新指定Buff——{buffNodeId}持续时间");
                        IBuffSystem aBuffSystemBase = buffManagerComponent
                            .GetBuffById(
                                (this.BelongtoRuntimeTree.BelongNP_DataSupportor.BuffNodeDataDic[buffNodeId.Value] as
                                    NormalBuffNodeData).BuffData.BuffId);

                        if (aBuffSystemBase!=null&&aBuffSystemBase.BuffData.SustainTime+1>0)
                        {
                             Log.Info(
                               $"刷新了指定Buff——{buffNodeId}持续时间{TimeInfo.Instance.ClientFrameTime() + aBuffSystemBase.BuffData.SustainTime},原本为{aBuffSystemBase.MaxLimitFrame}");
                            
                            aBuffSystemBase.MaxLimitFrame=currentFrame+TimeAndFrameConverter.Frame_Long2Frame(aBuffSystemBase.BuffData.SustainTime);
                        }
                    }
                }
        #endif
    }
}
