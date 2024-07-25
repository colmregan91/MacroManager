using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class FoodFetcher : MonoSingleton<FoodFetcher>
{
    public List<Food> AllFoods = new List<Food>();

    public void Init()
    {
        AllFoods.Clear();
        var filePaths = PathUtils.GetAllFoods();
        if (filePaths == null)
        {
            return;
        }

        foreach (string filePath in filePaths)
        {
            string json = File.ReadAllText(filePath);
            Food food = JsonUtility.FromJson<Food>(json);
            AllFoods.Add(food);
        }
    }
}
