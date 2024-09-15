//------------------------------------------------------------
// 代码由工具自动生成，请勿手动修改
// 代码由工具自动生成，请勿手动修改
// 代码由工具自动生成，请勿手动修改
//------------------------------------------------------------
namespace Suture
{
    /// <summary>
    /// 起跳玩落地状态
    /// </summary>
    public class JumpLandState:AFsmStateBase
    {
        // /// <summary>
        // /// 互斥的状态，如果当前身上有这些状态，将无法切换至此状态
        // /// </summary>
        //   StateTypes ConflictState =
        //     StateTypes.RePluse | StateTypes.Dizziness | StateTypes.Striketofly | StateTypes.Sneer | StateTypes.Fear;

        public JumpLandState()
        {
            StateTypes = StateTypes.JumpLand;
            this.StateName = "JumpLand";
            this.Priority = 1;
        }
        
        // public override StateTypes GetConflictStateTypeses()
        // {
        //     return ConflictState;
        // }

        // public override void OnEnter(StackFsmComponent stackFsmComponent)
        // {
        //     stackFsmComponent.PlayAnimation(  this.StateName );
        // }
        //
        // public override void OnUpdate(StackFsmComponent stackFsmComponent)
        // {
        //     if (stackFsmComponent._animatorStateInfo.normalizedTime>=1.0f&&!stackFsmComponent._animator.IsInTransition(0))
        //     {
        //         stackFsmComponent.ChangeState(new IdleState());
        //     }
        // }
        //
        // public override void OnExit(StackFsmComponent stackFsmComponent)
        // {
        //     stackFsmComponent.RemoveState(StateTypes.JumpLand);
        // }
        //
        // public override void OnRemoved(StackFsmComponent stackFsmComponent)
        // {
        //  
        // }
    }
}
