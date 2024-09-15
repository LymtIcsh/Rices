using GameFramework;
using GameFramework.Fsm;
using UnityEngine;

namespace Suture
{
    public class IdleState : AFsmStateBase
    {
        // /// <summary>
        // /// 互斥的状态，如果当前身上有这些状态，将无法切换至此状态
        // /// </summary>
        //   StateTypes ConflictState =
        //     StateTypes.RePluse | StateTypes.Dizziness | StateTypes.Striketofly | StateTypes.Sneer | StateTypes.Fear;

        public IdleState()
        {
            StateTypes = StateTypes.Idle;
            this.StateName = "Ilde";
            this.Priority = 1;
        }

        public static IdleState Create()
        {
            IdleState state = ReferencePool.Acquire<IdleState>();
            return state;
        }

        protected override void OnInit(IFsm<TargetableObject> fsm)
        {
            base.OnInit(fsm);
        }

        protected override void OnEnter(IFsm<TargetableObject> fsm)
        {
            base.OnEnter(fsm);

            fsm.Owner.  _evadeEnum = EvadeEnum.Evade_Back;
            
            PlayAnimation(this.StateName);
        }

        protected override void OnUpdate(IFsm<TargetableObject> fsm, float elapseSeconds, float realElapseSeconds)
        {
            base.OnUpdate(fsm, elapseSeconds, realElapseSeconds);

            if (_PlayerAssetsInputs.attack)
            {
                ChangeState<NormalAttackState>(fsm);
            }
            
            if (_PlayerAssetsInputs.move != Vector2.zero)
                ChangeState<RunState>(fsm);
            
            if (_PlayerAssetsInputs.evade)
            {
               fsm.Owner. _evadeEnum = EvadeEnum.Evade_Back;
                ChangeState<EvadeState>(fsm);
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