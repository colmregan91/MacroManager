using System;
using System.Collections;
using System.Collections.Generic;
using Menus;
using Singletons;
using UnityEngine;
using UnityEngine.UI;

public class AutoEntryMenu : BaseMenu
{
    [SerializeField] private BarcodeCam scanner;
    private bool _settingUpScanner;
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
        scanner.OnBarCodeScannned += handleBarCodeScanned;
        InitPictureCapture();
    }

    private void handleBarCodeScanned(string barcode)
    {
        Debug.Log(barcode);
        WebRequestManager.Instance.MakeApiRequest(barcode, (food) =>
        {
            scanner.Deinit();
            scanner.gameObject.SetActive(false);
            OpenFoodMenuWithSchema(food);
        }, (error) =>
        {
            scanner.Deinit();
            scanner.gameObject.SetActive(false);
            PopUpManager.Instance.ShowQuestionMessage("Food does not exist on server. Enter Manually?", "yes", "cancel", () =>
                { MenuManager.Instance.OpenAsSubMenu(typeof(ManualEntryMenu)); }, () => { MenuManager.Instance.ChangeMenu(typeof(MainMenu)); });
        });
    }

    private void OpenFoodMenuWithSchema(Food food)
    {
        FoodDisplaySchema schema = new FoodDisplaySchema() { requiresSelection = false,displayType = FoodDisplaySchema.DisplayType.AddingFood};
        schema.foods.Add(food);
        FoodDisplayMenu.SetDisplaySchema(schema);
        MenuManager.Instance.OpenMenu<FoodDisplayMenu>();
    }
    
    private void DeInitPictureCapture()
    {
        _settingUpScanner = false;
        scanner.Deinit();
        scanner.gameObject.SetActive(false);

    }

    public void InitPictureCapture()
    {
        if (_settingUpScanner)
        {
            return;
        }

        _settingUpScanner = true;
        scanner.gameObject.SetActive(true);
        scanner.Init();
    }



    public override void OnInactive()
    {
        scanner.OnBarCodeScannned -= handleBarCodeScanned;
        DeInitPictureCapture();
    }
}