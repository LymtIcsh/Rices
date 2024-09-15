using GameFramework;
using GameFramework.Fsm;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityGameFramework.Runtime;

namespace Suture
{
    public abstract class AFsmStateBase : FsmState<TargetableObject>, IReference
    {
        /// <summary>
        /// 互斥的状态，如果当前身上有这些状态，将无法切换至此状态
        /// </summary>
        [LabelText("互斥的状态")] protected StateTypes ConflictState =
            StateTypes.RePluse | StateTypes.Dizziness | StateTypes.Striketofly | StateTypes.Sneer | StateTypes.Fear;


        /// <summary>
        /// 状态类型
        /// </summary>
        [LabelText("状态类型")] public StateTypes StateTypes;

        /// <summary>
        /// 状态名称
        /// </summary>
        [LabelText("状态名称")] public string StateName;

        /// <summary>
        /// 状态的优先级，值越大，优先级越高。
        /// </summary>
        [LabelText("状态的优先级")] public int Priority;

        protected Animator _animator;
        protected AnimatorStateInfo _animatorStateInfo;

        protected PlayerAssetsInputs _PlayerAssetsInputs;
        protected PlayerThirdPersonController _PlayerThirdPersonController;

        public AFsmStateBase()
        {
        }

        public void SetData(StateTypes stateTypes, string stateName, int priority)
        {
            StateTypes = stateTypes;
            StateName = stateName;
            this.Priority = priority;
        }


        protected override void OnInit(IFsm<TargetableObject> fsm)
        {
            base.OnInit(fsm);

            _animator = fsm.Owner.GetComponent<Animator>();
            _PlayerAssetsInputs = fsm.Owner.GetComponent<PlayerAssetsInputs>();
            _PlayerThirdPersonController = fsm.Owner.GetComponent<PlayerThirdPersonController>();
        }

        protected override void OnEnter(IFsm<TargetableObject> fsm)
        {
            base.OnEnter(fsm);
        }

        protected override void OnUpdate(IFsm<TargetableObject> fsm, float elapseSeconds, float realElapseSeconds)
        {
            
            base.OnUpdate(fsm, elapseSeconds, realElapseSeconds);

            _animatorStateInfo = _animator.GetCurrentAnimatorStateInfo(0);
        }

        protected override void OnLeave(IFsm<TargetableObject> fsm, bool isShutdown)
        {
            base.OnLeave(fsm, isShutdown);
        }

        protected override void OnDestroy(IFsm<TargetableObject> fsm)
        {
            base.OnDestroy(fsm);
        }

        public void Clear()
        {
        }

        #region 动画

        #region 动画播放

        /// <summary>
        /// 播放动画
        /// </summary>
        /// <param name="animationName">动画名称</param>
        /// <param name="fixedTransitionDuration">过渡时间</param>
        public void PlayAnimation(string animationName, float fixedTransitionDuration = 0.25f,
            float fixedTimeOffset = 0f)
        {
            _animator.CrossFadeInFixedTime(animationName, fixedTransitionDuration, 0, fixedTimeOffset);
        }

        /// <summary>
        /// 动画是否播放完毕
        /// </summary>
        /// <returns></returns>
        protected bool WhetherAnimationFinishedPlaying(string clipName)
        {
           // Log.Info(_animatorStateInfo.IsName(clipName)+"      "+clipName);
           return _animatorStateInfo.normalizedTime >= 1.0f && !_animator.IsInTransition(0)&&_animatorStateInfo.IsName(clipName);
        }
           

        // /// <summary>
        // /// 左脚急刹
        // /// </summary>
        // public void SetLeftRunEnd() => _RunEnd = RunEnd.Left;

        // /// <summary>
        // /// 右脚急刹
        // /// </summary>
        // public void SetRightRunEnd() => _RunEnd = RunEnd.Right;

        #endregion

        #endregion
    }
}