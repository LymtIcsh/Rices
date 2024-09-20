using System.Threading;
using Cysharp.Threading.Tasks;
using Cysharp.Threading.Tasks.Triggers;
using UnityEngine;
using UnityEngine.AI;
using UnityGameFramework.Runtime;

namespace Suture
{
    public class SkillObjBase : TargetableObject
    {
        private SkillObjDataBase _skillObjDataBase = null;

        public NavMeshAgent _navMeshAgent;

        protected Collider curretCollider;
        

        protected CancellationTokenSource navigationAICancellationTokenSource = new CancellationTokenSource();

        protected float distanc;

        public override ImpactData GetImpactData()
        {
            return new ImpactData();
        }

        protected override void OnInit(object userData)
        {
            base.OnInit(userData);


            _navMeshAgent = GetComponent<NavMeshAgent>();
        }

        protected override void OnShow(object userData)
        {
            base.OnShow(userData);

            _skillObjDataBase = userData as SkillObjDataBase;
            if (_skillObjDataBase == null)
            {
                Log.Error("SkillObjDataBase data is invalid.");
                return;
            }

            // NavigationAI().Forget();
            // Dead().Forget();
        }

        protected override void OnDead(Entity attacker)
        {
            if (navigationAICancellationTokenSource.IsCancellationRequested)
            {
                navigationAICancellationTokenSource.Cancel();
            }


            base.OnDead(attacker);
        }

        protected override void OnRecycle()
        {
            if (navigationAICancellationTokenSource.IsCancellationRequested)
            {
                navigationAICancellationTokenSource.Cancel();
                navigationAICancellationTokenSource.Dispose();
            }

            base.OnRecycle();
        }

        /// <summary>
        /// 死亡
        /// </summary>
        ///         <param name="time">多久死亡</param>
        public virtual async UniTaskVoid Dead(float time)
        {
            await UniTask.WaitForSeconds(time);
            OnDead(this);
        }

        /// <summary>
        /// 进入检测范围，开始寻路
        /// </summary>
        ///   <param name="tag">检测tag</param>
        /// <param name="time">检测间隔</param>
        public virtual async UniTaskVoid NavigationAIEnter(GameObject trigetObj, string tag, float time)
        {
            do
            {
                curretCollider = await trigetObj.GetAsyncTriggerEnterTrigger().OnTriggerEnterAsync();
                Log.Info(curretCollider.name);
            } while (!curretCollider.CompareTag(tag));


            NavigationAIStay(trigetObj, tag, time).Forget();
            NavigationAIExit(trigetObj, tag, time).Forget();

            Log.Info("进入检测");
        }

        /// <summary>
        /// 一直在检测范围中，开始寻路
        /// </summary>
        /// <param name="time">检测间隔</param>
        public virtual async UniTaskVoid NavigationAIStay(GameObject trigetObj, string tag, float time)
        {
            do
            {
                curretCollider = await trigetObj.GetAsyncTriggerStayTrigger().OnTriggerStayAsync();
            } while (!curretCollider.CompareTag(tag));

            // if (curretCollider.CompareTag(tag))
            // {
            var position = curretCollider.transform.position;
            _navMeshAgent.SetDestination(position);

            distanc = Vector3.SqrMagnitude(position - transform.position);

            if (distanc <= 10)
            {
                _navMeshAgent.isStopped = true;

                Dead(0).Forget();
            }
            // }


            await UniTask.WaitForSeconds(time);
            NavigationAIStay(trigetObj, tag, time).Forget();

            Log.Info("持续检测");
        }

        /// <summary>
        /// 离开检测范围
        /// </summary>
        /// <param name="time">检测间隔</param>
        public virtual async UniTaskVoid NavigationAIExit(GameObject trigetObj, string tag, float time)
        {
            await trigetObj.GetAsyncTriggerExitTrigger().OnTriggerExitAsync();
            curretCollider = null;

            NavigationAIEnter(trigetObj, tag, time).Forget();

            Log.Info("离开检测");
        }
        
    }
}