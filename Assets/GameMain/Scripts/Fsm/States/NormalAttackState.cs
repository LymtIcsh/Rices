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
    /// 普通攻击状态
    /// </summary>
    public class NormalAttackState : AFsmStateBase
    {
        private HeroAttributesNodeData _HeroAttributesNodeData;

        public NormalAttackState()
        {
             StateTypes = StateTypes.CommonAttack;
            this.StateName = "Attack_Normal_0";
            this.Priority = 1;
        }

        public static NormalAttackState Create()
        {
            NormalAttackState state = ReferencePool.Acquire<NormalAttackState>();
            return state;
        }

        protected override void OnInit(IFsm<TargetableObject> fsm)
        {
            base.OnInit(fsm);

            _HeroAttributesNodeData = fsm.Owner.m_TargetableObjectData.m_unitAttributesNodeDataBase;
        }

        protected override void OnEnter(IFsm<TargetableObject> fsm)
        {
            base.OnEnter(fsm);
            _PlayerAssetsInputs.attack = false;
            
            PlayAnimation(this.StateName + _HeroAttributesNodeData.CurrentNormalAttackIndex);
        }

        protected override void OnUpdate(IFsm<TargetableObject> fsm, float elapseSeconds, float realElapseSeconds)
        {
            base.OnUpdate(fsm, elapseSeconds, realElapseSeconds);

            if (WhetherAnimationFinishedPlaying(this.StateName+ _HeroAttributesNodeData.CurrentNormalAttackIndex))
            {
                ChangeState<NormalAttackEndState>(fsm);
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