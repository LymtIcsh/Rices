//------------------------------------------------------------
// 代码由工具自动生成，请勿手动修改
// 代码由工具自动生成，请勿手动修改
// 代码由工具自动生成，请勿手动修改
//------------------------------------------------------------
using GameFramework;
using GameFramework.Fsm;

namespace Suture
{
    /// <summary>
    /// 普通攻击后摇
    /// </summary>
    public class NormalAttackEndState:AFsmStateBase
    {
        private HeroAttributesNodeData _HeroAttributesNodeData;
        public NormalAttackEndState()
        {
            StateTypes = StateTypes.CommonAttack;
            this.StateName = "Attack_Normal_End_0";
            this.Priority = 1;
        }
        
   public static NormalAttackEndState Create()
        {
           NormalAttackEndState state = ReferencePool.Acquire<NormalAttackEndState>();
            return state;
        }

        protected override void OnInit(IFsm<TargetableObject> fsm)
        {
            base.OnInit(fsm);
            
            _HeroAttributesNodeData = (fsm.Owner as Pet)._petData.m_unitAttributesNodeDataBase;
            
        }

        protected override void OnEnter(IFsm<TargetableObject> fsm)
        {
            base.OnEnter(fsm);
            
            PlayAnimation(this.StateName + _HeroAttributesNodeData.CurrentNormalAttackIndex);
        }

        protected override void OnUpdate(IFsm<TargetableObject> fsm, float elapseSeconds, float realElapseSeconds)
        {
            base.OnUpdate(fsm, elapseSeconds, realElapseSeconds);

            if (_PlayerAssetsInputs.attack)
            {
                ChangeState<NormalAttackState>(fsm);
            }

            if (WhetherAnimationFinishedPlaying(this.StateName+ _HeroAttributesNodeData.CurrentNormalAttackIndex))
            {
                ChangeState<IdleState>(fsm);
                _HeroAttributesNodeData.CurrentNormalAttackIndex = 1;
            }
        }

        protected override void OnLeave(IFsm<TargetableObject> fsm, bool isShutdown)
        {
            //TODO 此处 ++自增 无效
            //更新连击段数
            _HeroAttributesNodeData.CurrentNormalAttackIndex = _HeroAttributesNodeData.CurrentNormalAttackIndex + 1 >
                                                               _HeroAttributesNodeData.NormalAttackHarm.Length
                ? 1
                : _HeroAttributesNodeData.CurrentNormalAttackIndex+1;
            
            base.OnLeave(fsm, isShutdown);
        }

        protected override void OnDestroy(IFsm<TargetableObject> fsm)
        {
            base.OnDestroy(fsm);
        }
        
    }
}
