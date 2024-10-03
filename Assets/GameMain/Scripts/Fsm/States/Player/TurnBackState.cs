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
    /// 180度转身状态
    /// </summary>
    public class TurnBackState : AFsmStateBase
    {
        public TurnBackState()
        {
            StateTypes = StateTypes.TurnBack;
            this.StateName = "TurnBack";
            this.Priority = 1;
        }

        public static TurnBackState Create()
        {
            TurnBackState state = ReferencePool.Acquire<TurnBackState>();
            return state;
        }

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
                (fsm.Owner as Pet)._evadeEnum = EvadeEnum.Evade_Back;
                ChangeState<EvadeState>(fsm);
            }

            if (WhetherAnimationFinishedPlaying(this.StateName) && _PlayerAssetsInputs.move != Vector2.zero)
            {
                ChangeState<RunState>(fsm);
            }
            else
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