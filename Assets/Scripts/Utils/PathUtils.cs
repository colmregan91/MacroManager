using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEngine;
using UnityEngine.Rendering;

public class PathUtils
{
    public static string GetFoodPath(string type, string foodName)
    {
        string path = $"{GetInitialFoodPath()}/{type}";
        
        if(!Directory.Exists(path))
        {
            Directory.CreateDirectory(path);
        }
        
        return $"{path}/{foodName}.json";
    }
    
    public static string GetMealPath(string mealName)
    {
        string path = $"{GetInitialMealPath()}/{mealName}";
        
        if(!Directory.Exists(path))
        {
            Directory.CreateDirectory(path);
        }
        
        return $"{path}/{mealName}.json";
    }
    public static string GetIFoodPathByType(FoodType type)
    {
        return Application.persistentDataPath + $"/FoodBase/{type.ToString()}";
    }
    public static string GetInitialFoodPath()
    {
        return Application.persistentDataPath + "/FoodBase";
    }
   public static string GetInitialMealPath()
    {
        return Application.persistentDataPath + "/MealBase";
    }
    public static List<string> GetAllFoods()
    {
        var types = (FoodType[])Enum.GetValues(typeof(FoodType));
        List<string> allFoodList = new List<string>();
        for (int i = 1; i < types.Length; i++)
        {
            if (Directory.Exists($"{GetInitialFoodPath()}/{types[i].ToString()}"))
            {
                List<string> filePaths = Directory.GetFiles($"{GetInitialFoodPath()}/{types[i].ToString()}", "*.json").ToList();
                if (filePaths.Count > 0)
                {
                    allFoodList.AddRange(filePaths);
                }
            }
            else
            {
                Directory.CreateDirectory($"{GetInitialFoodPath()}/{types[i].ToString()}");
                Debug.Log($"{GetInitialFoodPath()}/{types[i].ToString()}   no exists" );
            }
        }

        return allFoodList;
    }
}
