using Sirenix.OdinInspector;
using UnityEngine;

namespace Suture.AnimationCore
{
    public class PlayAnimInfo
    {
        /// <summary>
        /// 三种AvatarMask，指越大，优先级越高
        /// </summary>
        public enum AvatarMaskType
        {
            /// <summary>
            /// 默认
            /// </summary>
            [LabelText("默认")] None,

            /// <summary>
            /// 上半身不听使唤
            /// </summary>
            [LabelText("上半身不听使唤")] AnimMask_UpNotAffect,

            /// <summary>
            /// 下半身不听使唤
            /// </summary>
            [LabelText("下半身不听使唤")] AnimMask_DownNotAffect
        }
        
        // /// <summary>
        // /// 要设置的运行时动画机的类型
        // /// </summary>
        // [LabelText("要设置的运行时动画机的类型")]
        // public string StateTypes;

        /// <summary>
        /// 要播放的动画名称
        /// </summary>
        [LabelText("要播放的动画名称")]
        public string AnimationClipName;

        [LabelText("要占用的AvatarMask")]
        public AvatarMaskType OccAvatarMaskType;

        /// <summary>
        /// 过渡时间
        /// </summary>
        [LabelText("过渡时间")]
        [Tooltip("这个过度时间指从其他动画过渡到本身所用时间")]
        public float FadeOutTime;
        
        /// <summary>
        /// 偏移时间
        /// </summary>
        [LabelText("偏移时间")]
        [Tooltip("状态的时间")]
        public float FadeOffsetTime;

        [LabelText("是否打断当前运行动画")] public bool IsStopCurrentRunuing;
    }
}