using System;
using Singletons;
using UnityEngine;

namespace Menus
{
    public class CheckFoodMenu : BaseMenu
    {
        [SerializeField] private FoodInfoModifier checkedFoodPanel;
        protected override void Start()
        {
            base.Start();
            MenuManager.Instance.AddMenu<CheckFoodMenu>(this);
        }
        
        public override void OnActive()
        {
            MenuManager.Instance.OpenAsSubMenu(typeof(AvailableFoodSelection));
            checkedFoodPanel.gameObject.SetActive(false);
  
        }
        
      

    
    }
}
