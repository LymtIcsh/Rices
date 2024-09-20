using GameFramework;
using GameFramework.Fsm;
using Sirenix.OdinInspector;

namespace Suture
{
    /// <summary>
    /// 自定义状态类，用于技能编辑器配置
    /// </summary>
    public class CustomState:AFsmStateBase
    {

        public CustomState()
        {
        //   ReferencePool.Acquire<CustomState>();
        }
        
        // public static CustomState Create()
        // {
        //     CustomState state = ReferencePool.Acquire<CustomState>();
        //     return state;
        // }

        protected override void OnInit(IFsm<Pet> fsm)
        {
            base.OnInit(fsm);
        }

        protected override void OnEnter(IFsm<Pet> fsm)
        {
            base.OnEnter(fsm);
        }

        protected override void OnUpdate(IFsm<Pet> fsm, float elapseSeconds, float realElapseSeconds)
        {
            base.OnUpdate(fsm, elapseSeconds, realElapseSeconds);
        }

        protected override void OnLeave(IFsm<Pet> fsm, bool isShutdown)
        {
            base.OnLeave(fsm, isShutdown);
        }

        protected override void OnDestroy(IFsm<Pet> fsm)
        {
            base.OnDestroy(fsm);
        }

        // /// <summary>
        // /// 互斥的状态
        // /// </summary>
        // [LabelText("互斥的状态")]
        // public StateTypes ConflictStateTypes;
        
        // public override StateTypes GetConflictStateTypeses()
        // {
        //    return base.GetConflictStateTypeses();
        // }

        // public override void OnEnter(StackFsmComponent stackFsmComponent)
        // {
        //
        // }
        //
        // public override void OnUpdate(StackFsmComponent stackFsmComponent)
        // {
        //   
        // }
        //
        // public override void OnExit(StackFsmComponent stackFsmComponent)
        // {
        //
        // }
        //
        // public override void OnRemoved(StackFsmComponent stackFsmComponent)
        // {
        //
        // }
    }
}