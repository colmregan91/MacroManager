using System.Collections;
using System.Collections.Generic;
using Menus;
using Singletons;
using UnityEngine;

public class AvailableFoodDisplay : BaseMenu
{
    [SerializeField] private FoodInfoModifier foodDisplayer;
    protected override void Start()
    {
        base.Start();
 
        MenuManager.Instance.AddMenu<AvailableFoodDisplay>(this);
        
    }

    public override void OnActive()
    {
        foodDisplayer.ShowFood(FoodDisplayMenu.displaySchema);
    }

}
