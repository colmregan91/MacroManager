using System;
using Notifications;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Singletons
{
    public class PopUpManager : MonoSingleton<PopUpManager>
    {
        private MessagePopUp _messagePopUp;
        private QuestionPopup _questionPopUp;

        protected override void Awake()
        {
            base.Awake();
            _messagePopUp = FindObjectOfType<MessagePopUp>();
            _questionPopUp = FindObjectOfType<QuestionPopup>();
        }
        
        public void ShowQuestionMessage(string message,string positiveText, string negativeText, Action positiveCallback, Action negativeCallback)
        {
            _questionPopUp.ShowQuestionPopup(message,positiveText ,negativeText,positiveCallback,negativeCallback);
        }
        
        public void ShowPopupMessage(string message)
        {
            _messagePopUp.ShowPopupMessage(message);
        }
    }
}