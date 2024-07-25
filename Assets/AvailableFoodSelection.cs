using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using Menus;
using Singletons;
using UnityEngine;
using UnityEngine.UI;

public class AvailableFoodSelection : BaseMenu
{
    private const string PATH = "FoodSelectionUI";
    private FoodSelectUI selectUI;
    [SerializeField] private Transform contentParent;
    private FoodSelectUI selectUIGO;
    
    [SerializeField] private Button continueButton;
    
    private List<Food> _selectedFoods = new  List<Food>();

    public Action<List<Food>> OnContinue;








    private void Init()
    {
        var AllFoods = FoodFetcher.Instance.AllFoods;
        foreach (Food food in AllFoods)
        {
            selectUIGO = Instantiate(selectUI, contentParent);
            selectUIGO.Init(food, HandleFoodSelected);
        }
    }

    protected override void Start()
    {
       base.Start();
 
       MenuManager.Instance.AddMenu<AvailableFoodSelection>(this);
        
        selectUI = Resources.Load<FoodSelectUI>(PATH);
        Init();
    }
    
    private void HandleFoodSelected(FoodSelectUI foodUI)
    {
        if (_selectedFoods.Contains(foodUI.FoodAtThisSelection))
        {
            foodUI.OnDeselect();
            _selectedFoods.Remove(foodUI.FoodAtThisSelection);
        }
        else
        {
            foodUI.OnSelect();
            _selectedFoods.Add(foodUI.FoodAtThisSelection);
        }

        continueButton.interactable = _selectedFoods.Count > 0;
    }

    private void HandleContinueClicked()
    {
        MenuManager.Instance.CloseCurrentSubMenu();
        OnContinue?.Invoke(_selectedFoods);
    }

    private void OnEnable()
    {
        AddFoodMenu.OnFoodAdded += OnAddFood;
    }
    
    private void OnAddFood(Food food)
    {
        selectUIGO = Instantiate(selectUI, contentParent);
        selectUIGO.Init(food, HandleFoodSelected);
    }

    private void OnDisable()
    {
        AddFoodMenu.OnFoodAdded -= OnAddFood;
    }
}