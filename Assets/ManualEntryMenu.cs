
using System;
using Menus;
using UnityEngine;
using System.IO;
using Singletons;
using UnityEngine.EventSystems;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class ManualEntryMenu : BaseMenu
{
    [SerializeField] private BarcodeCam _scanner;
    [SerializeField] private FoodInfoModifier _foodDisplayer;
    [SerializeField] private Button captureButton;

    private bool _settingUpScanner;
    protected override void Start()
    {
        base.Start();
        MenuManager.Instance.AddMenu<ManualEntryMenu>(this);
        
    }

    private void OnEnable()
    {
        _scanner.OnTextureCaptured += HandleTextureCaptured;
    }

    private void HandleTextureCaptured(Texture2D tex)
    {
        DeInitPictureCapture();
        _foodDisplayer.SetImageTexture(tex);
        _settingUpScanner = false;
    }

    private void DeInitPictureCapture()
    {
        captureButton.gameObject.SetActive(false);
        captureButton.onClick.RemoveListener(_scanner.CaptureImage);
        _scanner.Deinit();
        _foodDisplayer.ResetRotation();
        _scanner.gameObject.SetActive(false);
    }
    public void InitPictureCapture()
    {
        // pop up are you sure

        if (_settingUpScanner)
        {
            return;
        }
        _settingUpScanner = true;
        captureButton.gameObject.SetActive(true);
        captureButton.onClick.AddListener(_scanner.CaptureImage);
        _scanner.gameObject.SetActive(true);
        _scanner.Init();
        _foodDisplayer.RotateImageforScanning();
    }
    
    public override void OnActive()
    {
        _foodDisplayer.ShowFood(new Food() { normalPortionSize = 100 });
    }

    public override void OnInactive()
    {
        _settingUpScanner = false;
        DeInitPictureCapture();
        _foodDisplayer.ClearFields();
    }


    
    private void OnDisable()
    {
        captureButton.onClick.RemoveListener(_scanner.CaptureImage);
        _scanner.OnTextureCaptured -= HandleTextureCaptured;
    }
}
