//此文件格式由工具自动生成

using System;
using System.Collections.Generic;
using GameFramework;
using UnityGameFramework.Runtime;

namespace Suture
{
    public class CDInfo : IReference
    {
        /// <summary>
        /// 名称
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 时间间隔（CD），一般修改的都不是这个Interval
        /// </summary>
        public long Interval { get; set; }

        /// <summary>
        /// 剩余CD时长
        /// </summary>
        public long RemainCDLength { get; set; }

        /// <summary>
        /// 这个CD将在这一帧转好
        /// </summary>
        public uint TargetTriggerCDFrame;

        /// <summary>
        /// CD是否转好了
        /// </summary>
        public bool Result { get; set; }

        /// <summary>
        /// CD信息变化时的回调
        /// </summary>
        public Action<CDInfo> CDChangedCallBack;

        public void Init(string name, uint currentFrame, long cdLength, Action<CDInfo> cDChangedCallBack = null)
        {
            this.Name = name;
            this.Interval = cdLength;
            this.RemainCDLength = cdLength;
            this.Result = true;
            this.CDChangedCallBack = cDChangedCallBack;
            this.TargetTriggerCDFrame = currentFrame + TimeAndFrameConverter.Frame_Long2Frame(cdLength);
        }

        public void Clear()
        {
            this.Name = null;
            this.Interval = 0;
            this.RemainCDLength = 0;
            this.Result = false;
            this.CDChangedCallBack = null;
            this.TargetTriggerCDFrame = 0;
        }
    }

    public class CDComponent : EntityBase
    {
        #region 私有成员

        public Dictionary<long, Dictionary<string, CDInfo>>
            CDInfos = new Dictionary<long, Dictionary<string, CDInfo>>();

        #endregion

        #region 公有成员

        public static CDComponent Instance;

        #endregion

        #region 生命周期函数

        public void Awake()
        {
            Instance = this;
        }

        public void FixedUpdate()
        {
            //此处填写FixedUpdate逻辑
            foreach (var cdInfoDic in this.CDInfos)
            {
                foreach (var cdInfo in cdInfoDic.Value)
                {
                    if (!cdInfo.Value.Result)
                    {
                        cdInfo.Value.RemainCDLength -= 33;
                        if ((uint)TimeInfo.Instance.ClientFrameTime() >= cdInfo.Value.TargetTriggerCDFrame)
                        {
                            cdInfo.Value.Result = true;
                            cdInfo.Value.CDChangedCallBack?.Invoke(cdInfo.Value);
                            Log.Info(
                                $"{cdInfo.Value.Name}  冷却好了  当前{(uint)TimeInfo.Instance.ClientFrameTime()}  目标cd帧{cdInfo.Value.TargetTriggerCDFrame}");
                        }
                    }
                }
            }
        }

        protected override void OnRecycle()
        {
            foreach (var cdInfoList in CDInfos)
            {
                foreach (var cdInfo in cdInfoList.Value)
                {
                    ReferencePool.Release(cdInfo.Value);
                }

                cdInfoList.Value.Clear();
            }

            CDInfos.Clear();
            base.OnRecycle();
        }

        #endregion

        /// <summary>
        /// 新增一条CD数据
        /// </summary>
        public CDInfo AddCDData(long id, string name, long cDLength, Action<CDInfo> onCDChangedCallback = null)
        {
            if (this.GetCDData(id, name) != null)
            {
                Log.Error($"已注册id为：{id}，Name为：{name}的CD信息，请勿重复注册");
                return null;
            }

            CDInfo cdInfo = ReferencePool.Acquire<CDInfo>();
            cdInfo.Init(name, (uint)TimeInfo.Instance.ClientFrameTime(), cDLength, onCDChangedCallback);
            AddCDData(id, cdInfo);
            return cdInfo;
        }

        /// <summary>
        /// 新增一条CD数据
        /// </summary>
        /// <param name="id"></param>
        /// <param name="cdInfo"></param>
        public CDInfo AddCDData(long id, CDInfo cdInfo)
        {
            if (this.CDInfos.TryGetValue(id, out var cdInfoDic))
            {
                cdInfoDic.Add(cdInfo.Name, cdInfo);
            }
            else
            {
                CDInfos.Add(id, new Dictionary<string, CDInfo>() { { cdInfo.Name, cdInfo } });
            }

            return cdInfo;
        }

