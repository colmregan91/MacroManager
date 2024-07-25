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
        positiveButton.onClick.RemoveAllListeners();
        negativeButton.onClick.RemoveAllListeners();
        _messageText.text = message;
        positiveButtonText.text = positiveText;
        negativeButtonText.text = negativeText;
        ShowCanvasGroup();
        positiveButton.onClick.AddListener(()=>
        {
            HidePopup();
            positiveCallback?.Invoke();
          
        });
        
        negativeButton.onClick.AddListener(()=>
        {
            HidePopup();
            negativeCallback?.Invoke();
           
        });

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
