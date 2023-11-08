using UnityEngine;
using UnityEngine.UI;
using UnityGameFramework.Runtime;

namespace Suture
{
    public class UIPetSelectForm : UGuiForm
    {
        public Button backButton;
        public Button YaSuoButton;
        public Button TeemoButton;

        public GameMode GameMode = GameMode.GameStop;

        public (int, string, Vector3) InitPetData;

        protected override void OnInit(object userData)
        {
            base.OnInit(userData);

            backButton.onClick.AddListener(OnBackButtonClick);
            YaSuoButton.onClick.AddListener(OnYaSuoButtonClick);
            TeemoButton.onClick.AddListener(OnTeemoButtonClick);
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


        void OnYaSuoButtonClick()
        {
            InitPetData = (10002, "YaSuo", Vector3.zero);

            GameEntry.Event.Fire(this, LoadPatternEventArgs.Create(GameMode,InitPetData));
            Close(true);
        }

        void OnTeemoButtonClick()
        {
            InitPetData = (10003, "Teemo", Vector3.zero);
            GameEntry.Event.Fire(this, LoadPatternEventArgs.Create(GameMode,InitPetData));
            Close(true);
        }
    }
}