using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Menus;
using Singletons;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class AvailableFoodSelection : BaseMenu
{
    private const string PATH = "FoodSelectionUI";
    private FoodSelectUI selectUI;
    [SerializeField] private Transform contentParent;
    private FoodSelectUI selectUIGO;

    [SerializeField] private Button continueButton;

    private Dictionary<Food, FoodSelectUI> _selectedFoods = new Dictionary<Food, FoodSelectUI>();
 

    protected override void Start()
    {
        base.Start();
        MenuManager.Instance.AddMenu<AvailableFoodSelection>(this);

        selectUI = Resources.Load<FoodSelectUI>(PATH);
    }


    public override void OnActive()
    {
        continueButton.interactable = _selectedFoods.Count > 0;
    }

    private void HandleFoodSelected(FoodSelectUI foodUI)
    {
        if (_selectedFoods.ContainsKey(foodUI.FoodAtThisSelection))
        {
            foodUI.OnDeselect();
            _selectedFoods.Remove(foodUI.FoodAtThisSelection);
        }
        else
        {
            foodUI.OnSelect();
            _selectedFoods.Add(foodUI.FoodAtThisSelection, foodUI);
        }

        continueButton.interactable = _selectedFoods.Count > 0;
    }

    private void HandleContinueClicked()
    {
        FoodDisplayMenu.SetDisplayFoods(_selectedFoods.Keys.ToList());

        switch (FoodDisplayMenu.displaySchema.displayType)
        {
            case FoodDisplaySchema.DisplayType.AddingFood:
                MenuManager.OpenFoodDisplayMenu();
                break;
            case FoodDisplaySchema.DisplayType.AddingMeal:
                MenuManager.OpenMealDisplayMenu();
                break;
            case FoodDisplaySchema.DisplayType.CheckingFood:
                MenuManager.OpenFoodDisplayMenu();
                break;
            case FoodDisplaySchema.DisplayType.CheckingMeal:
                MenuManager.OpenMealDisplayMenu();
                break;
            
        }
    }

    private void OnEnable()
    {
        AddFoodMenu.OnFoodAdded += OnAddFood;
        FoodFetcher.Instance.OnFoodSerialized += OnAddFood;
        continueButton.onClick.AddListener(HandleContinueClicked);
        MenuManager.Instance.OnMenuChanged += HandleMenuChange;
    }

    private void HandleMenuChange(BaseMenu obj)
    {
        if (obj is AvailableFoodDisplay)
        {
            return;
        }

        if (obj is AvailableFoodSelection)
        {
            return;
        }

        foreach (var food in _selectedFoods.Keys)
        {
            _selectedFoods[food].OnDeselect();
        }

        _selectedFoods.Clear();
    }

    private void OnAddFood(Food food)
    {
        selectUIGO = Instantiate(selectUI, contentParent);
        selectUIGO.Init(food, HandleFoodSelected);
    }

    private void OnDisable()
    {
        AddFoodMenu.OnFoodAdded -= OnAddFood;
        FoodFetcher.Instance.OnFoodSerialized -= OnAddFood;
        continueButton.onClick.RemoveListener(HandleContinueClicked);
        MenuManager.Instance.OnMenuChanged -= HandleMenuChange;
    }
}