//此文件格式由工具自动生成

using System;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.AI;
using UnityGameFramework.Runtime;

namespace Suture
{
    [Title("距离检测", TitleAlignment = TitleAlignments.Centered)]
    public class NP_DistanceDetectionAction : NP_ClassForStoreAction
    {
        [LabelText("检测目标")] public NP_BlackBoardRelationData _distanceTar = new NP_BlackBoardRelationData();


        [LabelText("检测距离")] public NP_BlackBoardRelationData _distanceValue = new NP_BlackBoardRelationData();

        [LabelText("检测结果")] public NP_BlackBoardRelationData _Value = new NP_BlackBoardRelationData();
        [LabelText("检测结果1设置")] public bool _ValueSet;

        // [LabelText("检测结果")] public NP_BlackBoardRelationData _Move = new NP_BlackBoardRelationData();
        // [LabelText("检测结果2设置")] public bool _MoveValueSet;

        public override Action GetActionToBeDone()
        {
            this.Action = this.DistanceDetection;
            return this.Action;
        }

        public void DistanceDetection()
        {
            System.Numerics.Vector3 SV3 =
                _distanceTar.GetBlackBoardValue<System.Numerics.Vector3>(this.BelongtoRuntimeTree.GetBlackboard());
            Vector3 position = new Vector3(SV3.X, SV3.Y, SV3.Z);

            float distanc = (position - this.BelongToUnit.transform.position).sqrMagnitude;

            NavMeshAgent _navMeshAgent = this.BelongToUnit.transform.GetComponent<NavMeshAgent>();

            if (distanc > _distanceValue.GetBlackBoardValue<float>(this.BelongtoRuntimeTree.GetBlackboard()))
            {
                //距离外
                 //Log.Info($"检测距离外 结果{_ValueSet}");
                //   _Move.SetBlackBoardValue(this.BelongtoRuntimeTree.GetBlackboard(), _MoveValueSet); 
                if (_navMeshAgent.isStopped)
                {
                    _navMeshAgent.isStopped = false;
                }

                _Value.SetBlackBoardValue(this.BelongtoRuntimeTree.GetBlackboard(), _ValueSet);
            }
            else //距离内
            {
                //Log.Info($"检测距离内 结果{!_ValueSet}");
                //    _Move.SetBlackBoardValue(this.BelongtoRuntimeTree.GetBlackboard(), !_MoveValueSet);
                if (!_navMeshAgent.isStopped)
                {
                    _navMeshAgent.isStopped = true;
                }

                _Value.SetBlackBoardValue(this.BelongtoRuntimeTree.GetBlackboard(), !_ValueSet);
            }
        }
    }
}