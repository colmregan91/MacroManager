using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using Menus;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using Utils;

public class FoodDisplayer : MonoBehaviour
{
    [SerializeField] protected RawImage image;
    [SerializeField] protected TMP_InputField nameInput;
    [SerializeField] protected TMP_InputField servingSizeInput;
    [SerializeField] protected TMP_InputField caloriesInput;
    [SerializeField] protected TMP_InputField carbsInput;
    [SerializeField] protected TMP_InputField proteinInput;
    [SerializeField] protected TMP_InputField fatInput;
    [SerializeField] protected TMP_Dropdown typeInput;
    [SerializeField] private GameObject[] titles;
    protected Food displayedFood;
    

    private List<TMP_Dropdown.OptionData> _options = new List<TMP_Dropdown.OptionData>();
    private List<FoodType> foodTypes = new List<FoodType>();
    
    public Food DisplayedFood => displayedFood;
    
    public Texture2D GetImageInput()
    {
        if (image.texture == null)
        {
            return null;
        }
        return (Texture2D)image.texture;
    }

    public string GetNameInput() => nameInput.text;
    public int GetSizeInput() => int.Parse(servingSizeInput.text);
    public float GetCalorieInput() => float.Parse(caloriesInput.text);
    public float GetCarbsInput() => float.Parse(carbsInput.text);
    public float GetProteinInput() => float.Parse(proteinInput.text);
    public float GetFatInput() => float.Parse(fatInput.text);
    public FoodType GetFoodType() => foodTypes[0];
    
    

    public void RotateImageforScanning()
    {
        image.transform.eulerAngles = new Vector3(0, 0, -90);
    }

    public void ResetRotation()
    {
        image.transform.eulerAngles = Vector3.zero;
    }

    public void SetImageTexture(Texture2D texture)
    {
        image.texture = texture;
        image.material.mainTexture = texture;
    }
    
    
    public void UpdateFoodManual()
    {
        if (displayedFood == null)
        {
            displayedFood = new Food();
        }

        displayedFood.calories = GetCalorieInput();
        displayedFood.carbs = GetCarbsInput();
        displayedFood.protein = GetProteinInput();
        displayedFood.fat = GetFatInput();
    }

    public void ClearFields()
    {
        image.texture = null;
        image.material.mainTexture = null;
        nameInput.text = string.Empty;
        servingSizeInput.text = 100.ToString();
        carbsInput.text = 0.ToString();
        caloriesInput.text = 0.ToString();
        proteinInput.text = 0.ToString();
        fatInput.text = 0.ToString();
        displayedFood = null;
    }

    public void HideTitles()
    {
        foreach (var title in titles)
        {
            title.gameObject.SetActive(false);
        }
    }

    public void ShowMeal(Meal meal)
    {
        nameInput.text = meal.name;
        servingSizeInput.gameObject.SetActive(false);
        carbsInput.text = meal.mealTotal.carbs.ToString();
        caloriesInput.text = meal.mealTotal.calories.ToString();
        proteinInput.text = meal.mealTotal.protein.ToString();
        fatInput.text = meal.mealTotal.fat.ToString();
    }

    public void ShowFood(Food food)
    {
        if (food.TextureData != null)
        {
            image.texture = TextureUtils.GetTextureFromData(food.TextureData);
        }

        displayedFood = food;
        nameInput.text = food.name;
        servingSizeInput.text = food.normalPortionSize.ToString();
        carbsInput.text = food.carbs.ToString();
        caloriesInput.text = food.calories.ToString();
        proteinInput.text = food.protein.ToString();
        fatInput.text = food.fat.ToString();
        typeInput.value = foodTypes.IndexOf(food.foodType);
    }
}