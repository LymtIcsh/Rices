using System;
using System.Collections;
using System.Collections.Generic;
using Suture;
using UnityEngine;

public class AnimationComponent : Entity
{
    private Animator _animator;
    
    // /// <summary>
    // /// 栈式状态机组件，用于辅助切换动画
    // /// </summary>
    // private StackFsmComponent _StackFsmComponent;

    private void Awake()
    {
        _animator = GetComponent<Animator>();
        //_StackFsmComponent = GetComponent<StackFsmComponent>();
     //   _StackFsmComponent.ChangeState<IdleState>(StateTypes.Idle, "Idle", 1);
    }

    /// <summary>
    /// 播放动画
    /// </summary>
    /// <param name="animationName">动画名称</param>
    /// <param name="fixedTransitionDuration">过渡时间</param>
    public void PlayAnimation(string animationName, float fixedTransitionDuration = 0.25f)
    {
        _animator.CrossFadeInFixedTime(animationName,fixedTransitionDuration);
    }
}