using System;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityGameFramework.Runtime;

namespace Suture
{
    [HideLabel]
    [HideReferenceObjectPicker]
    public class NP_BBValue_List_Long:NP_BBValueBase<List<long>>,IEquatable<NP_BBValue_List_Long>
    {
         public override Type NP_BBValueType => typeof(List<long>);

         protected override void SetValueFrom(INP_BBValue<List<long>> bbValue)
         {
             //因为List是引用类型，所以这里要做一下特殊处理，如果要设置的值为0元素的List，就Clear一下，而且这个东西也不会用来做为黑板条件，因为它没办法用来对比
             //否则就拷贝全部元素
             this.Value.Clear();
             foreach (var item in bbValue.GetValue())
             {
                 this.Value.Add(item);
             }
         }

         public override void SetValueFrom(List<long> bbValue)
         {
             //因为List是引用类型，所以这里要做一下特殊处理，如果要设置的值为0元素的List，就Clear一下，而且这个东西也不会用来做为黑板条件，因为它没办法用来对比
             //否则就拷贝全部元素
             this.Value.Clear();
             foreach (var item in bbValue)
             {
                 this.Value.Add(item);
             }
         }

         #region 对比函数

        public bool Equals(NP_BBValue_List_Long other)
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

            if (this.Value.Count!=other.GetValue().Count)
            {
                return false;
            }

            //如果字段匹配则返回true。
            //注意基类不会被调用，因为它被调用了
            //System.Object，它将Equals定义为引用相等。
            for (int i = 0; i < this.Value.Count; i++)
            {
                if (this.Value[i]!= other.GetValue()[i])
                {
                    return false;
                }
            }

            return true;
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

            return Equals((NP_BBValue_List_Long)obj);
        }

        public override int GetHashCode()
        {
            return this.Value.GetHashCode();
        }

        public static bool operator ==(NP_BBValue_List_Long lhs, NP_BBValue_List_Long rhs)
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

        public static bool operator !=(NP_BBValue_List_Long lhs, NP_BBValue_List_Long rhs)
        {
            return !(lhs == rhs);
        }

        public static bool operator >(NP_BBValue_List_Long lhs, NP_BBValue_List_Long rhs)
        {
            Log.Error("你他妈确定对比两个List<long>大小关系？这是人能干出来的事？想对比自己写");
            return false;
        }

        public static bool operator <(NP_BBValue_List_Long lhs, NP_BBValue_List_Long rhs)
        {
            Log.Error("你他妈确定对比两个List<long>大小关系？这是人能干出来的事？想对比自己写");
            return false;
        }

        public static bool operator >=(NP_BBValue_List_Long lhs, NP_BBValue_List_Long rhs)
        {
            Log.Error("你他妈确定对比两个List<long>大小关系？这是人能干出来的事？想对比自己写");
            return false;
        }

        public static bool operator <=(NP_BBValue_List_Long lhs, NP_BBValue_List_Long rhs)
        {
            Log.Error("你他妈确定对比两个List<long>大小关系？这是人能干出来的事？想对比自己写");
            return false;
        }

        #endregion
    }
}