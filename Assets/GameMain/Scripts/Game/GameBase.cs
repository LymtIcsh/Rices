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

            GameEntry.Entity.ShowMyPet(new MyPetData(typeId, typeId)
            {
                Name = name,
                Position = InitPos,
                Scale = Vector3.one,
            });


            GameOver = false;
            m_myPet = null;
        }

        public virtual void Update(float elapseSeconds, float realElapseSeconds)
        {
            if (m_myPet != null && m_myPet.IsDead)
            {
                GameOver = true;
                return;
            }

            TimeInfo.Instance.Update();
        }

        public virtual void Shutdown()
        {
            GameEntry.Event.Unsubscribe(ShowEntitySuccessEventArgs.EventId, OnShowEntitySuccess);
            GameEntry.Event.Unsubscribe(ShowEntityFailureEventArgs.EventId, OnShowEntityFailure);

            IdGenerater.Instance.Dispose();
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

                //当前房间
                m_myPet.BelongToRoom = GameEntry.RoomManager.BattleRoom;

                PlayerAssetsInputs _playerAssetsInputs = m_myPet.GetComponent<PlayerAssetsInputs>();
                _playerAssetsInputs.cursorLocked = true;
                _playerAssetsInputs.cursorInputForLook = true;

                GameObject.FindGameObjectWithTag("PlayerFollowCamera").GetComponent<CinemachineVirtualCamera>().Follow =
                    m_myPet.Entity.transform.Find("PlayerCameraRoot").transform;

                m_myPet.AddComponent<DataModifierComponent>();
                m_myPet.AddComponent<NP_SyncComponent>();
                m_myPet.AddComponent<NumericComponent>();
                
                //增加栈式状态机，辅助动画切换
                m_myPet.AddComponent<StackFsmComponent>();
                //增加Buff管理组件
                m_myPet.AddComponent<BuffManagerComponent>();
                m_myPet.AddComponent<SkillCanvasManagerComponent>();

                m_myPet.AddComponent<NP_RuntimeTreeManager>();
                m_myPet.AddComponent<UnitAttributesDataComponent>();

                NP_RuntimeTreeFactory.CreateSkillNpRuntimeTree(m_myPet, 10002, 10002).Start();


                // CDComponent.Instance.AddCDData(m_myPet.Id, "E", 0, info =>
                // {
                //     if (info.Result) //cd 冷却好了
                //     {
                //         // self.FuiUIPanelBattle.m_SkillE_CDInfo.visible = false;
                //         // self.FuiUIPanelBattle.m_SkillE_Bar.Visible = false;
                //         return;
                //     }
                //
                //     Log.Info(((int)Math.Ceiling((double)(info.RemainCDLength) / 1000))
                //         .ToString());
                //     // self.FuiUIPanelBattle.m_SkillE_CDInfo.visible = true;
                //     // self.FuiUIPanelBattle.m_SkillE_Bar.self.value = 100 * (info.RemainCDLength / info.Interval);
                //     // self.FuiUIPanelBattle.m_SkillE_Bar.Visible = true;
                // });

                
                NumericComponent NumericComponent = m_myPet.GetComponent<NumericComponent>();
            }
        }
    }
}