using System;
using GameFramework;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;
using UnityGameFramework.Runtime;

namespace Suture
{
    public class BossObject:EnemyObject
    {
        public BossData _BossData;
        
        // private NavMeshAgent _navMeshAgent;
        // private Animator animator;

        protected override void OnInit(object userData)
        {
            base.OnInit(userData);
            // _navMeshAgent =GetComponent<NavMeshAgent>();
            // animator = transform.GetComponent<Animator>();
            // _navMeshAgent.updatePosition = false;
            // _navMeshAgent.updateRotation = false;
        }

        protected override void OnShow(object userData)
        {
            base.OnShow(userData);
            
            _BossData=userData as BossData;
            if (_BossData==null)
            {
                Log.Error("Boss data is invalid.");
                return;
            }
            
            this.AddComponent<DataModifierComponent>();
            this.AddComponent<NP_SyncComponent>();
            this.AddComponent<NumericComponent>();
        
            //增加Buff管理组件
            this.AddComponent<BuffManagerComponent>();
            this.AddComponent<SkillCanvasManagerComponent>();
            
            this.AddComponent<StackAnimationComponent>();
            this.AddComponent<NP_RuntimeTreeManager>();

            
            NP_RuntimeTreeFactory.CreateSkillNpRuntimeTree(this, 10004, 10004).Start();
            //  NP_RuntimeTreeFactory.CreateSkillNpRuntimeTree(m_myPet, 10002, 10002).Start();
            
            
            Name = Utility.Text.Format(_BossData.AssetName, Id);
        }

        // private void OnAnimatorMove()
        // {
        //     _navMeshAgent.velocity = animator.velocity; //此处赋值在下一帧才会生效
        // transform.position = _navMeshAgent.nextPosition; //nav.nextPosition就是agent圆柱状collider的位置
        //
        // }
    }
}