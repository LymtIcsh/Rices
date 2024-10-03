using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Cinemachine;
using GameFramework.Event;
using Unity.VisualScripting;
using UnityEditor.SceneManagement;
using UnityEngine;
using UnityGameFramework.Runtime;

namespace Suture
{
    public abstract class GameBase
    {
        public abstract GameMode GameMode { get; }

        /// <summary>
        /// 游戏是否结束
        /// </summary>
        public bool GameOver { get; protected set; }

        private MyPet m_myPet = null;

        public virtual void Initialize(int typeId, string name, Vector3 InitPos)
        {
            GameEntry.Event.Subscribe(ShowEntitySuccessEventArgs.EventId, OnShowEntitySuccess);
            GameEntry.Event.Subscribe(ShowEntityFailureEventArgs.EventId, OnShowEntityFailure);
            GameEntry.Event.Subscribe(SpawnSkillObjEventArgs.EventID, OnSpawnSkillObjSuccess);


            GameEntry.Entity.ShowMyPet(new MyPetData(GameEntry.Entity.GenerateSerialId(), typeId)
            {
                  //Name = name,
                //TODO Animator 开启了Apply Root Motion，所以无法设置位置
                Position = InitPos,
                Scale = Vector3.one,
            });
            



            GameOver = false;
            m_myPet = null;
        }

        public virtual void Update(float elapseSeconds, float realElapseSeconds)
        {
            TimeInfo.Instance.Update();
            
            if (m_myPet != null && m_myPet.IsDead)
            {
                GameOver = true;
                return;
            }

        }

        public virtual void Shutdown()
        {
            GameEntry.Event.Unsubscribe(ShowEntitySuccessEventArgs.EventId, OnShowEntitySuccess);
            GameEntry.Event.Unsubscribe(ShowEntityFailureEventArgs.EventId, OnShowEntityFailure);
            GameEntry.Event.Unsubscribe(SpawnSkillObjEventArgs.EventID, OnSpawnSkillObjSuccess);


            IdGenerater.Instance.Dispose();
        }

        private void OnSpawnSkillObjSuccess(object sender, GameEventArgs e)
        {
            SpawnSkillObjEventArgs ne = (SpawnSkillObjEventArgs)e;

            GameEntry.Entity.ShowSkillSummonObject<SkillMushroom>("SkillMushroom", /*ne.SkillObjData.AssetName,*/
                Constant.AssetPriority.ArmorAsset,ne.SkillObjData);
        }


        private void OnShowEntityFailure(object sender, GameEventArgs e)
        {
            ShowEntityFailureEventArgs ne = (ShowEntityFailureEventArgs)e;
            Log.Warning("Show entity failure with error message '{0}'.", ne.ErrorMessage);
        }

        private void OnShowEntitySuccess(object sender, GameEventArgs e)
        {
            ShowEntitySuccessEventArgs ne = (ShowEntitySuccessEventArgs)e;
            if ((ne.EntityLogicType == typeof(MyPet)))
            {
                m_myPet = (MyPet)ne.Entity.Logic;


            //     //当前房间
            //     m_myPet.BelongToRoom = GameEntry.RoomManager.BattleRoom;
            //     m_myPet.BelongToRoom.RoomHolderPlayerId = m_myPet._petData.TypeId;
            //     m_myPet.BelongToRoom.RoomName = m_myPet.Name;
            //     m_myPet.BelongToRoom.PlayerCount++;
            //
            //     UnitComponent unitComponent = m_myPet.BelongToRoom.GetComponent<UnitComponent>();
            //     unitComponent .MyUnit = m_myPet;
            //     unitComponent.idUnits.Add(m_myPet._petData.TypeId,m_myPet);
            //     
            //
            //     PlayerAssetsInputs _playerAssetsInputs = m_myPet.GetComponent<PlayerAssetsInputs>();
            //     _playerAssetsInputs.cursorLocked = true;
            //     _playerAssetsInputs.cursorInputForLook = true;
            //
            //
            //     CinemachineVirtualCamera cinemachineFreeLook = GameObject.FindGameObjectWithTag("PlayerFollowCamera")
            //         .GetComponent<CinemachineVirtualCamera>();
            //     cinemachineFreeLook.Follow =
            //         m_myPet.Entity.transform.Find("PlayerCameraRoot").transform;
            //     cinemachineFreeLook.LookAt = m_myPet.Entity.transform.Find("LookAt").transform;
            //
            //     m_myPet.AddComponent<DataModifierComponent>();
            //     m_myPet.AddComponent<NP_SyncComponent>();
            //     m_myPet.AddComponent<NumericComponent>();
            //
            //
            //     //增加Buff管理组件
            //     m_myPet.AddComponent<BuffManagerComponent>();
            //     m_myPet.AddComponent<SkillCanvasManagerComponent>();
            //
            //     m_myPet.AddComponent<NP_RuntimeTreeManager>();
            //     m_myPet.AddComponent<UnitAttributesDataComponent>();
            //
            //     m_myPet.GetComponent<PlayerThirdPersonController>().MoveSpeed = m_myPet
            //         .GetComponent<UnitAttributesDataComponent>().GetAttribute(NumericType.Speed) / 100;
            //     
            //
            //     NumericComponent NumericComponent = m_myPet.GetComponent<NumericComponent>();
            }
        }
    }
}