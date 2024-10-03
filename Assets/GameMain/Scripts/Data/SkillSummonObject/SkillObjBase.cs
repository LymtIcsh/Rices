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

        protected override void OnDead(EntityBase attacker)
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

       
        
    }
}