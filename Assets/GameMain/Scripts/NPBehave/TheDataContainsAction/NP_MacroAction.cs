//此文件格式由工具自动生成
using System;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityGameFramework.Runtime;

namespace Suture
{
    /// <summary>
    /// 用于封装一系列ActionNode，旨在增强易读性，默认只能是Action，而不能是Func
    /// </summary>
    [Title("宏行为节点",TitleAlignment = TitleAlignments.Centered)]
    public class NP_MacroAction:NP_ClassForStoreAction
    {
        public List<NP_ClassForStoreAction> NpClassForStoreActions = new List<NP_ClassForStoreAction>();
        
        public override Action GetActionToBeDone()
        {
            foreach (var npClassForStoreAction in NpClassForStoreActions)
            {
                npClassForStoreAction.BelongToUnit = this.BelongToUnit;
                npClassForStoreAction.BelongtoRuntimeTree = this.BelongtoRuntimeTree;
            }
            
            this.Action = this.DoMacro;
            return this.Action;
        }

        public void DoMacro()
        {
            foreach (var classForStoreAction in NpClassForStoreActions)
            {
                classForStoreAction.GetActionToBeDone().Invoke();
            }
        }
    }
}
