//此文件格式由工具自动生成

using System;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using Sirenix.OdinInspector;
using Suture.AnimationCore;
using UnityEditor.Animations;
using UnityEngine;
using UnityGameFramework.Runtime;

namespace Suture
{
    [Title("播放动画", TitleAlignment = TitleAlignments.Centered)]
    public class NP_PlayAnimationAction : NP_ClassForStoreAction
    {
        [LabelText("要播放的动画数据")] public List<PlayAnimInfo> NodeDataForPlayAnims = new List<PlayAnimInfo>();

        // <summary>
        /// 用于标识当前播放到哪一个动画的flag
        /// </summary>
        private int m_Flag = 0;


        /// <summary>
        /// 当前动画状态
        /// </summary>
        private Animator _animator;

        private AnimatorStateInfo _animatorStateInfo;


        public override Action GetActionToBeDone()
        {
#if !SERVER
            m_Flag = 0;

#endif
            this.Action = this.PlayAnimation;
            return this.Action;
        }

        public void PlayAnimation()
        {
                this.BelongToUnit.GetComponent<StackAnimationComponent>().AddAnimation(NodeDataForPlayAnims);
            

//             _animator = this.BelongToUnit.GetComponent<Animator>();
//             _animatorStateInfo = _animator.GetCurrentAnimatorStateInfo(0);
//
// #if !SERVER
//             // TODO 说明上次动画节点的动画尚未播放完毕，所以就忽略这次重复的播放，这块逻辑应当放在Timeline中处理
//             if (m_Flag != 0)
//             {
//                 return;
//             }
//
//             m_Flag = 0;
//
//             PlayAnimation(
//                 NodeDataForPlayAnims[this.m_Flag].AnimationClipName, NodeDataForPlayAnims[this.m_Flag].FadeOutTime,
//                 NodeDataForPlayAnims[this.m_Flag].FadeOffsetTime);
//           
//             if (NodeDataForPlayAnims.Count > 1)
//             {
//                 OnAnimFinished().Forget();
//             }
//             else
//             {
//                 while (WhetherAnimationFinishedPlaying(NodeDataForPlayAnims[this.m_Flag].AnimationClipName))
//                 {
//                     
//                 }
//             }
//             // HandlePlayAnim(NodeDataForPlayAnims[this.m_Flag].StateTypes,
//             //     NodeDataForPlayAnims[this.m_Flag].OccAvatarMaskType, NodeDataForPlayAnims[this.m_Flag].FadeOutTime);
//             //Log.Info("这次播放的是Q技能动画");
// #endif
        }

        public async UniTaskVoid OnAnimFinished()
        {
            for (int i = 1; i < NodeDataForPlayAnims.Count; i++)
            {
                await AnimFinished();
                m_Flag++;
                PlayAnimation(
                    NodeDataForPlayAnims[this.m_Flag].AnimationClipName,
                    NodeDataForPlayAnims[this.m_Flag].FadeOutTime,
                    NodeDataForPlayAnims[this.m_Flag].FadeOffsetTime);
            }
        }

        public async UniTask AnimFinished()
        {
            while (true)
            {
                _animatorStateInfo = _animator.GetCurrentAnimatorStateInfo(0);
                await UniTask.WaitForSeconds(0.1f);
                if (WhetherAnimationFinishedPlaying(NodeDataForPlayAnims[this.m_Flag].AnimationClipName))
                {
                    return;
                }
            }
        }

        public void PlayAnimation(string animationName, float fixedTransitionDuration = 0.25f,
            float fixedTimeOffset = 0f)
        {
            Log.Info($"播放动画{animationName}");
            _animator.CrossFadeInFixedTime(animationName, fixedTransitionDuration, 0, fixedTimeOffset);
        }

        /// <summary>
        /// 动画是否播放完毕
        /// </summary>
        /// <returns></returns>
        private bool WhetherAnimationFinishedPlaying(string clipName)
        {
            // Log.Info(_animatorStateInfo.IsName(clipName)+"      "+clipName);
            return _animatorStateInfo.normalizedTime >= 1.0f && !_animator.IsInTransition(0) &&
                   _animatorStateInfo.IsName(clipName);
        }
    }
}