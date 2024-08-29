//------------------------------------------------------------
// 此代码由工具自动生成，请勿更改
// 此代码由工具自动生成，请勿更改
// 此代码由工具自动生成，请勿更改
//------------------------------------------------------------

//using ETModel;
//using Plugins;
using UnityEditor;
using GraphProcessor;
using Plugins.NodeEditor;

namespace Suture
{
    [NodeMenuItem("技能数据部分/治疗Buff", typeof (SkillGraph))]
    public class TreatmentBuffNode: BuffNodeBase
    {
        public override string name => "治疗Buff";

        public NormalBuffNodeData SkillBuffBases =
                new NormalBuffNodeData()
                {
                    BuffDes = "治疗Buff",
                    BuffData = new TreatmentBuffData() {  }
                };

        public override BuffNodeDataBase GetBuffNodeData()
        {
            return SkillBuffBases;
        }
    }
}
