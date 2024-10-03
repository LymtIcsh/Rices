//此文件格式由工具自动生成

using System;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.AI;

namespace Suture
{
    [Title("等待unit到达指定位置", TitleAlignment = TitleAlignments.Centered)]
    public class NP_WaitUnitByTargetPosAction : NP_ClassForStoreAction
    {
        [LabelText("移动位置")] public NP_BlackBoardRelationData pos = new NP_BlackBoardRelationData() { };

        [LabelText("停止追击范围")] public NP_BlackBoardRelationData PursuitDistance = new NP_BlackBoardRelationData() { };

        [LabelText("是否在检测范围")] public NP_BlackBoardRelationData _BlackBoardRelation = new NP_BlackBoardRelationData();


        public override Func<bool, NPBehave.Action.Result> GetFunc2ToBeDone()
        {
            this.Func2 = this.CheckUnitByTargetPos;
            return this.Func2;
        }

        public NPBehave.Action.Result CheckUnitByTargetPos(bool hasDown)
        {
            System.Numerics.Vector3 SV3 =
                pos.GetBlackBoardValue<System.Numerics.Vector3>(this.BelongtoRuntimeTree.GetBlackboard());
            Vector3 position = new Vector3(SV3.X, SV3.Y, SV3.Z);

            float distanc = (position - this.BelongToUnit.transform.position).sqrMagnitude;

            if (distanc > PursuitDistance.GetBlackBoardValue<float>(this.BelongtoRuntimeTree.GetBlackboard()) &&
                _BlackBoardRelation.GetBlackBoardValue<bool>(this.BelongtoRuntimeTree.GetBlackboard()))
            {
                return NPBehave.Action.Result.PROGRESS;
            }
            else
            {
                //this.BelongToUnit.transform.GetComponent<NavMeshAgent>().isStopped = true;
                return NPBehave.Action.Result.SUCCESS;
            }
        }
    }
}