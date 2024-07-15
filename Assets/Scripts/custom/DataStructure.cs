using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Newtonsoft.Json;
using UnityEngine;
using UnityEngine.Serialization;

public class DataStructure
{

}



public enum FoodType
{
    None,
    Yoghurt,
    Sauce,
    Meat,
    FruitRveg,
    dairy,
    Bread,
    Beverages,
    Other
}

public enum MealType
{
    None,
    Breakfast,
    Lunch,
    Dinner,
    Snack
}

[Serializable]
public class Food
{
    [FormerlySerializedAs("Texture")] public TextureData TextureData;
    public string name;
    public float normalPortionSize;

    public float calories;
    public float carbs;
    public float protein;
    public float fat;
    public FoodType foodType;
}

[Serializable]
public class Meal
{
    public string name;
     public MealType mealType;
    public List<Food> foods;
    public MealTotalValues mealTotal;
}

[Serializable]
public class MealTotalValues
{
    public float calories;
    public float carbs;
    public float protein;
    public float fat;
}

public class DailyMeal
{
    public List<Meal> Meals;
}


[Serializable]
public class ProductData
{
    public string code;
    public ProductDetail product;
    public int status;
    public string status_verbose;
}

[Serializable]
public class ProductDetail
{
    public string image_front_small_url;
    public string product_name;
    public float carbohydrates_100g;
    
    [JsonProperty("energy-kcal_100g")]
    public int energy_kcal_100g;
    
    public float fat_100g;
   
    public float proteins_100g;
}

[Serializable]
public class TextureData
{
    public byte[] base64Data;
    public int width;
    public int height;
    public TextureFormat textureFormat;
}


