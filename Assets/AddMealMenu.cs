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
    private DropdownAdder _dropdownAdder;
    private const string PATH = "FoodField";
    private GameObject _fieldObj;

    [SerializeField] private Transform displayHolder;
    private MealTotal _mealTotal;
    private Meal _meal;
    private List<FoodInfoModifier> _modifiers = new List<FoodInfoModifier>();

    [SerializeField] private Button _addMealButton;
    [SerializeField] private TMP_InputField _mealNameField;
    [SerializeField] private MealTotal mealtotal;
    protected override void Start()
    {
        _fieldObj = Resources.Load<GameObject>(PATH);
        _dropdownAdder = GetComponentInChildren<DropdownAdder>();
        _mealTotal = GetComponentInChildren<MealTotal>();
        base.Start();
        MenuManager.Instance.AddMenu<AddMealMenu>(this);
    }

    private void CheckNameField(string arg0)
    {
        _addMealButton.interactable = arg0.Length > 3 && _modifiers.Count > 1;
    }


    public void HandleAddMealClicked()
    {
        _meal.name = _mealNameField.text;
        _meal.mealTotal = mealtotal.getTotalValues();
        string json = JsonUtility.ToJson(_meal, true);
        string path = PathUtils.GetMealPath(_meal.name);
        File.WriteAllText(path, json);
        Debug.Log(path);
        _modifiers.Clear();
    }


    private void OnEnable()
    {
        _dropdownAdder.OnFoodAdded += HandleFoodAdded;
        _mealNameField.onValueChanged.AddListener(CheckNameField);
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
        CheckNameField(_mealNameField.text);
    }

    private void HandleFoodAdded(Food food)
    {
        var foodModifier = Instantiate(_fieldObj, displayHolder).GetComponentInChildren<FoodInfoModifier>();

        _modifiers.Add(foodModifier);
        foodModifier.ShowFood(food);
        _meal.foods.Add(foodModifier.DisplayedFood);
        foodModifier.OnFoodModified += UpdateMeal;
        foodModifier.OnFoodRemoved += HandleFoodRemoved;
        if (displayHolder.transform.childCount > 1)
        {
            foodModifier.HideTitles();
        }

        _mealTotal.UpdateMealTotal(_meal);
        CheckNameField(_mealNameField.text);
    }

    private void OnDisable()
    {
        _dropdownAdder.OnFoodAdded -= HandleFoodAdded;
        _mealNameField.onValueChanged.RemoveListener(CheckNameField);
        foreach (var t in _modifiers)
        {
            t.OnFoodModified -= UpdateMeal;
            t.OnFoodRemoved -= HandleFoodRemoved;
        }
    }
}