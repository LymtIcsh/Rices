

using GameFramework.Event;
using GameFramework.Fsm;
using System;
using UnityGameFramework.Runtime;
using ProcedureOwner = GameFramework.Fsm.IFsm<GameFramework.Procedure.IProcedureManager>;

namespace Suture
{
    public class ProcedureMenu:ProcedureBase
    {
        /// <summary>
        /// 是否切换场景
        /// </summary>
        private bool changeScene = false;

        private ProcedureOwner procedureOwner;

        private UIMainMenuForm m_MenuForm =null;

        public override bool UseNativeDialog
        {
            get
            {
                return false;
            }
        }




        protected override void OnEnter(ProcedureOwner procedureOwner)
        {
            base.OnEnter(procedureOwner);

            this.procedureOwner = procedureOwner;
            changeScene = false;

            GameEntry.Event.Subscribe(OpenUIFormSuccessEventArgs.EventId, OnOpenUIFormSuccess);
            GameEntry.Event.Subscribe(ChangeSceneEventArgs.EventId, OnChangeScene);
            GameEntry.Event.Subscribe(LoadPatternEventArgs.EventId, OnLoadPattern);


            GameEntry.UI.OpenUIForm(UIFormId.MenuForm, this);
        }


        protected override void OnUpdate(ProcedureOwner procedureOwner, float elapseSeconds, float realElapseSeconds)
        {
            base.OnUpdate(procedureOwner, elapseSeconds, realElapseSeconds);

            if (changeScene)
            {
                
                // procedureOwner.SetData<VarInt32>("NextSceneId", GameEntry.Config.GetInt("Scene.Pve"));
                
                ChangeState<ProcedureChangeScene>(procedureOwner);
                
                // GameEntry.UI.CloseUIForm((int)UIFormId.UIPatternSelectForm);
            }
        }

        protected override void OnLeave(ProcedureOwner procedureOwner, bool isShutdown)
        {
            base.OnLeave(procedureOwner, isShutdown);

            GameEntry.Event.Unsubscribe(OpenUIFormSuccessEventArgs.EventId, OnOpenUIFormSuccess);
            GameEntry.Event.Unsubscribe(ChangeSceneEventArgs.EventId, OnChangeScene);
            GameEntry.Event.Unsubscribe(LoadPatternEventArgs.EventId, OnLoadPattern);

            if (m_MenuForm != null)
            {
                m_MenuForm.Close(!isShutdown);
                m_MenuForm = null;
            }
        }

        protected override void OnDestroy(ProcedureOwner procedureOwner)
        {
            base.OnDestroy(procedureOwner);
        }

        private void OnOpenUIFormSuccess(object sender, GameEventArgs e)
        {
            OpenUIFormSuccessEventArgs ne = (OpenUIFormSuccessEventArgs)e;
            if (ne.UserData != this)
            {
                return;
            }

            m_MenuForm = (UIMainMenuForm)ne.UIForm.Logic;
        }

        private void OnChangeScene(object sender, GameEventArgs e)
        {
            ChangeSceneEventArgs ne = (ChangeSceneEventArgs)e;
            if (ne==null)
                return;

            changeScene = true;
            procedureOwner.SetData<VarInt32>("NextSceneId", ne.SceneId);
        }

        private void OnLoadPattern(object sender, GameEventArgs e)
        {
            LoadPatternEventArgs ne = (LoadPatternEventArgs)e;
            if (ne == null)
                return;
            changeScene = true;
            procedureOwner.SetData<VarInt32>("NextSceneId", (int)ne.GameMode);
            procedureOwner.SetData<VarByte>("GameMode", (byte)ne.GameMode);
        }
    }
}
