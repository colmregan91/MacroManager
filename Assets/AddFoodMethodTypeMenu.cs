using System.Collections;
using System.Collections.Generic;
using Menus;
using Singletons;
using UnityEngine;
using UnityEngine.UI;

public class AddFoodMethodTypeMenu : BaseMenu
{

    protected override void Start()
    {
        base.Start();
        MenuManager.Instance.AddMenu<AddFoodMethodTypeMenu>(this);
    }

 

    private void OnEnable()
    {
   //     ManualEnterButton.onClick.AddListener(OpenManualEntryPanel);
        //        ScanBarcodeButton.onClick.AddListener(OpenCheckMenu);
    }
    
    
    private void OnDisable()
    {
    //    ManualEnterButton.onClick.RemoveListener(OpenManualEntryPanel);
    }
}
