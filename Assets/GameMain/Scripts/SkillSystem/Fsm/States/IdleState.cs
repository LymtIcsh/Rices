namespace Suture
{
    public class IdleState:AFsmStateBase
    {
        /// <summary>
        /// 互斥的状态，如果当前身上有这些状态，将无法切换至此状态
        /// </summary>
        public static StateTypes ConflictState =
            StateTypes.RePluse | StateTypes.Dizziness | StateTypes.Striketofly | StateTypes.Sneer | StateTypes.Fear;

        public IdleState()
        {
            StateTypes = StateTypes.Idle;
            this.StateName = "Anim_Idle1";
            this.Priority = 1;
        }
        
        public override StateTypes GetConflictStateTypeses()
        {
            return ConflictState;
        }

        public override void OnEnter(StackFsmComponent stackFsmComponent)
        {
           
        }

        public override void OnExit(StackFsmComponent stackFsmComponent)
        {
       
        }

        public override void OnRemoved(StackFsmComponent stackFsmComponent)
        {
         
        }
    }
}