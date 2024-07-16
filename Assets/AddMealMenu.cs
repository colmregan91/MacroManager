using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using Menus;
using Singletons;
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class AddMealMenu : BaseMenu
{
    private const string PATH = "FoodField";
    private GameObject _fieldObj;
    private AvailableFoodSelection selectionPanel;
    private MealTotal _mealTotal;
    private Meal _meal;
    private List<FoodInfoModifier> _modifiers = new List<FoodInfoModifier>();

    [SerializeField] private Button _addMealButton;
    [SerializeField] private MealTotal mealtotal;
    
    protected override void Start()
    {
        _fieldObj = Resources.Load<GameObject>(PATH);
        _mealTotal = GetComponentInChildren<MealTotal>();
        base.Start();
        MenuManager.Instance.AddMenu<AddMealMenu>(this);
    }
    

    public void HandleAddMealClicked()
    {
        _meal.mealTotal = mealtotal.getTotalValues();
        string json = JsonUtility.ToJson(_meal, true);
        string path = PathUtils.GetMealPath(_meal.name);
        File.WriteAllText(path, json);
        Debug.Log(path);
        _modifiers.Clear();
    }


    private void OnEnable()
    {
        _meal = new Meal();
        _meal.foods = new List<Food>();
    }

    private void UpdateMeal()
    {
        _meal.foods.Clear();
        foreach (var t in _modifiers)
        {
            _meal.foods.Add(t.DisplayedFood);
        }

        _mealTotal.UpdateMealTotal(_meal);
    }

    private void HandleFoodRemoved(FoodInfoModifier foodMod)
    {
        if (_meal.foods.Contains(foodMod.DisplayedFood))
        {
            _meal.foods.Remove(foodMod.DisplayedFood);
        }
        else
        {
            Debug.LogError("food doesnt exist");
        }

        if (_modifiers.Contains(foodMod))
        {
            _modifiers.Remove(foodMod);
        }
        else
        {
            Debug.LogError("foodModifier doesnt exist");
        }

        foodMod.OnFoodModified += UpdateMeal;
        foodMod.OnFoodRemoved += HandleFoodRemoved;
        Destroy(foodMod.transform.parent.gameObject);
        _mealTotal.UpdateMealTotal(_meal);
    }

    private void HandleFoodAdded(Food food)
    {
        // var foodModifier = Instantiate(_fieldObj, displayHolder).GetComponentInChildren<FoodInfoModifier>();
        //
        // _modifiers.Add(foodModifier);
        // foodModifier.ShowFood(food);
        // _meal.foods.Add(foodModifier.DisplayedFood);
        // foodModifier.OnFoodModified += UpdateMeal;
        // foodModifier.OnFoodRemoved += HandleFoodRemoved;
        // if (displayHolder.transform.childCount > 1)
        // {
        //     foodModifier.HideTitles();
        // }

        _mealTotal.UpdateMealTotal(_meal);
    }

    private void OnDisable()
    {
        foreach (var t in _modifiers)
        {
            t.OnFoodModified -= UpdateMeal;
            t.OnFoodRemoved -= HandleFoodRemoved;
        }
    }
}