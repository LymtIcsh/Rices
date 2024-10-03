//此文件格式由工具自动生成

using System;
using Cysharp.Threading.Tasks;
using Cysharp.Threading.Tasks.Triggers;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.AI;
using UnityGameFramework.Runtime;

namespace Suture
{
    [Title("NavigationAI", TitleAlignment = TitleAlignments.Centered)]
    public class NP_NavigationAIAction : NP_ClassForStoreAction
    {
        [LabelText("移动位置")] public NP_BlackBoardRelationData pos = new NP_BlackBoardRelationData() { };

        [LabelText("停止追击范围")] public NP_BlackBoardRelationData PursuitDistance = new NP_BlackBoardRelationData() { };
        

        private NavMeshAgent _navMeshAgent;
        private Animator animator;

        public override Action GetActionToBeDone()
        {
            this.Action = this.NavigationAI;
            return this.Action;
        }

        public void NavigationAI()
        {
            _navMeshAgent = this.BelongToUnit.transform.GetComponent<NavMeshAgent>();
            animator = this.BelongToUnit.transform.GetComponent<Animator>();


            _navMeshAgent.updatePosition = false;
            _navMeshAgent.updateRotation = false;
            
   
                System.Numerics.Vector3 SV3 =
                    pos.GetBlackBoardValue<System.Numerics.Vector3>(this.BelongtoRuntimeTree.GetBlackboard());
                Vector3 position = new Vector3(SV3.X, SV3.Y, SV3.Z);

                //继续追击
//                Log.Info($"要移动到的位置{position}");


                _navMeshAgent.destination = position;

            
                AnimatorMoveAsync(position).Forget();
            
        }


        #region 动画根节点移动

                
        public async UniTaskVoid AnimatorMoveAsync(Vector3 position)
        {
            while (!_navMeshAgent.isStopped)
            {

                await this.BelongToUnit.GetAsyncAnimatorMoveTrigger().OnAnimatorMoveAsync();


                Vector3 worldDeltaPosition = _navMeshAgent.nextPosition - this.BelongToUnit.transform.position;
                if (worldDeltaPosition.magnitude > _navMeshAgent.radius)
                    _navMeshAgent.nextPosition = this.BelongToUnit.transform.position + 0.9f * worldDeltaPosition;

                UseNav(position);
            }
        }
        
        public void UseNav(Vector3 position) //基于agent位移
        {
            float distanc = (position - this.BelongToUnit.transform.position).sqrMagnitude;
        
        //    Log.Info($"间距{distanc}");
            
            if (distanc > PursuitDistance.GetBlackBoardValue<float>(this.BelongtoRuntimeTree.GetBlackboard()))
            {
                _navMeshAgent.isStopped = false;
                
                //此处赋值在下一帧才会生效
                var pos = animator.rootPosition;
                pos.y = _navMeshAgent.nextPosition.y;
                this.BelongToUnit.transform.position = pos; 
                
                //nav.nextPosition就是agent圆柱状collider的位置
                // 使代理贴合根运动
                _navMeshAgent.nextPosition =position;
        
                // 使用根运动速度驱动导航速度
                _navMeshAgent.velocity = animator.deltaPosition / Time.deltaTime;
        
        
              //  Log.Info($"要移动到的位置{this.BelongToUnit.transform.position}");
            }
            else
            {
                _navMeshAgent.isStopped = true;
            }
        
            Vector3 tarve3 = (position - this.BelongToUnit.transform.position).normalized;
            // 处理转身
            this.BelongToUnit.transform.forward = Vector3.RotateTowards(this.BelongToUnit.transform.forward, tarve3,
                Time.deltaTime * _navMeshAgent.angularSpeed * Mathf.Deg2Rad, float.MaxValue);
        }

        #endregion
    }
}