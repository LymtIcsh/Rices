//此文件格式由工具自动生成

using System;
using Cysharp.Threading.Tasks;
using Cysharp.Threading.Tasks.Triggers;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityGameFramework.Runtime;

namespace Suture
{
    [Title("进入触发器", TitleAlignment = TitleAlignments.Centered)]
    public class NP_TriggerEnterAction : NP_ClassForStoreAction
    {
        [LabelText("是否在检测范围")] public NP_BlackBoardRelationData _BlackBoardRelation = new NP_BlackBoardRelationData()
            { WriteOrCompareToBB = true };

        [LabelText("下一次的位置")] public NP_BlackBoardRelationData NextPos = new NP_BlackBoardRelationData() { };

        [LabelText("触发的tag")] public NP_BlackBoardRelationData TriggerTag = new NP_BlackBoardRelationData() { };

        private Collider curretCollider;

        public override Action GetActionToBeDone()
        {
            this.Action = this.TriggerEnter;
            return this.Action;
        }


        public void TriggerEnter()
        {
            TriggerRadius().Forget();
        }

        async UniTaskVoid TriggerRadius()
        {
            do
            {
                curretCollider = await this.BelongToUnit.GetAsyncTriggerEnterTrigger().OnTriggerEnterAsync();
            } while (!curretCollider.CompareTag(
                         TriggerTag.GetBlackBoardValue<string>(this.BelongtoRuntimeTree.GetBlackboard())));


            _BlackBoardRelation.SetBlackBoardValue(this.BelongtoRuntimeTree.GetBlackboard(), true);

            var UnityV3 = curretCollider.transform.position;

            System.Numerics.Vector3 position = new System.Numerics.Vector3(UnityV3.x, UnityV3.y, UnityV3.z);

            Log.Info($"下一次的位置{position}");
            NextPos.SetBlackBoardValue(this.BelongtoRuntimeTree.GetBlackboard(), position);
        }
    }
}