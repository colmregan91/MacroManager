using System;
using System.Collections;
using System.Collections.Generic;
using Notifications;
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class QuestionPopup : BasePopup
{

    [SerializeField] private Button positiveButton;
   [SerializeField] private Button negativeButton;
   
   [SerializeField] private TextMeshProUGUI positiveButtonText;
   [SerializeField] private TextMeshProUGUI negativeButtonText;

        
    public void ShowQuestionPopup(string message,string positiveText, string negativeText, Action positiveCallback, Action negativeCallback)
    {
        _messageText.text = message;
        positiveButtonText.text = positiveText;
        negativeButtonText.text = negativeText;
        
        positiveButton.onClick.AddListener(()=>
        {
         
            positiveCallback?.Invoke();
            HidePopup();
        });
        
        negativeButton.onClick.AddListener(()=>
        {
            negativeCallback?.Invoke();
            HidePopup();
        });
        ShowCanvasGroup();
    }
    
    protected override void HidePopup()
    {
       base.HidePopup();
        positiveButton.onClick.RemoveAllListeners();
        negativeButton.onClick.RemoveAllListeners();
    }
    public override void OnDisable()
    {
        base.OnDisable();
        positiveButton.onClick.RemoveAllListeners();
        negativeButton.onClick.RemoveAllListeners();
    }
}
