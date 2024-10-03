//此文件格式由工具自动生成

using System;
using Cysharp.Threading.Tasks;
using Cysharp.Threading.Tasks.Triggers;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.AI;
using UnityGameFramework.Runtime;

namespace Suture
{
    [Title("离开检测范围", TitleAlignment = TitleAlignments.Centered)]
    public class NP_ExitTriggerRadiusAction : NP_ClassForStoreAction
    {
        [LabelText("是否在检测范围")] public NP_BlackBoardRelationData _BlackBoardRelation = new NP_BlackBoardRelationData();

        [LabelText("是否在攻击范围")] public NP_BlackBoardRelationData ATKRadius = new NP_BlackBoardRelationData();

        [LabelText("返回初始位置")] public NP_BlackBoardRelationData IldfeRadius = new NP_BlackBoardRelationData();

        private Collider curretCollider;

        public override Action GetActionToBeDone()
        {
            this.Action = this.ExitTriggerRadius;
            return this.Action;
        }

        public void ExitTriggerRadius()
        {
            ExitTrigger().Forget();
        }

        public async UniTaskVoid ExitTrigger()
        {
                curretCollider = await this.BelongToUnit.GetAsyncTriggerExitTrigger().OnTriggerExitAsync();

            _BlackBoardRelation.SetBlackBoardValue(this.BelongtoRuntimeTree.GetBlackboard(), false);
            ATKRadius.SetBlackBoardValue(this.BelongtoRuntimeTree.GetBlackboard(), false);
            IldfeRadius.SetBlackBoardValue(this.BelongtoRuntimeTree.GetBlackboard(), true);

            //停止追击
            this.BelongToUnit.transform.GetComponent<NavMeshAgent>().isStopped = true;

            Log.Info($"{curretCollider.name} 离开检测范围");
            curretCollider = null;
        }
    }
}