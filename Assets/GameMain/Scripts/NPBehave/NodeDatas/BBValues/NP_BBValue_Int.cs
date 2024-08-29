﻿using System;

using Sirenix.OdinInspector;


namespace Suture
{
    [HideLabel]
    [HideReferenceObjectPicker]
    public class NP_BBValue_Int:NP_BBValueBase<int>,IEquatable<NP_BBValue_Int>
    {
         public override Type NP_BBValueType => typeof(int);

        #region 对比函数

        public bool Equals(NP_BBValue_Int other)
        {
            // 如果parameter为null，则返回false。
            if (System.Object.ReferenceEquals(other, null))
            {
                return false;
            }

            // 优化一个常见的成功案例。
            if (System.Object.ReferenceEquals(this, other))
            {
                return true;
            }

            // 如果运行时类型不完全相同，则返回false。
            if (this.GetType() != other.GetType())
            {
                return false;
            }

            //如果字段匹配则返回true。
            //注意基类不会被调用，因为它被调用了
            //System.Object，它将Equals定义为引用相等。
            return this.Value==other.GetValue();
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null,obj))
            {
                return false;
            }

            if (ReferenceEquals(this,obj))
            {
                return true;
            }

            if (obj.GetType()!=this.GetType())
            {
                return false;
            }

            return Equals((NP_BBValue_Int)obj);
        }

        public override int GetHashCode()
        {
            return this.Value.GetHashCode();
        }

        public static bool operator ==(NP_BBValue_Int lhs, NP_BBValue_Int rhs)
        {
            // 检查左侧是否为空。
            if (System.Object.ReferenceEquals(lhs, null))
            {
                if (System.Object.ReferenceEquals(rhs, null))
                {
                    // null == null = true.
                    return true;
                }

                // 只有左边是空的。
                return false;
            }

            // 等号处理右侧为空的情况。
            return lhs.Equals(rhs);
        }

        public static bool operator !=(NP_BBValue_Int lhs, NP_BBValue_Int rhs)
        {
            return !(lhs == rhs);
        }

        public static bool operator >(NP_BBValue_Int lhs, NP_BBValue_Int rhs)
        {
            return lhs.GetValue() > rhs.GetValue();
        }

        public static bool operator <(NP_BBValue_Int lhs, NP_BBValue_Int rhs)
        {
            return lhs.GetValue() < rhs.GetValue();
        }

        public static bool operator >=(NP_BBValue_Int lhs, NP_BBValue_Int rhs)
        {
            return lhs.GetValue() >= rhs.GetValue();
        }

        public static bool operator <=(NP_BBValue_Int lhs, NP_BBValue_Int rhs)
        {
            return lhs.GetValue() <= rhs.GetValue();
        }

        #endregion

    }
}