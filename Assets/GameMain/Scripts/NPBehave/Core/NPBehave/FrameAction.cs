using System;
using GameFramework;

namespace NPBehave
{
    public class FrameAction :IReference,IEquatable<FrameAction>
    {
        public override int GetHashCode()
        {
            // 关键字用于取消整型类型的算术运算和转换的溢出检查。
            //checked和unchecked，都可以加于一个语句块前或者一个算术表达式前。
            //加checked标志的语句或表达式如果发生算术溢出，则抛出System.OverflowException类型的异常，
            //而加unchecked标志的语句发生算术溢出时，则不抛出异常。
            unchecked    
            {
                var hashCode = Id.GetHashCode();
                hashCode = (hashCode * 397) ^ (Action != null ? Action.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (int)IntervalFrame;
                hashCode = (hashCode * 397) ^ RepeatTime;
                hashCode = (hashCode * 397) ^ (int) TargetTickFrame;
                return hashCode;
            }
        }

        public long Id;
        public System.Action Action;
        
        /// <summary>
        /// 间隔帧，为0代表每帧都执行
        /// </summary>
        public uint IntervalFrame = 0;
        
        /// <summary>
        /// 重复次数，默认为1，只执行一次，如果为-1代表无限执行，直到手动移除
        /// </summary>
        public int RepeatTime = 1;

        /// <summary>
        /// 目标触发帧，将在这一帧进行callBack
        /// </summary>
        public uint TargetTickFrame = 0;
        
        public void Clear()
        {
            this.Id = 0;
            this.IntervalFrame = 0;
            this.Action = null;
            this.RepeatTime = 1;
            this.TargetTickFrame = 0;
        }

        #region 对比函数
        
        public bool Equals(FrameAction other)
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

            return this.Id == other.Id && IntervalFrame == other.IntervalFrame && this.RepeatTime == other.RepeatTime;
        }
        
        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj))
            {
                return false;
            }

            if (ReferenceEquals(this, obj))
            {
                return true;
            }

            if (obj.GetType() != this.GetType())
            {
                return false;
            }

            return Equals((FrameAction) obj);
        }

        public static bool operator ==(FrameAction lhs, FrameAction rhs)
        {
            //检查左侧是否为空。
            if (System.Object.ReferenceEquals(lhs, null))
            {
                if (System.Object.ReferenceEquals(rhs, null))
                {
                    // null == null = true.
                    return true;
                }

                //只有左边是空的。
                return false;
            }

            //等号处理右侧为空的情况。
            return lhs.Equals(rhs);
        }

        public static bool operator !=(FrameAction lhs, FrameAction rhs)
        {
            return !(lhs == rhs);
        }
        
        #endregion
    }
}