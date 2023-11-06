using UnityEngine;
using UnityGameFramework.Runtime;

namespace Suture
{
    public class MyPet : Pet
    {
        [SerializeField] private MyPetData m_myPetData = null;

        private Vector3 m_TargetPosition = Vector3.zero;

#if UNITY_2017_3_OR_NEWER
        protected override void OnInit(object userData)
#else
        protected internal override void OnInit(object userData)
#endif
        {
            base.OnInit(userData);
        }

#if UNITY_2017_3_OR_NEWER
        protected override void OnShow(object userData)
#else
        protected internal override void OnShow(object userData)
#endif
        {
            base.OnShow(userData);

            m_myPetData = userData as MyPetData;
            if (m_myPetData == null)
            {
                    Log.Error("My pet data is invalid.");
                    return;
            }
        }
        
#if UNITY_2017_3_OR_NEWER
        protected override void OnUpdate(float elapseSeconds, float realElapseSeconds)
#else
        protected internal override void OnUpdate(float elapseSeconds, float realElapseSeconds)
#endif
        {
                 base.OnUpdate(elapseSeconds,realElapseSeconds);
                 
                 
        }
        
        
    }
}