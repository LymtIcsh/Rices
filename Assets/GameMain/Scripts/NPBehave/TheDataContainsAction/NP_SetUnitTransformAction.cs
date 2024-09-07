//此文件格式由工具自动生成

using System;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Suture
{
    [Title("设置Unit的Transform信息", TitleAlignment = TitleAlignments.Centered)]
    public class NP_SetUnitTransformAction : NP_ClassForStoreAction
    {
        [LabelText("是否设置位置")] public bool SetPos;

        [LabelText("是否设置旋转")] public bool SetRot;

        [ShowIf(nameof(SetPos))] [LabelText("将要设置的位置")]
        public NP_BlackBoardRelationData PosBlackBoardRelationData = new NP_BlackBoardRelationData();

        [ShowIf(nameof(SetRot))] [LabelText("将要设置的旋转")]
        public NP_BlackBoardRelationData RotBlackBoardRelationData = new NP_BlackBoardRelationData();


        public override Action GetActionToBeDone()
        {
            this.Action = this.SetUnitTransform;
            return this.Action;
        }

        public void SetUnitTransform()
        {
            TargetableObject unit = BelongToUnit;
            if (SetPos)
            {
                // float result =
                //     PosBlackBoardRelationData.GetBlackBoardValue<float>(this.BelongtoRuntimeTree.GetBlackboard());
                //
                // Vector2 value = unit.GetComponent<PlayerAssetsInputs>().move.normalized;
                
               // value = value == Vector2.zero ? Vector2.up * result : value * result;
               
                //unit.Position = new Vector3(unit.Position.x + value.x, unit.Position.y, unit.Position.z + value.y);

                unit.Position += unit.transform.forward;
            }

            if (SetRot)
            {
                unit.Rotation = Quaternion.Euler(0,
                    RotBlackBoardRelationData.GetBlackBoardValue<float>(this.BelongtoRuntimeTree.GetBlackboard()), 0);
            }
        }
    }
}