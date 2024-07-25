using System.Collections;
using System.Collections.Generic;
using Menus;
using Singletons;
using UnityEngine;

public class CheckedFoodPanel : BaseMenu
{
    [SerializeField] private FoodDisplayer _foodDisplayer;
    protected override void Start()
    {
        base.Start();
        MenuManager.Instance.AddMenu<CheckedFoodPanel>(this);
    }
    
    public override void OnActive() =>  _foodDisplayer.gameObject.SetActive(true);
  //  public override void OnInactive() => ClearSelection();
    private void OnEnable()
    {
        // AvailableFoodSelection.OnFoodSelected += ShowSelectedFood;
    }

    private void OnDisable()
    {
      //     AvailableFoodSelection.OnFoodSelected -= ShowSelectedFood;
    }

    // private void ClearSelection() // this is shit
    // {
    //     _foodDisplayer.ClearFields();
    //     _foodDisplayer.gameObject.SetActive(false);
    // }
    // private void ShowSelectedFood(Food food) // this is shit
    // {
    //     MenuManager.Instance.OpenAsSubMenu(typeof(CheckedFoodPanel));
    //      _foodDisplayer.ShowFood(food);
    // }

}
