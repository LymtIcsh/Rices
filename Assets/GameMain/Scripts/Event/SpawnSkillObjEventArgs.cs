//------------------------------------------------------------
// 此代码由工具自动生成，请勿更改
// 此代码由工具自动生成，请勿更改
// 此代码由工具自动生成，请勿更改
//------------------------------------------------------------

using System;
using GameFramework;
using GameFramework.Event;

namespace Suture
{
    /// <summary>
    /// 生成技能物体
    /// </summary>
    public class SpawnSkillObjEventArgs : GameEventArgs
    {
        public static readonly int EventID = typeof(SpawnSkillObjEventArgs).GetHashCode();

        public override int Id => EventID;
        
        public Action<Entity> ShowSuccess
        {
            get;
            private set;
        }

        public SkillObjDataBase SkillObjData
        {
            get;
            private set;
        }

        public SpawnSkillObjEventArgs()
        {
            ShowSuccess = null;
            SkillObjData = null;
        }

        public override void Clear()
        {
            ShowSuccess = null;
            SkillObjData = null;
        }


        public static SpawnSkillObjEventArgs Create(Action<Entity> showSuccess, SkillObjDataBase skillObjDataData, object userData = null)
        {
            SpawnSkillObjEventArgs ne = ReferencePool.Acquire<SpawnSkillObjEventArgs>();
            ne.ShowSuccess = showSuccess;
            ne.SkillObjData = skillObjDataData;
            return ne;
        }
    }
}