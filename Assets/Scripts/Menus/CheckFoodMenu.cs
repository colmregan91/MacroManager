using System;
using Singletons;
using UnityEngine;

namespace Menus
{
    public class CheckFoodMenu : BaseMenu
    {
        [SerializeField] private AvailableFoodSelection selectionPanel;
    
        protected override void Start()
        {
            base.Start();
            MenuManager.Instance.AddMenu<CheckFoodMenu>(this);
        }
        
        public override void OnActive() => OpenFoodSelectionPanel();


        private void OpenFoodSelectionPanel()
        {
            selectionPanel.gameObject.SetActive(true);
        }
 
      

    
    }
}
