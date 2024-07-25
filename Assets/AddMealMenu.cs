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

    [SerializeField]private GameObject mealDisplayPanel;
    private const string PATH = "FoodField";
    private GameObject _fieldObj;
 
    private MealTotal _mealTotal;


    [SerializeField] private Button _addMealButton;

    public override void OnActive()
    {
        MenuManager.Instance.OpenAsSubMenu(typeof(AvailableFoodSelection));
        mealDisplayPanel.SetActive(false);
  
    }
    protected override void Start()
    {
        
        _fieldObj = Resources.Load<GameObject>(PATH);
        _mealTotal = GetComponentInChildren<MealTotal>();
        base.Start();
        MenuManager.Instance.AddMenu<AddMealMenu>(this);
    }
    

    // public void HandleAddMealClicked()
    // {
    //     _meal.mealTotal = _mealTotal.getTotalValues();
    //     string json = JsonUtility.ToJson(_meal, true);
    //     string path = PathUtils.GetMealPath(_meal.name);
    //     File.WriteAllText(path, json);
    // }
    
    // private void UpdateMeal()
    // {
    //     _meal.foods.Clear();
    //     foreach (var t in _modifiers)
    //     {
    //         _meal.foods.Add(t.DisplayedFood);
    //     }
    //
    //     _mealTotal.UpdateMealTotal(_meal);
    // }
    
}