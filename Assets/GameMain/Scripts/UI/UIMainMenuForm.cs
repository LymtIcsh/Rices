using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using UnityGameFramework.Runtime;

namespace Suture
{
    public class UIMainMenuForm : UGuiForm
    {
        [SerializeField]
        private GameObject m_QuitButton = null;

        [SerializeField]
        private GameObject m_PatternSelectButton = null;

        [SerializeField]
        private GameObject m_SettingButton = null;


        private ProcedureMenu m_ProcedureMenu = null;


        protected override void OnInit(object userData)
        {
            base.OnInit(userData);
            m_QuitButton.GetComponent<Button>().onClick.AddListener(OnQuitButtonClick);
            m_PatternSelectButton.GetComponent<Button>().onClick.AddListener(OnStartButtonClick);
            m_SettingButton.GetComponent<Button>().onClick.AddListener(OnSettingButtonClick);
        }



        protected override void OnOpen(object userData)
        {
            base.OnOpen(userData);

            m_ProcedureMenu = (ProcedureMenu)userData;
            if (m_ProcedureMenu == null)
            {
                Log.Warning("ProcedureMenu is invalid when open MenuForm.");
                return;
            }

            m_QuitButton.SetActive(Application.platform != RuntimePlatform.IPhonePlayer);
        }

        protected override void OnClose(bool isShutdown, object userData)
        {
            m_ProcedureMenu = null;

            base.OnClose(isShutdown, userData);
        }

        public void OnStartButtonClick()
        {
            //m_ProcedureMenu.StartGame();
            GameEntry.UI.OpenUIForm(UIFormId.UIPatternSelectForm,this);
          //组队房间
          //  GameEntry.RoomManager.CreateLobbyRoom();
            //GameEntry.UI.OpenUIForm(UIFormId.UIPatternSelectForm);
        }

        private void OnSettingButtonClick()
        {
            GameEntry.UI.OpenUIForm(UIFormId.SettingForm,this);
        }

        private void OnQuitButtonClick()
        {
            UnityGameFramework.Runtime.GameEntry.Shutdown(ShutdownType.Quit);
        }
    }
}

