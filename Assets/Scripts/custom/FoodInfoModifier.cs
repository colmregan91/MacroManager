using System;
using UnityEngine;
using UnityEngine.UI;

public class FoodInfoModifier : FoodDisplayer
{
    private float XGrams => ModifiedFood.normalPortionSize;

    [SerializeField] private Button incrementButton;
    [SerializeField] private Button decrementButton;

    public Action OnFoodModified;
    public Action<FoodInfoModifier> OnFoodRemoved;
    private Food _modifiedFood;
    private UISwitcher.UISwitcher _switcher;
    public Food ModifiedFood => _modifiedFood ?? DisplayedFood;

    private void Awake()
    {
        _switcher = GetComponentInChildren<UISwitcher.UISwitcher>();
        
        // image.texture = defaultTexture;
        // image.material.mainTexture = defaultTexture;
    }

    private void Start()
    {
        HandleSwitcherChanged(true);
    }

    public void RemoveFood()
    {
        OnFoodRemoved?.Invoke(this);
    }

    public void IncrementAmount()
    {
        var g = float.Parse(servingSizeInput.text);
        g += 5;
        servingSizeInput.text = g.ToString();
    }

    public void DecrementAmount()
    {
        var g = float.Parse(servingSizeInput.text);
        g -= 5;
        servingSizeInput.text = g.ToString();
    }

    private void OnEnable()
    {
        incrementButton.onClick.AddListener(IncrementAmount);
        decrementButton.onClick.AddListener(DecrementAmount);
        servingSizeInput.onValueChanged.AddListener(ModifyNutrition);

        if (_switcher != null)
        {
            _switcher.OnValueChanged += HandleSwitcherChanged;  
        }
    
    }

    private void HandleSwitcherChanged(bool val)
    {
        if (val) // modifying nutrition
        {
            servingSizeInput.text = 100.ToString();
        }
        else  // modifying grams
        {
           
            UpdateFoodManual();
        }
    }

    private void ModifyNutrition(string targetAmount)
    {
        if (string.IsNullOrEmpty(targetAmount))
        {
            return;
        }


        float yGrams;
        if (float.TryParse(targetAmount, out yGrams))
        {
            var modifier = yGrams / XGrams;

            var targetCalories = Mathf.Ceil(modifier * ModifiedFood.calories * 10f) / 10f;
            var targetCarbs = Mathf.Ceil(modifier * ModifiedFood.carbs * 10f) / 10f;
            var targetProtein = Mathf.Ceil(modifier * ModifiedFood.protein * 10f) / 10f;
            var targetFat = Mathf.Ceil(modifier * ModifiedFood.fat * 10f) / 10f;


            carbsInput.text = targetCarbs.ToString();
            caloriesInput.text = targetCalories.ToString();
            proteinInput.text = targetProtein.ToString();
            fatInput.text = targetFat.ToString();

            if (_modifiedFood != null)
            {
                _modifiedFood.calories = targetCalories;
                _modifiedFood.carbs = targetCarbs;
                _modifiedFood.protein = targetProtein;
                _modifiedFood.fat = targetFat;
                OnFoodModified?.Invoke();
            }
           
        }
        else
        {
            Debug.LogError("parsing failed");
        }
    }

    private void OnDisable()
    {
        servingSizeInput.onValueChanged.RemoveListener(ModifyNutrition);
        incrementButton.onClick.RemoveListener(IncrementAmount);
        decrementButton.onClick.RemoveListener(DecrementAmount);
        if (_switcher != null)
        {
            _switcher.OnValueChanged -= HandleSwitcherChanged;
        }
    }
}