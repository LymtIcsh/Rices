using UnityEngine;
using UnityEngine.UI;

namespace Suture
{
    public class UIPetSelectForm : UGuiForm
    {
        public Button backButton;
        public Button YaSuoButton;
        public Button TeemoButton;
        
        public GameMode GameMode = GameMode.GameStop;

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
            Debug.Log(GameMode);
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
            GameEntry.Event.Fire(this, LoadPatternEventArgs.Create(GameMode));
            Close(true);
        }
        
        void OnTeemoButtonClick()
        {
            GameEntry.Event.Fire(this, LoadPatternEventArgs.Create(GameMode));
            Close(true);
        }
    }
}