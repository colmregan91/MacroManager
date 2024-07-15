using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class MealTotal : MonoBehaviour
{
    [SerializeField] protected TMP_InputField caloriesInput;
    [SerializeField] protected TMP_InputField carbsInput;
    [SerializeField] protected TMP_InputField proteinInput;
    [SerializeField] protected TMP_InputField fatInput;

    private MealTotalValues _mealValues;

    public MealTotalValues getTotalValues()
    {
        return _mealValues;
    }
    public void UpdateMealTotal(Meal meal)
    {
        float caloriesTotal = 0;
        float carbsTotal = 0;
        float proteinTotal = 0;
        float fatTotal = 0;

        for (int i = 0; i < meal.foods.Count; i++)
        {
            caloriesTotal += meal.foods[i].calories;
            carbsTotal += meal.foods[i].carbs;
            proteinTotal += meal.foods[i].protein;
            fatTotal += meal.foods[i].fat;
        }

        if (_mealValues == null)
        {
            _mealValues = new MealTotalValues();
        }
        
        _mealValues.calories = caloriesTotal;
        _mealValues.carbs = carbsTotal;
        _mealValues.protein = proteinTotal;
        _mealValues.fat = fatTotal;
        
        caloriesInput.text = caloriesTotal.ToString();
        carbsInput.text = carbsTotal.ToString();
        proteinInput.text = proteinTotal.ToString();
        fatInput.text = fatTotal.ToString();
    }
}
