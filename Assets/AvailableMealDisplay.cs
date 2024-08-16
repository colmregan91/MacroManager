using System.Collections;
using System.Collections.Generic;
using Menus;
using Singletons;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class AvailableMealDisplay : BaseMenu
{
    private FoodDisplaySchema _schema;
    private List<FoodInfoModifier> _modifierList = new List<FoodInfoModifier>();
    private Queue<FoodInfoModifier> _availableModifiers = new Queue<FoodInfoModifier>();
    private FoodInfoModifier _modifierPrefab;

    [SerializeField] private RectTransform displayHolder;
    [SerializeField] private Button addMoreFoodsButton;
    [SerializeField] private Button nextButton;
    [SerializeField] private Button prevButton;
    [SerializeField] private int currentIndex = 0;
    private float _spacing;
    private float _baseWidth;
    protected override void Start()
    {
        base.Start();
        MenuManager.Instance.AddMenu<AvailableMealDisplay>(this);
        _modifierPrefab = Resources.Load<FoodInfoModifier>("FoodInfoDisplay");
    }
        
    public override void OnActive()
    {
        
    }

    private void InitMealDisplay()
    {
        
    }
    
    private void PopulateFields(List<Food> foodList) 
    {
        Instantiate(_modifierPrefab, displayHolder);
        
        for (int i = 0; i < foodList.Count; i++)
        {
            FoodInfoModifier modifier;

            if (_availableModifiers.Count > 0)
            {
                modifier = _availableModifiers.Dequeue();

                modifier.gameObject.SetActive(true);
            }
            else
            {
                Debug.Log("adding mid");
                modifier = Instantiate(_modifierPrefab, displayHolder);
          
            }
            
            _modifierList.Add(modifier);
        
            modifier.Init();
            modifier.ClearFields();
            modifier.ShowFood(foodList[i]);
        }
    }
}
