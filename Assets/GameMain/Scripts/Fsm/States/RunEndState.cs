//------------------------------------------------------------
// 代码由工具自动生成，请勿手动修改
// 代码由工具自动生成，请勿手动修改
// 代码由工具自动生成，请勿手动修改
//------------------------------------------------------------

using GameFramework;
using GameFramework.Fsm;
using UnityEngine;

namespace Suture
{
    /// <summary>
    /// 急刹
    /// </summary>
    public class RunEndState : AFsmStateBase
    {
        // /// <summary>
        // /// 互斥的状态，如果当前身上有这些状态，将无法切换至此状态
        // /// </summary>
        //   StateTypes ConflictState =
        //     StateTypes.RePluse | StateTypes.Dizziness | StateTypes.Striketofly | StateTypes.Sneer | StateTypes.Fear;


        public RunEndState()
        {
            StateTypes = StateTypes.RunEnd;
            this.StateName = "Run_End";
            this.Priority = 1;
        }

        public static RunEndState Create()
        {
            RunEndState state = ReferencePool.Acquire<RunEndState>();
            return state;
        }

        // public override StateTypes GetConflictStateTypeses()
        // {
        //     return ConflictState;
        // }

        protected override void OnInit(IFsm<TargetableObject> fsm)
        {
            base.OnInit(fsm);
        }

        protected override void OnEnter(IFsm<TargetableObject> fsm)
        {
            base.OnEnter(fsm);

     
            PlayAnimation(this.StateName);
        }

        protected override void OnUpdate(IFsm<TargetableObject> fsm, float elapseSeconds, float realElapseSeconds)
        {
            base.OnUpdate(fsm, elapseSeconds, realElapseSeconds);
            
            if (_PlayerAssetsInputs.attack)
            {
                ChangeState<NormalAttackState>(fsm);
            }
            
            if (_PlayerAssetsInputs.evade)
            {
                fsm.Owner.   _evadeEnum = EvadeEnum.Evade_Back;
                ChangeState<EvadeState>(fsm);
            }
            
            if ( _PlayerAssetsInputs.move != Vector2.zero)
            {
                ChangeState<RunState>(fsm);
            }

            if (WhetherAnimationFinishedPlaying(this.StateName) )
            {
                ChangeState<IdleState>(fsm);
            }
            

        }

        protected override void OnLeave(IFsm<TargetableObject> fsm, bool isShutdown)
        {
            base.OnLeave(fsm, isShutdown);
        }

        protected override void OnDestroy(IFsm<TargetableObject> fsm)
        {
            base.OnDestroy(fsm);
        }
    }
}