using System;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using DG.Tweening;
using Suture.AnimationCore;
using UnityEngine;
using UnityGameFramework.Runtime;

namespace Suture
{
    public class StackAnimationComponent : EntityBase
    {
        private LinkedList<PlayAnimInfo> m_PlayAnimInfo = new LinkedList<PlayAnimInfo>();

        /// <summary>
        /// 当前动画状态
        /// </summary>
        private Animator _animator;

        private AnimatorStateInfo _animatorStateInfo;

        private void Awake()
        {
            _animator = GetComponent<Animator>();
        }

        private void Update()
        {
            _animatorStateInfo = _animator.GetCurrentAnimatorStateInfo(0);
            if (_animator.GetCurrentAnimatorClipInfo(0).Length > 0 &&
                !_animator.GetCurrentAnimatorClipInfo(0)[0].clip.isLooping)
            {
                if (WhetherAnimationFinishedPlaying(GetCurrentFsmState().AnimationClipName))
                {
                    AnimFinished();
                }
            }
        }


        /// <summary>
        ///  获取栈顶状态
        /// </summary>
        /// <returns></returns>
        public PlayAnimInfo GetCurrentFsmState() => this.m_PlayAnimInfo.First?.Value;

        /// <summary>
        /// 检查是否为栈顶状态
        /// </summary>
        /// <param name="aFsmStateBase"></param>
        /// <returns></returns>
        private bool CheckIsFirstState(PlayAnimInfo playAnimInfo) => playAnimInfo == this.GetCurrentFsmState();

        /// <summary>
        /// 添加
        /// </summary>
        /// <param name="playAnimInfo"></param>
        public void AddAnimation(List<PlayAnimInfo> playAnimInfo)
        {
            foreach (var temp in playAnimInfo)
            {
                ChangeAnimation(temp);
            }
        }

        /// <summary>
        /// 切换
        /// </summary>
        /// <param name="playAnimInfo"></param>
        private void ChangeAnimation(PlayAnimInfo playAnimInfo)
        {
            if (this.m_PlayAnimInfo.Count == 0)
            {
                this.m_PlayAnimInfo.AddFirst(playAnimInfo);
                PlayAnimation(
                    playAnimInfo.AnimationClipName,
                    playAnimInfo.FadeOutTime,
                    playAnimInfo.FadeOffsetTime);
                return;
            }


            if (!GetCurrentFsmState().IsStopCurrentRunuing)
            {
                this.m_PlayAnimInfo.Clear();
                this.m_PlayAnimInfo.AddFirst(playAnimInfo);
                PlayAnimation(
                    playAnimInfo.AnimationClipName,
                    playAnimInfo.FadeOutTime,
                    playAnimInfo.FadeOffsetTime);
            }
            else
            {
                //   AnimFinished().Forget();
                this.m_PlayAnimInfo.AddLast(playAnimInfo);
            }
        }

        private void RemoveAnimation(PlayAnimInfo playAnimInfo)
        {
            if (m_PlayAnimInfo.Count > 1)
                this.m_PlayAnimInfo.Remove(playAnimInfo);
        }


        private void AnimFinished()
        {
            Log.Info($"动画{GetCurrentFsmState().AnimationClipName}  播放完成");

            RemoveAnimation(GetCurrentFsmState());
            PlayAnimInfo playAnimInfo = GetCurrentFsmState();

            PlayAnimation(
                playAnimInfo.AnimationClipName,
                playAnimInfo.FadeOutTime,
                playAnimInfo.FadeOffsetTime);
            // }
        }

        private void PlayAnimation(string animationName, float fixedTransitionDuration = 0.25f,
            float fixedTimeOffset = 0f)
        {
            Log.Info($"播放动画{animationName}");
            _animator.CrossFadeInFixedTime(animationName, fixedTransitionDuration, 0, fixedTimeOffset);
            //  AnimFinished().Forget();
        }

        /// <summary>
        /// 动画是否播放完毕
        /// </summary>
        /// <returns></returns>
        private bool WhetherAnimationFinishedPlaying(string clipName)
        {
            // Log.Info(
            //     $"栈顶动画 {GetCurrentFsmState().AnimationClipName}  当前播放的的动画是否是{_animatorStateInfo.IsName(clipName)}  {clipName}");
            return _animatorStateInfo.normalizedTime >= 1.0f && !_animator.IsInTransition(0) &&
                   _animatorStateInfo.IsName(clipName);
        }
    }
}