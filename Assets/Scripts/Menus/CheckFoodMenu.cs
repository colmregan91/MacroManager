using System;
using Singletons;
using UnityEngine;

namespace Menus
{
    public class CheckFoodMenu : BaseMenu
    {
        private FoodDisplaySchema _schema;
        protected override void Start()
        {
            base.Start();
            MenuManager.Instance.AddMenu<CheckFoodMenu>(this);
            
         _schema = new FoodDisplaySchema() { requiresSelection = true, displayType = FoodDisplaySchema.DisplayType.CheckingFood };
        }
        
        private void OpenFoodMenuWithSchema()
        {
        
            FoodDisplayMenu.SetDisplaySchema(_schema);
            MenuManager.Instance.OpenMenu<FoodDisplayMenu>();
        }

        
        public override void OnActive()
        {
            OpenFoodMenuWithSchema();

        }
        
      

    
    }
}
