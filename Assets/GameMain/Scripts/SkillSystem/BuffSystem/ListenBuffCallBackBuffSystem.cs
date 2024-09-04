//------------------------------------------------------------
// 代码由工具自动生成，请勿手动修改
// 代码由工具自动生成，请勿手动修改
// 代码由工具自动生成，请勿手动修改
//------------------------------------------------------------

using GameFramework;
using UnityGameFramework.Runtime;

namespace Suture
{
    /// <summary>
    /// 监听Buff回调
    /// </summary>
    public class ListenBuffCallBackBuffSystem : ABuffSystemBase<ListenBuffCallBackBuffData>
    {
        public ListenBuffEvent_Normal ListenBuffEventNormal;


        public override void OnExecute(uint currentFrame)
        {
            //是否需要判断buff层数
            if (GetBuffDataWithTType.HasOverlayerJudge)
            {
                ListenBuffEventNormal = ReferencePool.Acquire<ListenBuffEvent_CheckOverlay>();
                ListenBuffEventNormal.BuffInfoWillBeAdded = GetBuffDataWithTType.BuffInfoWillBeAdded;
                var listenOverLayer = ListenBuffEventNormal as ListenBuffEvent_CheckOverlay;
                listenOverLayer.TargetOverlay = GetBuffDataWithTType.TargetOverLayer;
            }
            else
            {
                ListenBuffEventNormal = ReferencePool.Acquire<ListenBuffEvent_Normal>();
                ListenBuffEventNormal.BuffInfoWillBeAdded = GetBuffDataWithTType.BuffInfoWillBeAdded;
            }

            this.GetBuffTarget().BelongToRoom.GetComponent<BattleEventSystemComponent>()
                .RegisterEvent($"{this.GetBuffDataWithTType.EventId.Value}{this.TheUnitFrom.Id}",
                    ListenBuffEventNormal);
            
            Log.Info($"监听{this.GetBuffDataWithTType.EventId.Value}{this.TheUnitFrom.Id}");
        }

        public override void OnFinished(uint currentFrame)
        {
            this.GetBuffTarget().BelongToRoom.GetComponent<BattleEventSystemComponent>()
                .UnRegisterEvent($"{this.GetBuffDataWithTType.EventId.Value}{this.TheUnitFrom.Id}",
                    ListenBuffEventNormal);
        }
    }
}