        /// <summary>
        /// 触发某个CD，使其进入CD状态
        /// </summary>
        /// <param name="id"></param>
        /// <param name="name"></param>
        /// <param name="cdLength">CD长度</param>
        public void TriggerCD(long id, string name, long cdLength = -1)
        {
            CDInfo cdInfo = GetCDData(id, name);
            cdInfo.Result = false;
            cdInfo.RemainCDLength = cdLength == -1 ? cdInfo.Interval : cdLength;

            cdInfo.TargetTriggerCDFrame = (uint)TimeInfo.Instance.ClientFrameTime() +
                                          TimeAndFrameConverter.Frame_Long2Frame(cdInfo.RemainCDLength);
        }

        /// <summary>
        /// 获取CD数据
        /// </summary>
        /// <param name="id"></param>
        /// <param name="name"></param>
        public CDInfo GetCDData(long id, string name)
        {
            if (this.CDInfos.TryGetValue(id, out var cdInfoDic))
            {
                if (cdInfoDic.TryGetValue(name, out var cdInfo))
                {
                    return cdInfo;
                }
            }

            return null;
        }

        /// <summary>
        /// 增加CD时间到指定CD
        /// </summary>
        public void AddCD(long id, string name, long addedCDLength)
        {
            CDInfo cdInfo = GetCDData(id, name);
            cdInfo.RemainCDLength += addedCDLength;
            cdInfo.TargetTriggerCDFrame += TimeAndFrameConverter.Frame_Long2Frame(addedCDLength);
            cdInfo.CDChangedCallBack?.Invoke(cdInfo);
        }

        /// <summary>
        /// 减少CD时间到指定CD
        /// </summary>
        public void ReduceCD(long id, string name, long reducedCDLength)
        {
            CDInfo cdInfo = GetCDData(id, name);
            cdInfo.RemainCDLength -= reducedCDLength;

            int tempFrame = (int)cdInfo.TargetTriggerCDFrame;
            int result = tempFrame - (int)TimeAndFrameConverter.Frame_Long2Frame(reducedCDLength);
            cdInfo.TargetTriggerCDFrame = result <= 0 ? (uint)0 : (uint)result;
            cdInfo.CDChangedCallBack?.Invoke(cdInfo);
        }

        /// <summary>
        /// 直接重设CD数据以及CD的剩余时长
        /// </summary>
        public void SetCD(long id, string name, long cDLength, long remainCDLength)
        {
            CDInfo cdInfo = GetCDData(id, name);
            if (cdInfo == null)
            {
                cdInfo = this.AddCDData(id, name, cDLength, null);
            }

            cdInfo.Interval = cDLength;
            cdInfo.RemainCDLength = remainCDLength;
            cdInfo.TargetTriggerCDFrame =
                (uint)TimeInfo.Instance.ClientFrameTime() + TimeAndFrameConverter.Frame_Long2Frame(remainCDLength);

            Log.Info(
                $"设置 {cdInfo.Name} 技能cd 当前帧{(uint)TimeInfo.Instance.ClientFrameTime()} cd总帧{TimeAndFrameConverter.Frame_Long2Frame(remainCDLength)}   cd冷却完成帧{cdInfo.TargetTriggerCDFrame}    当前时间{TimeInfo.Instance.ToDateTime((uint)TimeInfo.Instance.ClientFrameTime()).ToString("T")}    cd结束时间{TimeInfo.Instance.ToDateTime(cdInfo.TargetTriggerCDFrame).ToString("T")}");

            cdInfo.Result = false;
            cdInfo.CDChangedCallBack?.Invoke(cdInfo);
        }

        /// <summary>
        /// 获取CD结果
        /// </summary>
        /// <param name="id"></param>
        /// <param name="name"></param>
        /// <returns></returns>
        public bool GetCDResult(long id, string name)
        {
            if (this.CDInfos.TryGetValue(id, out var cdInfoDic))
            {
                if (cdInfoDic.TryGetValue(name, out var cdInfo))
                {
                    return cdInfo.Result;
                }
            }

            Log.Error($"尚未注册id为：{id}，Name为：{name}的CD信息");
            return false;
        }

        /// <summary>
        /// 移除一条CD数据
        /// </summary>
        /// <param name="id"></param>
        /// <param name="name"></param>
        public void RemoveCDData(long id, string name)
        {
            if (this.CDInfos.TryGetValue(id, out var cdInfoDic))
            {
                cdInfoDic.Remove(name);
            }
        }

        public void ResetCD(long belongToUnitId, string cdName)
        {
            CDInfo cdInfo = GetCDData(belongToUnitId, cdName);
            cdInfo.RemainCDLength = 0;
            cdInfo.TargetTriggerCDFrame = 0;
            cdInfo.Result = true;
        }
    }
}