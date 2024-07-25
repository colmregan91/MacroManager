using System.Collections;
using System.Collections.Generic;
using Menus;
using Singletons;
using UnityEngine;

public class FoodDisplayMenu : BaseMenu
{
    public static FoodDisplaySchema displaySchema;
    public static void SetDisplaySchema(FoodDisplaySchema desiredDisplay)
    {
        displaySchema = desiredDisplay;
    }
    protected override void Start()
    {
        base.Start();
 
        MenuManager.Instance.AddMenu<FoodDisplayMenu>(this);
        
      
    }
    
    public override void OnActive()
    {
        SetUpViewWithSchema(displaySchema);
    }
    
    private void SetUpViewWithSchema(FoodDisplaySchema schema)
    {
        if (schema.requiresSelection)
        {
            MenuManager.Instance.OpenAsSubMenu(typeof(AvailableFoodSelection));
        }
        else
        {
            MenuManager.Instance.OpenAsSubMenu(typeof(AvailableFoodDisplay));
        }
    }
 
}
