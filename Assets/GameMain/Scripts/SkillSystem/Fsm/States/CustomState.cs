using Sirenix.OdinInspector;

namespace Suture
{
    /// <summary>
    /// 自定义状态类，用于技能编辑器配置
    /// </summary>
    public class CustomState:AFsmStateBase
    {
        /// <summary>
        /// 互斥的状态
        /// </summary>
        [LabelText("互斥的状态")]
        public StateTypes ConflictStateTypes;
        
        public override StateTypes GetConflictStateTypeses()
        {
            return ConflictStateTypes;
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