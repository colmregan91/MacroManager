using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using UnityEngine;

public class FoodFetcher : MonoSingleton<FoodFetcher>
{
    public List<Food> AllFoods = new List<Food>();
    private CancellationTokenSource cancellationTokenSource;
    public Action<Food> OnFoodSerialized;
    public void Init()
    {
        GetAllFoods();
    }

    private void OnDisable()
    {
        // Cancel the operation if the GameObject is disabled or destroyed
        cancellationTokenSource?.Cancel();
    }

    private async void GetAllFoods() // look into making this init on load - runtime init on load attribute
    {
        cancellationTokenSource = new CancellationTokenSource();
        AllFoods.Clear();
        var filePaths = PathUtils.GetAllFoods();
        if (filePaths == null)
        {
            return;
        }

        foreach (string filePath in filePaths)
        {
            try
            {
                string json = File.ReadAllText(filePath);
                Food food = await DeserializeJsonAsync<Food>(json, cancellationTokenSource.Token);
                
                AllFoods.Add(food);
                OnFoodSerialized?.Invoke(food);
            }
            catch (OperationCanceledException)
            {
                Debug.Log("Deserialization was canceled.");
            }
    
        }
    }

    private Task<T> DeserializeJsonAsync<T>(string json, CancellationToken cancellationToken)
    {
        return Task.Run(() =>
        {
            cancellationToken.ThrowIfCancellationRequested();
            
            T result = JsonUtility.FromJson<T>(json);

            cancellationToken.ThrowIfCancellationRequested();

            return result;
        }, cancellationToken);
    }
}


