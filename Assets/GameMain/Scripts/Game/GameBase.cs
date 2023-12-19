using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GameFramework.Event;
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

            GameEntry.Entity.ShowMyPet(new MyPetData(GameEntry.Entity.GenerateSerialId(), typeId)
            {
                Name = name,
                Position = InitPos,
                Scale = new Vector3(0.01f, 0.01f, 0.01f)
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
            }
        }
    }
}