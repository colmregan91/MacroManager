using System;
using Singletons;
using UnityEngine;

namespace Menus
{
    public class CheckFoodMenu : BaseMenu
    {
    
        protected override void Start()
        {
            base.Start();
            MenuManager.Instance.AddMenu<CheckFoodMenu>(this);
        }
        
        public override void OnActive() => OpenFoodSelectionPanel();


        private void OpenFoodSelectionPanel()
        {
            MenuManager.Instance.OpenAsSubMenu(typeof(AvailableFoodSelection));
        }
 
      

    
    }
}
