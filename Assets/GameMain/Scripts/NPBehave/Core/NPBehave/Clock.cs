using System.Collections.Generic;
using GameFramework;
using UnityEngine.Assertions;

namespace NPBehave
{
    public class Clock
    {
        private List<System.Action> updateObservers = new List<System.Action>();
        private Dictionary<System.Action, Timer> timers = new Dictionary<System.Action, Timer>();
        private HashSet<System.Action> removeObservers = new HashSet<System.Action>();
        private HashSet<System.Action> addObservers = new HashSet<System.Action>();
        private HashSet<System.Action> removeTimers = new HashSet<System.Action>();
        private Dictionary<System.Action, Timer> addTimers = new Dictionary<System.Action, Timer>();
        private bool isInUpdate = false;

        class Timer
        {
            public double scheduledTime = 0f;
            public int repeat = 0;
            public bool used = false;
            public double delay = 0f;
            public float randomVariance = 0.0f;

            public void ScheduleAbsoluteTime(double elapsedTime)
            {
                scheduledTime = elapsedTime + delay - randomVariance * 0.5f + randomVariance * UnityEngine.Random.value;
            }
        }

        private double elapsedTime = 0f;

        private List<Timer> timerPool = new List<Timer>();
        private int currentTimerPoolIndex = 0;

        /// <summary>
        /// 将要被添加的帧事件
        /// </summary>
        private Dictionary<long, FrameAction> ToBeAddedFrameActions = new Dictionary<long, FrameAction>();

        /// <summary>
        /// 将要被移除的帧事件
        /// </summary>
        private List<long> ToBeRemovedFrameActions = new List<long>();

        /// <summary>
        /// 所有的帧事件
        /// </summary>
        private Dictionary<long, FrameAction> AllFrameActions = new Dictionary<long, FrameAction>();

        /// <summary>Register a timer function , 因为这个函数可能会在回滚的时候调用，所以要强行传递一个currentFrame</summary>
        /// <param name="intervalFrame">time in milliseconds</param>
        /// <param name="repeat">number of times to repeat, set to -1 to repeat until unregistered.</param>
        /// <param name="action">method to invoke</param>
        public long AddTimer(uint intervalFrame, System.Action action, int repeat = 1)
        {
            FrameAction frameAction = ReferencePool.Acquire<FrameAction>();
            frameAction.Id = action.GetHashCode();
            frameAction.Action = action;
            frameAction.RepeatTime = repeat;
            frameAction.IntervalFrame = intervalFrame;

            // CalculateTimerFrame(frameAction);
            AddTimer(frameAction);

            return frameAction.Id;
        }

        private void AddTimer(FrameAction frameAction)
        {
            if (!isInUpdate)
            {
                if (AllFrameActions.TryGetValue(frameAction.Id, out var result))
                {
                    if (result.TargetTickFrame > frameAction.TargetTickFrame)
                    {
                        result.TargetTickFrame = frameAction.TargetTickFrame;
                    }
                }
                else
                {
                    AllFrameActions[frameAction.Id] = frameAction;
                }
            }
        }

        /// <summary>Register a timer function </summary>
        /// <param name="time">time in milliseconds</param>
        /// <param name="repeat">number of times to repeat, set to -1 to repeat until unregistered.</param>
        /// <param name="action">method to invoke</param>
        public void AddTimer(float time, int repeat, System.Action action)
        {
            AddTimer(time, 0f, repeat, action);
        }

        /// <summary>Register a timer function with random variance</summary>
        /// <param name="time">time in milliseconds</param>
        /// <param name="randomVariance">deviate from time on a random basis</param>
        /// <param name="repeat">number of times to repeat, set to -1 to repeat until unregistered.</param>
        /// <param name="action">method to invoke</param>
        public void AddTimer(float delay, float randomVariance, int repeat, System.Action action)
        {
            Timer timer = null;

            if (!isInUpdate)
            {
                if (!this.timers.ContainsKey(action))
                {
                    this.timers[action] = getTimerFromPool();
                }

                timer = this.timers[action];
            }
            else
            {
                if (!this.addTimers.ContainsKey(action))
                {
                    this.addTimers[action] = getTimerFromPool();
                }

                timer = this.addTimers[action];

                if (this.removeTimers.Contains(action))
                {
                    this.removeTimers.Remove(action);
                }
            }

            Assert.IsTrue(timer.used);
            timer.delay = delay;
            timer.randomVariance = randomVariance;
            timer.repeat = repeat;
            timer.ScheduleAbsoluteTime(elapsedTime);
        }


        public void RemoveTimer(long id)
        {
            if (!isInUpdate)
            {
                if (this.AllFrameActions.TryGetValue(id, out var frameAction))
                {
                    this.AllFrameActions.Remove(id);
                }
            }
            else
            {
                this.ToBeRemovedFrameActions.Add(id);
            }
        }

        public void RemoveTimer(System.Action action)
        {
            if (!isInUpdate)
            {
                if (this.timers.ContainsKey(action))
                {
                    timers[action].used = false;
                    this.timers.Remove(action);
                }
            }
            else
            {
                if (this.timers.ContainsKey(action))
                {
                    this.removeTimers.Add(action);
                }

                if (this.addTimers.ContainsKey(action))
                {
                    Assert.IsTrue(this.addTimers[action].used);
                    this.addTimers[action].used = false;
                    this.addTimers.Remove(action);
                }
            }
        }

