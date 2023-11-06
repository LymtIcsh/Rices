using UnityEngine;
using UnityEngine.UI;

namespace Suture
{
    public class UIPetSelectForm : UGuiForm
    {
        public Button backButton;
        public Button YaSuoButton;
        public Button TeemoButton;

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
            Close(true);
        }
        
        void OnTeemoButtonClick()
        {
            Close(true);
        }
    }
}