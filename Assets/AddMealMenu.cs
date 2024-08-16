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
    private FoodDisplaySchema schema;
    
    protected override void Start()
    {
        base.Start();
        MenuManager.Instance.AddMenu<AddMealMenu>(this);
            
        schema = new FoodDisplaySchema() { requiresSelection = true, displayType = FoodDisplaySchema.DisplayType.AddingMeal };
    }
        
    private void OpenFoodMenuWithSchema()
    {
        
        FoodDisplayMenu.SetDisplaySchema(schema);
        MenuManager.Instance.OpenMenu<FoodDisplayMenu>();
    }

        
    public override void OnActive()
    {
        OpenFoodMenuWithSchema();

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