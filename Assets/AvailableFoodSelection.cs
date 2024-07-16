using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using Menus;
using Singletons;
using UnityEngine;

public class AvailableFoodSelection : MonoBehaviour
{
    private const string PATH = "FoodSelectionUI";
    private FoodSelectUI selectUI;
    [SerializeField] private Transform contentParent;
    private FoodSelectUI selectUIGO;
    
    [SerializeField] private bool moltiSelect;

    protected void Start()
    {
       // base.Start();
 
   //     MenuManager.Instance.AddMenu<AvailableFoodSelection>(this);
        
        selectUI = Resources.Load<FoodSelectUI>(PATH);
        Init();
    }
    
    private void HandleFoodSelected(Food food)
    {
        if (moltiSelect)
        {
            
        }
    }

    private void OnEnable()
    {
        AddFoodMenu.OnFoodAdded += OnAddFood;
    }
    
    private void OnAddFood(Food food)
    {
        selectUIGO = Instantiate(selectUI, contentParent);
        selectUIGO.Init(food, HandleFoodSelected);
    }


    public void Init()
    {
        var filePaths = PathUtils.GetAllFoods();
        if (filePaths == null)
        {
            return;
        }

        foreach (string filePath in filePaths)
        {
            string json = File.ReadAllText(filePath);
            Food food = JsonUtility.FromJson<Food>(json);
            selectUIGO = Instantiate(selectUI, contentParent);

            selectUIGO.Init(food, HandleFoodSelected);
        }
    }

    private void OnDisable()
    {
        AddFoodMenu.OnFoodAdded -= OnAddFood;
    }
}