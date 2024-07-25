using System;
using System.IO;
using Singletons;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

namespace Menus
{
    public class AddFoodMenu : BaseMenu // need a notification system
    {
        [SerializeField] private Button ManualEnterButton;
        [SerializeField] private Button ScanBarcodeButton;

        public static Action<Food> OnFoodAdded;
        protected override void Start()
        {
            base.Start();
            MenuManager.Instance.AddMenu<AddFoodMenu>(this);
        }
        

        private void OnEnable()
        {
            ManualEnterButton.onClick.AddListener(OpenManualEntryPanel);
            ScanBarcodeButton.onClick.AddListener(OpenAutoEntryPanel);
    
        }

        public override void OnActive() => OpenMethodEntryPanel();
        
        private void OpenMethodEntryPanel()
        {
            MenuManager.Instance.OpenAsSubMenu(typeof(AddFoodMethodTypeMenu));
        }
        private void OpenAutoEntryPanel()
        {
            MenuManager.Instance.OpenAsSubMenu(typeof(AutoEntryMenu));
        }
        
        private void OpenManualEntryPanel()
        {
            MenuManager.Instance.OpenAsSubMenu(typeof(ManualEntryMenu));
        }

        private void OnDisable()
        {
            ManualEnterButton.onClick.RemoveListener(OpenManualEntryPanel);
            ScanBarcodeButton.onClick.RemoveListener(OpenAutoEntryPanel);
    
        }


    }
}