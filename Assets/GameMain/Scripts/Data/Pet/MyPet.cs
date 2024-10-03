using Cinemachine;
using Unity.VisualScripting;
using UnityEngine;
using UnityGameFramework.Runtime;

namespace Suture
{
    public class MyPet : Pet
    {
        [SerializeField] private MyPetData m_myPetData = null;
        

        
      //  private Vector3 m_TargetPosition = Vector3.zero;

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
     
            
            //当前房间
            this.BelongToRoom = GameEntry.RoomManager.BattleRoom;
            this.BelongToRoom.RoomHolderPlayerId = this._petData.TypeId;
            this.BelongToRoom.RoomName = this.Name;
            this.BelongToRoom.PlayerCount++;

            UnitComponent unitComponent = this.BelongToRoom.GetComponent<UnitComponent>();
            unitComponent .MyUnit = this;
            unitComponent.idUnits.Add(this._petData.TypeId,this);
                

            PlayerAssetsInputs _playerAssetsInputs = this.GetComponent<PlayerAssetsInputs>();
            _playerAssetsInputs.cursorLocked = true;
            _playerAssetsInputs.cursorInputForLook = true;


            CinemachineVirtualCamera cinemachineFreeLook = GameObject.FindGameObjectWithTag("PlayerFollowCamera")
                    .GetComponent<CinemachineVirtualCamera>();
            cinemachineFreeLook.Follow =
                    this.Entity.transform.Find("PlayerCameraRoot").transform;
            cinemachineFreeLook.LookAt = this.Entity.transform.Find("LookAt").transform;

            this.AddComponent<DataModifierComponent>();
            this.AddComponent<NP_SyncComponent>();
            this.AddComponent<NumericComponent>();


            //增加Buff管理组件
            this.AddComponent<BuffManagerComponent>();
            this.AddComponent<SkillCanvasManagerComponent>();

            this.AddComponent<NP_RuntimeTreeManager>();
            this.AddComponent<UnitAttributesDataComponent>();

            this.GetComponent<PlayerThirdPersonController>().MoveSpeed = this
                    .GetComponent<UnitAttributesDataComponent>().GetAttribute(NumericType.Speed) / 100;
               
            NP_RuntimeTreeFactory.CreateSkillNpRuntimeTree(this, 10003, 10003).Start();
            //  NP_RuntimeTreeFactory.CreateSkillNpRuntimeTree(m_myPet, 10002, 10002).Start();


            NumericComponent NumericComponent = this.GetComponent<NumericComponent>();

            
        }
        
#if UNITY_2017_3_OR_NEWER
        protected override void OnUpdate(float elapseSeconds, float realElapseSeconds)
#else
        protected internal override void OnUpdate(float elapseSeconds, float realElapseSeconds)
#endif
        {
                 base.OnUpdate(elapseSeconds,realElapseSeconds);
                 
                 
        }


        protected override void StartFsm()
        {
               // base.StartFsm();
                m_fsm.Start<IdleState>();
        }

        protected override void AddFsmState()
        {
             //   base.AddFsmState();
                
                m_States.Add( IdleState.Create());
                m_States.Add( RunState.Create());
                m_States.Add( RunEndState.Create());
                m_States.Add( TurnBackState.Create());
                m_States.Add( EvadeState.Create());
                m_States.Add( NormalAttackState.Create());
                m_States.Add( NormalAttackEndState.Create());
        }
    }
}