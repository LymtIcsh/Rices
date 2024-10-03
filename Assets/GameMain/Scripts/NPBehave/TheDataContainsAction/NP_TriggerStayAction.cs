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
    [Title("是否在检测范围", TitleAlignment = TitleAlignments.Centered)]
    public class NP_TriggerStayAction : NP_ClassForStoreAction
    {
        [LabelText("是否在检测范围")] public NP_BlackBoardRelationData _BlackBoardRelation = new NP_BlackBoardRelationData()
            { WriteOrCompareToBB = true };

        // [LabelText("是否在攻击范围")] public NP_BlackBoardRelationData ATKRadius = new NP_BlackBoardRelationData();
        //
        // [LabelText("停止追击范围")] public NP_BlackBoardRelationData PursuitDistance = new NP_BlackBoardRelationData() { };

        [LabelText("下一次的位置")] public NP_BlackBoardRelationData NextPos = new NP_BlackBoardRelationData() { };

        [LabelText("触发的tag")] public NP_BlackBoardRelationData TriggerTag = new NP_BlackBoardRelationData() { };

        private Collider curretCollider;

        public override Action GetActionToBeDone()
        {
            this.Action = this.DetectionSkillHit;
            return this.Action;
        }

        public void DetectionSkillHit()
        {
            TriggerRadius().Forget();

            // GameEntry.RangeCheck.NavigationAIEnter(this.BelongToUnit.gameObject,
            //     TriggerTag.GetBlackBoardValue<string>(this.BelongtoRuntimeTree.GetBlackboard()), 1.0f,
            //     async (curretCollider) =>
            //     {
            //         _BlackBoardRelation.SetBlackBoardValue(this.BelongtoRuntimeTree.GetBlackboard(), true);
            //         float value = PursuitDistance.GetBlackBoardValue<float>(this.BelongtoRuntimeTree.GetBlackboard());
            //
            //         var UnityV3 = curretCollider.transform.position;
            //
            //         // //是否到达停止追击范围
            //         // if (Vector3.SqrMagnitude(UnityV3 - BelongToUnit.transform.position) > value)
            //         // {        //继续追击
            //         //     
            //             System.Numerics.Vector3 position = new System.Numerics.Vector3(UnityV3.x, UnityV3.y, UnityV3.z);
            //
            //             Log.Info($"下一次的位置{position}");
            //             NextPos.SetBlackBoardValue(this.BelongtoRuntimeTree.GetBlackboard(), position);
            //             await UniTask.Yield();
            //         // }
            //         // else
            //         // {        //停止追击
            //         //     _BlackBoardRelation.SetBlackBoardValue(this.BelongtoRuntimeTree.GetBlackboard(), false);
            //         // }
            //     }).Forget();
        }

        async UniTaskVoid TriggerRadius()
        {
            do
            {
                curretCollider = await this.BelongToUnit.GetAsyncTriggerStayTrigger().OnTriggerStayAsync();
            } while (!curretCollider.CompareTag(
                         TriggerTag.GetBlackBoardValue<string>(this.BelongtoRuntimeTree.GetBlackboard())));

  
         
            _BlackBoardRelation.SetBlackBoardValue(this.BelongtoRuntimeTree.GetBlackboard(), true);

            var UnityV3 = curretCollider.transform.position;
            

                System.Numerics.Vector3 position = new System.Numerics.Vector3(UnityV3.x, UnityV3.y, UnityV3.z);

               // Log.Info($"下一次的位置{position}");
                NextPos.SetBlackBoardValue(this.BelongtoRuntimeTree.GetBlackboard(), position);
         
        }
    }
}