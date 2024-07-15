using System.Collections;
using System.Collections.Generic;
using Menus;
using Singletons;
using UnityEngine;
using UnityEngine.UI;

public class AutoEntryMenu : BaseMenu
{
    [SerializeField] private BarcodeCam scanner;
    [SerializeField] private FoodDisplayer displayer;
    protected override void Start()
    {
        base.Start();
        MenuManager.Instance.AddMenu<AutoEntryMenu>(this);
    }
    
    public void Test()
    {
        scanner.Test();
        
    }

    public override void OnActive()
    {
  
        scanner.gameObject.SetActive(true);
        scanner.OnBarCodeScannned += handleBarCodeScanned;
        scanner.Init();
        displayer.RotateImageforScanning();
    }

    private void handleBarCodeScanned(string barcode)
    {
        WebRequestManager.Instance.MakeApiRequest(barcode, (s)=>
        {
            displayer.ResetRotation();
            displayer.ShowFood(s);
            scanner.Deinit();
            scanner.gameObject.SetActive(false);
            
        }, (error)=> Debug.Log(error));

        
    }


    public override void OnInactive()
    {
        scanner.OnBarCodeScannned -= handleBarCodeScanned;
        displayer.ResetRotation();
        displayer.ClearFields();
        scanner.Deinit();
        scanner.gameObject.SetActive(false);
    }
}

