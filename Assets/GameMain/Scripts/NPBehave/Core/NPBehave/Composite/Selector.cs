using UnityEngine;
using UnityEngine.Assertions;
using System.Collections;

namespace NPBehave
{
    /// <summary>
    ///串行执行所有子节点，执行过程中有子节点返回true，中止执行，停用并返回true，如果找不到下一个节点，停用并返回false
    ///启用第一个子节点，
    ///当子节点停用返回false时，按顺序启用下一个子节点
    ///当子节点停用返回true时，停用当前节点并返回true
    ///若所有子节点都返回false，停用当前节点并返回false
    ///若执行过程中 有外部关闭当前节点
    ///当前子节点停用返回true时，停用当前节点并返回true
    ///当前子节点停用返回false时，停用当前节点并返回false
    /// </summary>
    public class Selector : Composite
    {
        /// <summary>
        ///  当前启动的子节点索引
        /// </summary>
        private int currentIndex = -1;

        public Selector(params Node[] children) : base("Selector", children)
        {
        }


        protected override void DoStart()
        {
            foreach (Node child in Children)
            {
                Assert.AreEqual(child.CurrentState, State.INACTIVE);
            }

            currentIndex = -1;

            ProcessChildren();
        }

        protected override void DoStop()
        {
            Children[currentIndex].CancelWithoutReturnResult();
        }

        protected override void DoChildStopped(Node child, bool result)
        {
            if (result)
            {
                //停用当前节点并返回true
                Stopped(true);
            }
            else
            {
                ProcessChildren();
            }
        }

        private void ProcessChildren()
        {
            if (++currentIndex < Children.Length)
            {
                if (IsStopRequested)
                {
                    //按顺序启用下一个节点
                    Stopped(false);
                }
                else
                {
                    Children[currentIndex].Start();
                }
            }
            else
            {
                Stopped(false);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="abortForChild"></param>
        /// <param name="immediateRestart"> true：当前子节点停用返回false，启动请求中的子节点，否则停用返回true;    false：当前子节点停用返回true，停用并返回true，否则停用返回false</param>
        public override void StopLowerPriorityChildrenForChild(Node abortForChild, bool immediateRestart)
        {
            int indexForChild = 0;
            bool found = false;
            foreach (Node currentChild in Children)
            {
                if (currentChild == abortForChild)
                {
                    found = true;
                }
                else if (!found)
                {
                    indexForChild++;
                }
                else if (found && currentChild.IsActive)
                {
                    if (immediateRestart)
                    {
                        currentIndex = indexForChild - 1;
                    }
                    else
                    {
                        currentIndex = Children.Length;
                    }
                    currentChild.CancelWithoutReturnResult();
                    break;
                }
            }
        }

        override public string ToString()
        {
            return base.ToString() + "[" + this.currentIndex + "]";
        }
    }
}