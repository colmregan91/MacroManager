using System;
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

namespace Notifications
{
    public class MessagePopUp : BasePopup
    {

        [SerializeField]  private Button _Button;
       [SerializeField] private TextMeshProUGUI buttonTextObj;


        protected override void HidePopup()
        {
            base.HidePopup();
            _Button.onClick.RemoveAllListeners();
        }
        
        
        public void ShowPopupMessage(string message,string buttonText, Action callback)
        {
            _Button.onClick.AddListener(HidePopup);
            _messageText.text = message;
            buttonTextObj.text = buttonText;
            ShowCanvasGroup();
            if (callback != null)
            {
                _Button.onClick.AddListener( callback.Invoke);
            }
        }
        public override void OnDisable()
        {
            base.OnDisable();
            _Button.onClick.RemoveAllListeners();
        }

    }
}


