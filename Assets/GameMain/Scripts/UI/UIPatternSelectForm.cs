using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;
using UnityGameFramework.Runtime;

namespace Suture
{
    public class UIPatternSelectForm : UGuiForm
    {
        public Button backButton;
        public Button PveButton;
        public Button PetButton;
        
        
        protected override void OnInit(object userData)
        {
            base.OnInit(userData);
            backButton.onClick.AddListener(OnBackButtonClick);
            PveButton.onClick.AddListener(OnPveButtonClick);
            PetButton.onClick.AddListener(OnPetButtonClick);
        }

        protected override void OnOpen(object userData)
        {
            base.OnOpen(userData);
        }

        protected override void OnClose(bool isShutdown, object userData)
        {
            base.OnClose(isShutdown, userData);
        }

        void OnBackButtonClick()
        {
            Close();
        }

        void OnPveButtonClick()
        {
            GameEntry.Event.Fire(this, LoadPatternEventArgs.Create(GameMode.Pve));
            GameEntry.UI.OpenUIForm(UIFormId.UIPetSelectForm, this);
            Close(true);
        }

        void OnPetButtonClick()
        {
            GameEntry.Event.Fire(this, LoadPatternEventArgs.Create(GameMode.Pet));
            GameEntry.UI.OpenUIForm(UIFormId.UIPetSelectForm, this);
            Close(true);
        }
    }
}

