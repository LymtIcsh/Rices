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
    public enum EvadeEnum
    {
        /// <summary>
        /// 前闪
        /// </summary>
        Evade_Front,
        
        /// <summary>
        /// 后闪
        /// </summary>
        Evade_Back
    }
    
    /// <summary>
    /// 闪避状态
    /// </summary>
    public class EvadeState:AFsmStateBase
    {
 
        public EvadeState()
        {
            StateTypes = StateTypes.Evade;
            this.StateName = "Evade_Front";
            this.Priority = 1;
        }
        
   public static EvadeState Create()
        {
           EvadeState state = ReferencePool.Acquire<EvadeState>();
            return state;
        }

        protected override void OnInit(IFsm<Pet> fsm)
        {
            base.OnInit(fsm);
        }

        protected override void OnEnter(IFsm<Pet> fsm)
        {
            base.OnEnter(fsm);

            _PlayerAssetsInputs.evade = false;
            
            this.StateName =fsm.Owner._evadeEnum == EvadeEnum.Evade_Front ? "Evade_Front" : "Evade_Back";
           
            PlayAnimation(  this.StateName );
        }

        protected override void OnUpdate(IFsm<Pet> fsm, float elapseSeconds, float realElapseSeconds)
        {
            base.OnUpdate(fsm, elapseSeconds, realElapseSeconds);     
            
            // if ( _PlayerAssetsInputs.move != Vector2.zero)
            // {
            //     ChangeState<RunState>(fsm);
            // }
            if (_PlayerAssetsInputs.attack)
            {
                ChangeState<NormalAttackState>(fsm);
            }

            if (WhetherAnimationFinishedPlaying(this.StateName) )
            {
                ChangeState<IdleState>(fsm);
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
