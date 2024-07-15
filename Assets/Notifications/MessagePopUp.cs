using UnityEngine.UI;

namespace Notifications
{
    public class MessagePopUp : BasePopup
    {

        private Button _okButton;

        public override void Awake()
        {
            base.Awake();
            _okButton = GetComponentInChildren<Button>();
        }

        public override void OnEnable()
        {
            base.OnEnable();
            _okButton.onClick.AddListener(HidePopup);
  
        }
        
        public void ShowPopupMessage(string message)
        {
            _messageText.text = message;
            ShowCanvasGroup();
        }
        public override void OnDisable()
        {
            base.OnDisable();
            _okButton.onClick.RemoveListener(HidePopup);
        }

    }
}


