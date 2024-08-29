//此文件格式由工具自动生成

using System;
using NPBehave;
using Sirenix.OdinInspector;
using UnityGameFramework.Runtime;
using Action = System.Action;

namespace Suture
{
    [Title("检查指定黑板数据", TitleAlignment = TitleAlignments.Centered)]
    public class NP_CheckBBValueAction : NP_ClassForStoreAction
    {
        [LabelText("运算符号")] public Operator Ope = Operator.IS_EQUAL;

        public NP_BlackBoardRelationData NpBlackBoardRelationData = new NP_BlackBoardRelationData()
            { WriteOrCompareToBB = true };

        public override Func<bool> GetFunc1ToBeDone()
        {
            this.Func1 = this.CheckBBValue;
            return this.Func1;
        }

        public bool CheckBBValue()
        {
            if (Ope == Operator.ALWAYS_TRUE)
            {
                return true;
            }

            string key = NpBlackBoardRelationData.BBkey;
            Blackboard selfBlackboard = this.BelongtoRuntimeTree.GetBlackboard();
            if (!selfBlackboard.Isset(key))
            {
                return Ope == Operator.IS_NOT_SET;
            }

            ANP_BBValue preSetValue = this.NpBlackBoardRelationData.NP_BBValue;
            ANP_BBValue bbValue = selfBlackboard.Get(key);

            switch (this.Ope)
            {
                case Operator.IS_SET:
                    return true;
                case Operator.IS_EQUAL:
                {
                    switch (preSetValue)
                    {
                        case NP_BBValue_Bool npBbValue:
                            return npBbValue == bbValue as NP_BBValue_Bool;
                        case NP_BBValue_Float npBbValue:
                            return npBbValue == bbValue as NP_BBValue_Float;
                        case NP_BBValue_Int npBbValue:
                            return npBbValue == bbValue as NP_BBValue_Int;
                        case NP_BBValue_String npBbValue:
                            return npBbValue == bbValue as NP_BBValue_String;
                        case NP_BBValue_Vector3 npBbValue:
                            return npBbValue == bbValue as NP_BBValue_Vector3;
                        case NP_BBValue_Long npBbValue:
                            return npBbValue == bbValue as NP_BBValue_Long;
                        default:
                            Log.Error($"类型为{preSetValue.GetType()}的数未注册为NP_BBValue");
                            return false;
                    }
                }
                case Operator.IS_NOT_EQUAL:
                {
                    switch (preSetValue)
                    {
                        case NP_BBValue_Bool npBbValue:
                            return npBbValue != bbValue as NP_BBValue_Bool;
                        case NP_BBValue_Float npBbValue:
                            return npBbValue != bbValue as NP_BBValue_Float;
                        case NP_BBValue_Int npBbValue:
                            return npBbValue != bbValue as NP_BBValue_Int;
                        case NP_BBValue_String npBbValue:
                            return npBbValue != bbValue as NP_BBValue_String;
                        case NP_BBValue_Long npBbValue:
                            return npBbValue != bbValue as NP_BBValue_Long;
                        case NP_BBValue_Vector3 npBbValue:
                            return npBbValue != bbValue as NP_BBValue_Vector3;
                        default:
                            Log.Error($"类型为{preSetValue.GetType()}的数未注册为NP_BBValue");
                            return false;
                    }
                }
                case Operator.IS_GREATER_OR_EQUAL:
                {
                    switch (preSetValue)
                    {
                        case NP_BBValue_Bool npBbValue:
                            return (bbValue as NP_BBValue_Bool) >= npBbValue;
                        case NP_BBValue_Float npBbValue:
                            return (bbValue as NP_BBValue_Float) >= npBbValue;
                        case NP_BBValue_Int npBbValue:
                            return (bbValue as NP_BBValue_Int) >= npBbValue;
                        case NP_BBValue_String npBbValue:
                            return (bbValue as NP_BBValue_String) >= npBbValue;
                        case NP_BBValue_Long npBbValue:
                            return (bbValue as NP_BBValue_Long) >= npBbValue;
                        case NP_BBValue_Vector3 npBbValue:
                            return (bbValue as NP_BBValue_Vector3) >= npBbValue;
                        default:
                            Log.Error($"类型为{preSetValue.GetType()}的数未注册为NP_BBValue");
                            return false;
                    }
                }
                case Operator.IS_GREATER:
                {
                    switch (preSetValue)
                    {
                        case NP_BBValue_Bool npBbValue:
                            return (bbValue as NP_BBValue_Bool) > npBbValue;
                        case NP_BBValue_Float npBbValue:
                            return (bbValue as NP_BBValue_Float) > npBbValue;
                        case NP_BBValue_Int npBbValue:
                            return (bbValue as NP_BBValue_Int) > npBbValue;
                        case NP_BBValue_String npBbValue:
                            return (bbValue as NP_BBValue_String) > npBbValue;
                        case NP_BBValue_Long npBbValue:
                            return (bbValue as NP_BBValue_Long) > npBbValue;
                        case NP_BBValue_Vector3 npBbValue:
                            return (bbValue as NP_BBValue_Vector3) > npBbValue;
                        default:
                            Log.Error($"类型为{preSetValue.GetType()}的数未注册为NP_BBValue");
                            return false;
                    }
                }
                case Operator.IS_SMALLER_OR_EQUAL:
                    switch (preSetValue)
                    {
                        case NP_BBValue_Bool npBbValue:
                            return (bbValue as NP_BBValue_Bool) <= npBbValue;
                        case NP_BBValue_Float npBbValue:
                            return (bbValue as NP_BBValue_Float) <= npBbValue;
                        case NP_BBValue_Int npBbValue:
                            return (bbValue as NP_BBValue_Int) <= npBbValue;
                        case NP_BBValue_String npBbValue:
                            return (bbValue as NP_BBValue_String) <= npBbValue;
                        case NP_BBValue_Long npBbValue:
                            return (bbValue as NP_BBValue_Long) <= npBbValue;
                        case NP_BBValue_Vector3 npBbValue:
                            return (bbValue as NP_BBValue_Vector3) <= npBbValue;
                        default:
                            Log.Error($"类型为{preSetValue.GetType()}的数未注册为NP_BBValue");
                            return false;
                    }
                case Operator.IS_SMALLER:
                    switch (preSetValue)
                    {
                        case NP_BBValue_Bool npBbValue:
                            return (bbValue as NP_BBValue_Bool) < npBbValue;
                        case NP_BBValue_Float npBbValue:
                            return (bbValue as NP_BBValue_Float) < npBbValue;
                        case NP_BBValue_Int npBbValue:
                            return (bbValue as NP_BBValue_Int) < npBbValue;
                        case NP_BBValue_String npBbValue:
                            return (bbValue as NP_BBValue_String) < npBbValue;
                        case NP_BBValue_Long npBbValue:
                            return (bbValue as NP_BBValue_Long) < npBbValue;
                        case NP_BBValue_Vector3 npBbValue:
                            return (bbValue as NP_BBValue_Vector3) < npBbValue;
                        default:
                            Log.Error($"类型为{preSetValue.GetType()}的数未注册为NP_BBValue");
                            return false;
                    }

                default: return false;
            }
        }
    }
}