using System.Collections;
using System.Collections.Generic;
using System.IO;
using Menus;
using Singletons;
using UnityEngine;
using Utils;

public class FoodValidator : MonoBehaviour
{
    private FoodInfoModifier modifier;

    // Start is called before the first frame update
    void Start()
    {
        modifier = GetComponent<FoodInfoModifier>();
    }
    
    private bool HasNutritionBeenEntered()
    {
        return !(modifier.GetCalorieInput() == 0 && modifier.GetProteinInput() == 0 &&  modifier.GetFatInput() == 0 &&  modifier.GetCarbsInput() == 0);
    }

    private void HandleAddButtonClicked()
    {
        if (modifier.GetImageInput() == null || modifier.GetNameInput().Length < 3)
        {
            PopUpManager.Instance.ShowPopupMessage("Please ensure image and name of food has been entered.");
            return;
        }

        if (HasNutritionBeenEntered() == false)
        {
            PopUpManager.Instance.ShowPopupMessage("No nutritional information has been entered.");
            return;
        }

        PopUpManager.Instance.ShowQuestionMessage("Please ensure data is correct.  Accuracy of scanned products is not guarateed.", "continue", "Go Back", 
            () => SaveNewFood(),
            null);
        // if tests pass popupmanager, say make sure image is clear and that data matches nutri info as sometimes server gets it wrong, option to add food or to go back 

    }

    public void SaveNewFood()
    {
        var tex = modifier.GetImageInput();
            Food newFood = new Food
            {
                TextureData = TextureUtils.GetDataFromTexture(tex),
                foodType = FoodType.Other,
                name = modifier.GetNameInput(),
                normalPortionSize = modifier.GetSizeInput(),
                carbs = modifier.GetCarbsInput(),
                calories = modifier.GetCalorieInput(),
                protein = modifier.GetProteinInput(),
                fat = modifier.GetFatInput(),
            };

            string json = JsonUtility.ToJson(newFood);
            string path = PathUtils.GetFoodPath(newFood.foodType.ToString(), newFood.name);
            File.WriteAllText(path, json);

            modifier.ClearFields();
            AddFoodMenu.OnFoodAdded?.Invoke(newFood);
            PopUpManager.Instance.ShowPopupMessage($"{name} has been successfully added.");
        }
    }