using Cinemachine;
using GameFramework.Event;
using NBC.ActionEditor;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;


namespace Suture
{
    /// <summary>
    /// 技能预览
    /// </summary>
    [NBC.ActionEditor.CustomPreview(typeof(PlaySkillGraph))]
    public class PlaySkillGraphPreview : PreviewBase<PlaySkillGraph>
    {
        public GameObject myPet = null;

        public override void Update(float time, float previousTime)
        {
            if (myPet == null)
            {
                return;
            }

            if (myPet != null && !myPet.activeSelf)
            {
                myPet.SetActive(true);
            }
        }

        public override void Enter()
        {



            if (myPet == null)
            {
                //创建技能。
                //实际业务建议自行编写技能对象池
                CreateSkillNpRuntimeTree();
            }

            Play();
            
        }

        public override void Exit()
        {
            if (myPet != null)
            {
                myPet.gameObject.SetActive(false);
            }
            
            foreach (var skillTree in myPet.GetComponent<NP_RuntimeTreeManager>().RuntimeTrees)
            {
                skillTree.Value.Finish().Forget();
            }
        }

        protected void Play()
        {
            foreach (var skillTree in myPet.GetComponent<NP_RuntimeTreeManager>().RuntimeTrees)
            {
                skillTree.Value.GetBlackboard().Set("PlayerInput", "E", true, true);
                skillTree.Value.GetBlackboard().Set("SkillTargetAngle", Quaternion.identity, true, true);
            }
        }

        void CreateSkillNpRuntimeTree()
        {


                
                //演示代码只演示原地播放，挂点播放等需要自行编写挂点相关脚本和设置挂点
            
            myPet = Object.Instantiate(clip.Model);
            
            myPet.transform.position = Vector3.zero;
            
   MyPet m_=  myPet.GetOrAddComponent<MyPet>();
            //当前房间
            m_.BelongToRoom =new GameObject().AddComponent<Room>();
           

            myPet.AddComponent<DataModifierComponent>();
            myPet.AddComponent<NP_SyncComponent>();
            myPet.AddComponent<NumericComponent>();

            //增加栈式状态机，辅助动画切换
         //   myPet.AddComponent<StackFsmComponent>();
            //增加Buff管理组件
            myPet.AddComponent<BuffManagerComponent>();
            myPet.AddComponent<SkillCanvasManagerComponent>();

            myPet.AddComponent<NP_RuntimeTreeManager>();
            myPet.AddComponent<UnitAttributesDataComponent>();

      
            NP_RuntimeTreeFactory.CreateSkillNpRuntimeTree(m_, 10002, 10002).Start();
        }

        
    }
}