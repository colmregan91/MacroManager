using System;
using Menus;
using UnityEngine;
using System.IO;
using Singletons;
using UnityEngine.EventSystems;
using UnityEngine.Serialization;
using UnityEngine.UI;
using Utils;

public class ManualEntryMenu : BaseMenu
{
    [SerializeField] private BarcodeCam _scanner;
    [SerializeField] private Button captureButton;
    [SerializeField] private Button continueButton;
    [SerializeField] private Button TakeAgainButton;
    private bool _settingUpScanner;

    protected override void Start()
    {
        base.Start();
        MenuManager.Instance.AddMenu<ManualEntryMenu>(this);
    }

    public override void OnActive()
    {
        continueButton.gameObject.SetActive(false);
        TakeAgainButton.gameObject.SetActive(false);
        _scanner.OnTextureCaptured += HandleTextureCaptured;
        InitPictureCapture();
    }

    public override void OnInactive()
    {
        continueButton.gameObject.SetActive(false);
        TakeAgainButton.gameObject.SetActive(false);
        _scanner.OnTextureCaptured -= HandleTextureCaptured;
    }

    private void OnContinue(Texture2D tex)
    {
        continueButton.onClick.RemoveAllListeners();
        FoodDisplaySchema schema = new FoodDisplaySchema() { requiresSelection = false,food = new Food() { normalPortionSize = 100, TextureData = TextureUtils.GetDataFromTexture(tex) } };
        FoodDisplayMenu.SetDisplaySchema(schema);
        MenuManager.Instance.OpenMenu<FoodDisplayMenu>();
    }

    private void OnTakeAgain()
    {
        continueButton.gameObject.SetActive(false);
        TakeAgainButton.gameObject.SetActive(false);
        TakeAgainButton.onClick.RemoveListener(OnTakeAgain);
        InitPictureCapture();
    }

    private void HandleTextureCaptured(Texture2D tex)
    {
        DeInitPictureCapture(tex);
    }

    private void DeInitPictureCapture(Texture2D tex)
    {
        _settingUpScanner = false;
        captureButton.gameObject.SetActive(false);
        captureButton.onClick.RemoveListener(_scanner.CaptureImage);
        _scanner.Deinit();
        _scanner.gameObject.SetActive(false);

        continueButton.gameObject.SetActive(true);
        TakeAgainButton.gameObject.SetActive(true);
        continueButton.onClick.AddListener(() => OnContinue(tex));
        TakeAgainButton.onClick.AddListener(OnTakeAgain);
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
    }


    private void OnDisable()
    {
        captureButton.onClick.RemoveListener(_scanner.CaptureImage);
    }
}