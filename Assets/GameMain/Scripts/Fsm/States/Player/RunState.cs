using GameFramework;
using GameFramework.Fsm;
using UnityEngine;
using UnityGameFramework.Runtime;

namespace Suture
{
    /// <summary>
    /// 走路
    /// </summary>
    public class RunState : AFsmStateBase
    {
        public RunState()
        {
            StateTypes = StateTypes.Run;
            this.StateName = "Run";
            this.Priority = 1;
        }

        public static RunState Create()
        {
            RunState state = ReferencePool.Acquire<RunState>();
            return state;
        }

        protected override void OnInit(IFsm<Pet> fsm)
        {
            base.OnInit(fsm);
        }

        protected override void OnEnter(IFsm<Pet> fsm)
        {
            base.OnEnter(fsm);

            PlayAnimation(this.StateName);
        }

        protected override void OnUpdate(IFsm<Pet> fsm, float elapseSeconds, float realElapseSeconds)
        {
            base.OnUpdate(fsm, elapseSeconds, realElapseSeconds);

            if (_PlayerAssetsInputs.attack)
            {
                ChangeState<NormalAttackState>(fsm);
            }
            
            if (_PlayerAssetsInputs.evade)
            {
                fsm.Owner._evadeEnum = EvadeEnum.Evade_Front;
                ChangeState<EvadeState>(fsm);
            }

            if (_PlayerAssetsInputs.move == Vector2.zero)
            {
                ChangeState<RunEndState>(fsm);
            }
            else
            {
                float cameraAxisY = Camera.main.transform.rotation.eulerAngles.y;
                Vector3 targetDic = Quaternion.Euler(0, cameraAxisY, 0) *
                                    new Vector3(_PlayerAssetsInputs.move.x, 0, _PlayerAssetsInputs.move.y);
                Quaternion tatgetQua = Quaternion.LookRotation(targetDic);

                float angles = Mathf.Abs(tatgetQua.eulerAngles.y - fsm.Owner.transform.eulerAngles.y);
                if (angles > 175.5 && angles < 182.5)
                {
                    ChangeState<TurnBackState>(fsm);
                }

                // if (_PlayerThirdPersonController.IsTuenBack)
                // {
                //     ChangeState<TurnBackState>(fsm);
                //     _PlayerThirdPersonController.IsTuenBack = false;
                // }
            }
        }

        protected override void OnLeave(IFsm<Pet> fsm, bool isShutdown)
        {
            base.OnLeave(fsm, isShutdown);
        }

        protected override void OnDestroy(IFsm<Pet> fsm)
        {
            base.OnDestroy(fsm);
        }
    }
}