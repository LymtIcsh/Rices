//此文件格式由工具自动生成

using System;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityGameFramework.Runtime;

namespace Suture
{
    /// <summary>
    /// 创建物体Action
    /// </summary>
    [Title("创建一个物体", TitleAlignment = TitleAlignments.Centered)]
    public class NP_CreateObjAction : NP_ClassForStoreAction
    {
        [LabelText("要创建的实体ID")] public NP_BlackBoardRelationData _BoardRelationID = new NP_BlackBoardRelationData();

        [LabelText("要创建的实体资源名称")]
        public NP_BlackBoardRelationData _BoardRelationAssetName = new NP_BlackBoardRelationData();

        public override Action GetActionToBeDone()
        {
            this.Action = this.CreateObj;
            return this.Action;
        }

        public void CreateObj()
        {
            Pet unit = BelongToUnit;

            int id = _BoardRelationID.GetBlackBoardValue<int>(this.BelongtoRuntimeTree.GetBlackboard());

            Transform _transform;
            
            GameEntry.Event.Fire(this,
                SpawnSkillObjEventArgs.Create(null,
                    SkillMushroomData.Create(GameEntry.Entity.GenerateSerialId(), id,
                        _BoardRelationAssetName.GetBlackBoardValue<string>(this.BelongtoRuntimeTree.GetBlackboard()),
                        (_transform = unit.transform).position + _transform.forward * 3,unit)));

            // GameEntry.Entity.ShowSkillSummonObject<SkillMushroom>("SkillMushroom",
            //     _BoardRelationAssetName.GetBlackBoardValue<string>(this.BelongtoRuntimeTree.GetBlackboard()),
            //     Constant.AssetPriority.ArmorAsset, new SkillMushroomData(id, id)
            //     {
            //         //  Name = name,
            //         //TODO Animator 开启了Apply Root Motion，所以无法设置位置
            //         Position = unit.transform.forward + Vector3.forward * 3,
            //         
            //         Scale = Vector3.one,
            //     });
            // GameEntry.Entity.GetEntity(id);
            //  Log.Info();
        }
    }
}