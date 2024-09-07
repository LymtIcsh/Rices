//此文件格式由工具自动生成
using System;
using Sirenix.OdinInspector;
using UnityGameFramework.Runtime;

namespace Suture
{
    /// <summary>
    /// 设置碰撞体BelongToUnit的Id
    /// 所谓所归属的Unit，也就是产出碰撞体的Unit，
    /// 比如诺克放一个Q，那么BelongUnit就是诺克
    /// </summary>
    [Title("设置碰撞体BelongToUnit的Id",TitleAlignment = TitleAlignments.Centered)]
    public class NP_SetUnitIdTheColliderBelongToAction:NP_ClassForStoreAction
    {        
        public NP_BlackBoardRelationData NpBlackBoardRelationData = new NP_BlackBoardRelationData();
        
        public override Action GetActionToBeDone()
        {
            this.Action = this.SetUnitIdTheColliderBelongTo;
            return this.Action;
        }

        public void SetUnitIdTheColliderBelongTo()
        {
#if SERVER
            //这里默许碰撞体自身带有B2S_ColliderComponent
            this.BelongtoRuntimeTree.GetBlackboard().Set(NpBlackBoardRelationData.BBKey, BelongToUnit.GetComponent<B2S_ColliderComponent>().BelongToUnit.Id);
#endif
            Log.Info("碰撞到的unit  id未设置");
            //this.BelongtoRuntimeTree.GetBlackboard().Set(NpBlackBoardRelationData.BBkey, BelongToUnit.GetComponent<B2S_ColliderComponent>().BelongToUnit.Id);
        }
    }
}
