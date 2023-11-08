using System;
using GameFramework;
using UnityEngine;

namespace UnityGameFramework.Runtime
{
    public sealed class VarValueTuple : Variable<ValueTuple<int,string,Vector3>>
    {
        /// <summary>
        /// 初始化 System.ValueTuple 变量类的新实例。
        /// </summary>
        public VarValueTuple()
        {
        }

        /// <summary>
        /// 从 System.ValueTuple 到 System.ValueTuple 变量类的隐式转换。
        /// </summary>
        /// <param name="value">值。</param>
        public static implicit operator VarValueTuple(ValueTuple<int,string,Vector3> value)
        {
            VarValueTuple varValue = ReferencePool.Acquire<VarValueTuple>();
            varValue.Value = value;
            return varValue;
        }

        /// <summary>
        /// 从 System.ValueTuple 变量类到 System.ValueTuple 的隐式转换。
        /// </summary>
        /// <param name="value">值。</param>
        public static implicit operator ValueTuple<int,string,Vector3>(VarValueTuple value)
        {
            return value.Value;
        }
    }
}