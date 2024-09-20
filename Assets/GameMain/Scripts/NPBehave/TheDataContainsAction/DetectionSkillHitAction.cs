//此文件格式由工具自动生成

using System;
using Sirenix.OdinInspector;

namespace Suture
{
   

    [Title("检测技能是否中", TitleAlignment = TitleAlignments.Centered)]
    public class NP_DetectionSkillHitAction : NP_ClassForStoreAction
    {
        [LabelText("检测类型")] public DetectionEnum _DetectionEnum = DetectionEnum.Ray;
        
        public override Action GetActionToBeDone()
        {
            this.Action = this.DetectionSkillHit;
            return this.Action;
        }

        public void DetectionSkillHit()
        {
            switch (_DetectionEnum)
            {
                case DetectionEnum.Ray:
                    break;
                case DetectionEnum.Fanshaped:
                    break;
                case DetectionEnum.Circle:
                    break;
                case DetectionEnum.Triangle:
                    break;
                case DetectionEnum.Sector:
                    break;
                case DetectionEnum.Ring:
                    break;
                case DetectionEnum.Orthogon:
                    break;
                default:
                    break;
            }
        }
    }
}