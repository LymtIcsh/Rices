﻿using System;
using Sirenix.OdinInspector;

namespace Suture
{
    /// <summary>
    /// 这里默认修改自己的黑板值
    /// </summary>
    [Title("修改黑板值", TitleAlignment = TitleAlignments.Centered)]
    public class NP_ChangeBlackValueAction:NP_ClassForStoreAction
    {
        public NP_BlackBoardRelationData NPBalckBoardRelationData = new NP_BlackBoardRelationData()
            { WriteOrCompareToBB = true };

        public override Action GetActionToBeDone()
        {
            this.Action = this.ChangeBlackBoard;
            return this.Action;
        }

        private void ChangeBlackBoard()
        {
            this.NPBalckBoardRelationData.SetBlackBoardValue(this.BelongtoRuntimeTree.GetBlackboard());
        }
    }
}