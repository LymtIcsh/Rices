using System;
using Cysharp.Threading.Tasks;
using Cysharp.Threading.Tasks.Triggers;
using GameFramework;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;
using UnityGameFramework.Runtime;

namespace Suture
{
    public class SkillMushroom : SkillObjBase
    {
        [SerializeField] private SkillMushroomData _skillMushroomData = null;

        #region 生命周期

        protected override void OnInit(object userData)
        {
            base.OnInit(userData);
        }

        protected override void OnShow(object userData)
        {
            base.OnShow(userData);

            _skillMushroomData = (SkillMushroomData)userData;
            if (_skillMushroomData == null)
            {
                Log.Error("SkillMushroom data is invalid.");
                return;
            }

            Name = Utility.Text.Format(_skillMushroomData.AssetName, Id);

            CollisionEnterAsync().Forget();


            if (navigationAICancellationTokenSource != null)
            {
                navigationAICancellationTokenSource.Dispose();
            }

            GetComponent<SphereCollider>().radius = 9;

            // Log.Info(this.GetComponentInChildren<SphereCollider>().gameObject.name);

          GameEntry.RangeCheck.NavigationAIEnter(this.gameObject, "Enemy", 1.0f, async (curretCollider) =>
          {
              var position =  curretCollider.transform.position;
              _navMeshAgent.SetDestination(position);

              distanc = Vector3.SqrMagnitude(position - transform.position);
              
              await UniTask.Yield();
              
              if (distanc <= 10)
              {
                  _navMeshAgent.isStopped = false;

                  Dead(0).Forget();
              }
          }).Forget();
        }

        protected override void OnUpdate(float elapseSeconds, float realElapseSeconds)
        {
            base.OnUpdate(elapseSeconds, realElapseSeconds);
        }

        protected override void OnHide(bool isShutdown, object userData)
        {
            base.OnHide(isShutdown, userData);
        }


        protected override void OnDead(EntityBase attacker)
        {
            base.OnDead(attacker);
        }

        #endregion

        public async UniTaskVoid CollisionEnterAsync()
        {
        
            var (hasResultLeft, result) = await UniTask.WhenAny(
                transform.GetAsyncTriggerEnterTrigger().OnTriggerEnterAsync(),
                UniTask.WaitForSeconds(2));

            if (hasResultLeft && string.Equals(result.name, "Mushroom"))
            {
               // Log.Info(_skillMushroomData.belongPet.transform.forward.normalized);
                this.transform.position += _skillMushroomData.belongPet.transform.forward.normalized * 3;
                CollisionEnterAsync().Forget();
            }

            //
            // Collider[] collider = new Collider[10];
            // Physics.OverlapSphereNonAlloc(Vector3.zero, 1, collider, 1 << LayerMask.NameToLayer("Mushroom"));
            //
            // Log.Info(Physics.OverlapSphereNonAlloc(Vector3.zero, 1, collider, 1 << LayerMask.NameToLayer("Mushroom")));
            // // foreach (var c in collider)
            // // {
            // //     Log.Info(c?.name);
            // // }
        }
    }
}