
using System.Collections.Generic;
using ProcedureOwner = GameFramework.Fsm.IFsm<GameFramework.Procedure.IProcedureManager>;

namespace Suture
{
    public class ProcedureMain :ProcedureBase
    {
        /// <summary>
        /// 游戏延迟秒数
        /// </summary>
        private const float GameOverDelayedSeconds = 2f;

        //private readonly Dictionary<GameMode,Ga>
        public bool m_GotoMenu = false;

        public override bool UseNativeDialog
        {
            get
            {
                return false;
            }
        }

        public void GotoMenu()
        {
            m_GotoMenu = true;
        }

        protected override void OnInit(ProcedureOwner procedureOwner)
        {
            base.OnInit(procedureOwner);

        }

        protected override void OnEnter(ProcedureOwner procedureOwner)
        {
            base.OnEnter(procedureOwner);

        }

        protected override void OnUpdate(ProcedureOwner procedureOwner, float elapseSeconds, float realElapseSeconds)
        {
            base.OnUpdate(procedureOwner, elapseSeconds, realElapseSeconds);
        }

        protected override void OnLeave(ProcedureOwner procedureOwner, bool isShutdown)
        {
            base.OnLeave(procedureOwner, isShutdown);
        }

        protected override void OnDestroy(ProcedureOwner procedureOwner)
        {
            base.OnDestroy(procedureOwner);
        }
    }
}
