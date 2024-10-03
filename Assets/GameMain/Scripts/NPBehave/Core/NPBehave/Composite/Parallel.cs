using UnityEngine;
using UnityEngine.Assertions;
using System.Collections.Generic;
using Sirenix.OdinInspector;

namespace NPBehave
{
    /// <summary>
    /// 并行执行所有子节点，根据成功原则和失败原则，决定节点停用时机
    /// | 成功原则 | 失败原则 |
    ///   | One | One |
    ///   第一个子节点停用返回true(false)后，关闭所有子节点，停用当前节点返回true(false)
    ///    | One | ALL |
    ///  当有一个子节点停用返回true后，关闭所有子节点，停用当前节点返回true，否则返回false
    ///  | ALL | One |
    ///  所有子节点停用返回true后，当前节点停用返回true，否则返回false
    ///   | ALL | ALL |
    ///   所有子节点停用返回true后，当前节点停用返回true，否则返回false
    /// </summary>
    public class Parallel : Composite
    {
        /// <summary>
        /// 原则
        /// </summary>
        public enum Policy
        {
            [LabelText("一个XX就返回XX")]
            ONE,
            [LabelText("全部XX才返回XX")]
            ALL,
        }

        // public enum Wait
        // {
        //     NEVER,
        //     ON_FAILURE,
        //     ON_SUCCESS,
        //     BOTH
        // }

        // private Wait waitForPendingChildrenRule;
        /// <summary>
        /// 失败原则
        /// </summary>
        private Policy failurePolicy;
        /// <summary>
        /// 成功原则
        /// </summary>
        private Policy successPolicy;
        /// <summary>
        /// 子节点数量
        /// </summary>
        private int childrenCount = 0;
        /// <summary>
        /// 当前处于启用状态的子节点数量
        /// </summary>
        private int runningCount = 0;
        /// <summary>
        /// 子节点停用返回true数量
        /// </summary>
        private int succeededCount = 0;
        /// <summary>
        /// 子节点停用返回false数量
        /// </summary>
        private int failedCount = 0;
        /// <summary>
        /// 子节点→返回结果 字典
        /// </summary>
        private Dictionary<Node, bool> childrenResults;
        /// <summary>
        /// 返回结果
        /// </summary>
        private bool successState;
        /// <summary>
        /// 锁定successState无法被覆盖
        /// </summary>
        private bool childrenAborted;

        public Parallel(Policy successPolicy, Policy failurePolicy, /*Wait waitForPendingChildrenRule,*/ params Node[] children) : base("Parallel", children)
        {
            this.successPolicy = successPolicy;
            this.failurePolicy = failurePolicy;
            // this.waitForPendingChildrenRule = waitForPendingChildrenRule;
            this.childrenCount = children.Length;
            this.childrenResults = new Dictionary<Node, bool>();
        }

        protected override void DoStart()
        {
            foreach (Node child in Children)
            {
                Assert.AreEqual(child.CurrentState, State.INACTIVE);
            }

            childrenAborted = false;
            runningCount = 0;
            succeededCount = 0;
            failedCount = 0;
            foreach (Node child in this.Children)
            {
                runningCount++;
                child.Start();
            }
        }

        protected override void DoStop()
        {
            Debug.Assert(runningCount + succeededCount + failedCount == childrenCount);

            foreach (Node child in this.Children)
            {
                if (child.IsActive)
                {
                    child.CancelWithoutReturnResult();
                }
            }
        }

        protected override void DoChildStopped(Node child, bool result)
        {
            runningCount--;
            if (result)
            {
                succeededCount++;
            }
            else
            {
                failedCount++;
            }
            this.childrenResults[child] = result;

            bool allChildrenStarted = runningCount + succeededCount + failedCount == childrenCount;
            if (allChildrenStarted)
            {
                if (runningCount == 0)
                {
                    if (!this.childrenAborted) // if children got aborted because rule was evaluated previously, we don't want to override the successState 
                    {
                        if (failurePolicy == Policy.ONE && failedCount > 0)
                        {
                            successState = false;
                        }
                        else if (successPolicy == Policy.ONE && succeededCount > 0)
                        {
                            successState = true;
                        }
                        else if (successPolicy == Policy.ALL && succeededCount == childrenCount)
                        {
                            successState = true;
                        }
                        else
                        {
                            successState = false;
                        }
                    }
                    Stopped(successState);
                }
                else if (!this.childrenAborted)
                {
                    Debug.Assert(succeededCount != childrenCount);
                    Debug.Assert(failedCount != childrenCount);

                    if (failurePolicy == Policy.ONE && failedCount > 0/* && waitForPendingChildrenRule != Wait.ON_FAILURE && waitForPendingChildrenRule != Wait.BOTH*/)
                    {
                        successState = false;
                        childrenAborted = true;
                    }
                    else if (successPolicy == Policy.ONE && succeededCount > 0/* && waitForPendingChildrenRule != Wait.ON_SUCCESS && waitForPendingChildrenRule != Wait.BOTH*/)
                    {
                        successState = true;
                        childrenAborted = true;
                    }

                    if (childrenAborted)
                    {
                        foreach (Node currentChild in this.Children)
                        {
                            if (currentChild.IsActive)
                            {
                                currentChild.CancelWithoutReturnResult();
                            }
                        }
                    }
                }
            }
        }
/// <summary>
/// 
/// </summary>
/// <param name="abortForChild"></param>
/// <param name="immediateRestart">true：重新启用当前子节点  ;false：报错</param>
/// <exception cref="Exception"></exception>
        public override void StopLowerPriorityChildrenForChild(Node abortForChild, bool immediateRestart)
        {
            if (immediateRestart)
            {
                Assert.IsFalse(abortForChild.IsActive);
                if (childrenResults[abortForChild])
                {
                    succeededCount--;
                }
                else
                {
                    failedCount--;
                }
                runningCount++;
                abortForChild.Start();
            }
            else
            {
                throw new Exception("On Parallel Nodes all children have the same priority, thus the method does nothing if you pass false to 'immediateRestart'!");
            }
        }
    }
}