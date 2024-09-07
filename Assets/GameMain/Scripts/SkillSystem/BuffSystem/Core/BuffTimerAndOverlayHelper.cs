﻿using UnityGameFramework.Runtime;

namespace Suture
{
    public static class BuffTimerAndOverlayHelper
    {
        /// <summary>
        /// 为Buff计算时间和层数
        /// </summary>
        /// <param name="buffSystemBase"></param>
        /// /// <param name="currentFrame"></param>
        /// <param name="layer">为 -1 时需要计算层数，否则直接强制应用层数</param>
        /// <returns>是否成功加入了运行时Buff列表</returns>
        public static bool CalculateTimerAndOverlay(IBuffSystem buffSystemBase, uint currentFrame, int layer = -1)
        {
            BuffManagerComponent buffManagerComponent =
                buffSystemBase.GetBuffTarget().GetComponent<BuffManagerComponent>();

            //先尝试从Buff链表取得Buff
            IBuffSystem targetBuffSystemBase = buffManagerComponent.GetBuffById(buffSystemBase.BuffData.BuffId);

            if (targetBuffSystemBase != null)
            {
                CalculateTimerAndOverlayHelper(targetBuffSystemBase, currentFrame,layer);
                //刷新当前已有的Buff
                targetBuffSystemBase.Refresh(currentFrame);

                return false;
            }
            else
            {
                CalculateTimerAndOverlayHelper(buffSystemBase, currentFrame,layer);

                Log.Info($"本次新加BuffID为{buffSystemBase.BuffData.BuffId}");
                buffSystemBase.BuffState = BuffState.Waiting;
                buffManagerComponent.AddBuff(buffSystemBase);

                return true;
            }
        }

        /// <summary>
        /// 计算刷新的持续时间和层数
        /// </summary>
        private static void CalculateTimerAndOverlayHelper(IBuffSystem buffSystemBase, uint currentFrame, 
            int layer = -1)
        {
            //可以叠加，并且当前层数加上要添加Buff的目标层数未达到最高层
            if (buffSystemBase.BuffData.CanOverlay)
            {
                if (layer != -1)
                {
                    buffSystemBase.CurrentOverlay = layer;
                }
                else
                {
                    if (buffSystemBase.CurrentOverlay + buffSystemBase.BuffData.TargetOverlay <=
                        buffSystemBase.BuffData.MaxOverlay)
                    {
                        buffSystemBase.CurrentOverlay += buffSystemBase.BuffData.TargetOverlay;
                    }
                    else
                    {
                        buffSystemBase.CurrentOverlay = buffSystemBase.BuffData.MaxOverlay;
                    }
                }
            }
            else
            {
                buffSystemBase.CurrentOverlay = 1;
            }

            //如果是有限时长的 TODO:这里考虑处理持续时间和Buff层数挂钩的情况（比如磕了5瓶药，就是5*单瓶药的持续时间）
            if (buffSystemBase.BuffData.SustainTime + 1 > 0)
            {
              //  buffSystemBase.MaxLimitFrame = (uint)buffSystemBase.BuffData.SustainTime;
                Log.Info(
                    $"原本结束时间：{buffSystemBase.MaxLimitFrame},续命前的时间{currentFrame} 续命时长{TimeAndFrameConverter.Frame_Long2Frame(buffSystemBase.BuffData.SustainTime)} 续命之后的结束时间{currentFrame+ TimeAndFrameConverter.Frame_Long2Frame(buffSystemBase.BuffData.SustainTime)}");
              
                buffSystemBase.MaxLimitFrame = currentFrame +
                                               TimeAndFrameConverter.Frame_Long2Frame(buffSystemBase
                                                   .BuffData.SustainTime);
            }
        }
    }
}