        public void Update()
        {
            this.isInUpdate = true;
            foreach (var frameActionPair in AllFrameActions)
            {
                FrameAction frameAction = frameActionPair.Value;
                if ( /*frameAction.TargetTickFrame<=LsfComponent.CurrentFrame &&*/
                    !this.ToBeRemovedFrameActions.Contains(frameAction.Id))
                {
                    frameAction.Action.Invoke();

                    if (frameAction.RepeatTime != -1 && --frameAction.RepeatTime <= 0)
                    {
                        RemoveTimer(frameAction.Id);
                    }
                    else
                    {
                        //CalculateTimerFrame(frameAction);
                    }
                }
            }

            this.isInUpdate = false;

            foreach (var frameActionId in this.ToBeRemovedFrameActions)
            {
                RemoveTimer(frameActionId);
            }

            foreach (var frameActionPair in this.ToBeAddedFrameActions)
            {
                AddTimer(frameActionPair.Value);
            }
            
            this.ToBeAddedFrameActions.Clear();
            this.ToBeRemovedFrameActions.Clear();
        }
        
        
        // /// <summary>
        // /// 支持传递一个自定义currentFrame进来，用于回滚时精确调用
        // /// </summary>
        // /// <param name="frameAction"></param>
        // /// <param name="currentFrame"></param>
        // private void CalculateTimerFrame(FrameAction frameAction)
        // {
        //     frameAction.TargetTickFrame = LsfComponent.CurrentFrame + frameAction.IntervalFrame;
        // }

        public bool HasTimer(System.Action action)
        {
            if (!isInUpdate)
            {
                return this.timers.ContainsKey(action);
            }
            else
            {
                if (this.removeTimers.Contains(action))
                {
                    return false;
                }
                else if (this.addTimers.ContainsKey(action))
                {
                    return true;
                }
                else
                {
                    return this.timers.ContainsKey(action);
                }
            }
        }

        /// <summary>Register a function that is called every frame</summary>
        /// <param name="action">function to invoke</param>
        public void AddUpdateObserver(System.Action action)
        {
            if (!isInUpdate)
            {
                this.updateObservers.Add(action);
            }
            else
            {
                if (!this.updateObservers.Contains(action))
                {
                    this.addObservers.Add(action);
                }

                if (this.removeObservers.Contains(action))
                {
                    this.removeObservers.Remove(action);
                }
            }
        }

        public void RemoveUpdateObserver(System.Action action)
        {
            if (!isInUpdate)
            {
                this.updateObservers.Remove(action);
            }
            else
            {
                if (this.updateObservers.Contains(action))
                {
                    this.removeObservers.Add(action);
                }

                if (this.addObservers.Contains(action))
                {
                    this.addObservers.Remove(action);
                }
            }
        }

        public bool HasUpdateObserver(System.Action action)
        {
            if (!isInUpdate)
            {
                return this.updateObservers.Contains(action);
            }
            else
            {
                if (this.removeObservers.Contains(action))
                {
                    return false;
                }
                else if (this.addObservers.Contains(action))
                {
                    return true;
                }
                else
                {
                    return this.updateObservers.Contains(action);
                }
            }
        }

        public void Update(float deltaTime)
        {
            this.elapsedTime += deltaTime;

            this.isInUpdate = true;

            foreach (System.Action action in updateObservers)
            {
                if (!removeObservers.Contains(action))
                {
                    action.Invoke();
                }
            }

            Dictionary<System.Action, Timer>.KeyCollection keys = timers.Keys;
            foreach (System.Action callback in keys)
            {
                if (this.removeTimers.Contains(callback))
                {
                    continue;
                }

                Timer timer = timers[callback];
                if (timer.scheduledTime <= this.elapsedTime)
                {
                    if (timer.repeat == 0)
                    {
                        RemoveTimer(callback);
                    }
                    else if (timer.repeat >= 0)
                    {
                        timer.repeat--;
                    }

                    callback.Invoke();
                    timer.ScheduleAbsoluteTime(elapsedTime);
                }
            }

            foreach (System.Action action in this.addObservers)
            {
                this.updateObservers.Add(action);
            }

            foreach (System.Action action in this.removeObservers)
            {
                this.updateObservers.Remove(action);
            }

            foreach (System.Action action in this.addTimers.Keys)
            {
                if (this.timers.ContainsKey(action))
                {
                    Assert.AreNotEqual(this.timers[action], this.addTimers[action]);
                    this.timers[action].used = false;
                }

                Assert.IsTrue(this.addTimers[action].used);
                this.timers[action] = this.addTimers[action];
            }

            foreach (System.Action action in this.removeTimers)
            {
                Assert.IsTrue(this.timers[action].used);
                timers[action].used = false;
                this.timers.Remove(action);
            }

            this.addObservers.Clear();
            this.removeObservers.Clear();
            this.addTimers.Clear();
            this.removeTimers.Clear();

            this.isInUpdate = false;
        }

        public int NumUpdateObservers
        {
            get { return updateObservers.Count; }
        }

        public int NumTimers
        {
            get { return timers.Count; }
        }

        public double ElapsedTime
        {
            get { return elapsedTime; }
        }

        private Timer getTimerFromPool()
        {
            int i = 0;
            int l = timerPool.Count;
            Timer timer = null;
            while (i < l)
            {
                int timerIndex = (i + currentTimerPoolIndex) % l;
                if (!timerPool[timerIndex].used)
                {
                    currentTimerPoolIndex = timerIndex;
                    timer = timerPool[timerIndex];
                    break;
                }

                i++;
            }

            if (timer == null)
            {
                timer = new Timer();
                currentTimerPoolIndex = 0;
                timerPool.Add(timer);
            }

            timer.used = true;
            return timer;
        }

        public int DebugPoolSize
        {
            get { return this.timerPool.Count; }
        }
    }
}