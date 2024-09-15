using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;
using UnityGameFramework.Runtime;

namespace Suture
{
    public class UIPetSelectForm : UGuiForm
    {
        public Button backButton;
        public Button NingGuangButton;
         public Button YingButton;

        public GameMode GameMode = GameMode.GameStop;

        public (int, string, Vector3) InitPetData;

        protected override void OnInit(object userData)
        {
            base.OnInit(userData);

            backButton.onClick.AddListener(OnBackButtonClick);
            NingGuangButton.onClick.AddListener(OnNingGuangButtonClick);
            YingButton.onClick.AddListener(OnYingoButtonClick);
        }

        protected override void OnOpen(object userData)
        {
            base.OnOpen(userData);

            GameMode = (userData as UIPatternSelectForm).GameMode;
        }

        protected override void OnClose(bool isShutdown, object userData)
        {
            base.OnClose(isShutdown, userData);
        }

        void OnBackButtonClick()
        {
            Close();
        }


        void OnNingGuangButtonClick()
        {
            InitPetData = (10002, "安比", new Vector3(57,0,40));

            GameEntry.Event.Fire(this, LoadPatternEventArgs.Create(GameMode,InitPetData));
            Close(true);
        }

        void OnYingoButtonClick()
        {
            InitPetData = (10003, "Ying",  new Vector3(57,0,40));
            GameEntry.Event.Fire(this, LoadPatternEventArgs.Create(GameMode,InitPetData));
            Close(true);
        }
    }
}