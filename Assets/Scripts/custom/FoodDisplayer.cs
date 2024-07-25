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

    private Material imageMaterial;
    private List<TMP_Dropdown.OptionData> _options = new List<TMP_Dropdown.OptionData>();
    private List<FoodType> foodTypes = new List<FoodType>();
    public Texture2D defaultTex;
    public Food DisplayedFood => displayedFood;
    
    public Texture2D GetImageInput()
    {
        if (image.texture == null)
        {
            return null;
        }
        return (Texture2D)image.texture;
    }

    public string GetNameInput() =>string.IsNullOrEmpty(nameInput.text) ? " " : nameInput.text;
    public int GetSizeInput() => string.IsNullOrEmpty(caloriesInput.text) ? 0 : int.Parse(servingSizeInput.text);
    public float GetCalorieInput() => string.IsNullOrEmpty(caloriesInput.text) ? 0 : float.Parse(caloriesInput.text);
    public float GetCarbsInput() => string.IsNullOrEmpty(carbsInput.text) ? 0 : float.Parse(carbsInput.text);
    public float GetProteinInput() =>  string.IsNullOrEmpty(proteinInput.text) ? 0 : float.Parse(proteinInput.text);
    public float GetFatInput() =>   string.IsNullOrEmpty(fatInput.text) ? 0 : float.Parse(fatInput.text);
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
        
        if (image.material == null || image.material.name == image.material.shader.name + " (Instance)")
        {
            image.material = new Material(image.material);
        }

        // Set the new texture on the RawImage's material
        image.material.mainTexture = texture;
        image.texture = texture;
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
    
    public void ShowFood(FoodDisplaySchema schema)
    {
        if (schema.food.TextureData != null)
        {
            SetImageTexture(TextureUtils.GetTextureFromData(schema.food.TextureData));
        }
        else
        {   image.material.mainTexture = defaultTex;
            image.texture = defaultTex;
            
        }

        displayedFood = schema.food;
        nameInput.text = schema.food.name;
        servingSizeInput.text = schema.food.normalPortionSize.ToString();
        carbsInput.text = schema.food.carbs.ToString();
        caloriesInput.text = schema.food.calories.ToString();
        proteinInput.text = schema.food.protein.ToString();
        fatInput.text = schema.food.fat.ToString();
        typeInput.value = foodTypes.IndexOf(schema.food.foodType);
    }
}