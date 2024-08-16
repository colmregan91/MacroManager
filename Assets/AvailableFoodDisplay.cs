using System;
using System.Collections;
using System.Collections.Generic;
using Menus;
using Singletons;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class AvailableFoodDisplay : BaseMenu
{
    
    private List<FoodInfoModifier> _modifierList = new List<FoodInfoModifier>();
    private Queue<FoodInfoModifier> _availableModifiers = new Queue<FoodInfoModifier>();
    private FoodInfoModifier _modifierPrefab;

    [SerializeField] private RectTransform displayHolder;
    [SerializeField] private Button AddMoreFoodsButton;
    [SerializeField] private Button NextButton;
    [SerializeField] private Button PrevButton;
    [SerializeField] private int _currentIndex = 0;
    private float _spacing;
    private float _baseWidth;
    protected override void Start()
    {
        base.Start();
        _modifierPrefab = Resources.Load<FoodInfoModifier>("FoodInfoDisplay");
        MenuManager.Instance.AddMenu<AvailableFoodDisplay>(this);
        _spacing = GetComponentInChildren<HorizontalLayoutGroup>().spacing;
        _baseWidth = _modifierPrefab.GetComponent<RectTransform>().rect.width;
    }

    private void OnEnable()
    {
        NextButton.onClick.AddListener(MoveToNext);
        PrevButton.onClick.AddListener(MoveToPrevious);
    }
    
    private void OnDisable()
    {
        NextButton.onClick.RemoveListener(MoveToNext);
        PrevButton.onClick.RemoveListener(MoveToPrevious);
    }



    private void ManageButtons(bool checking, int foodCount)
    {
        if (checking)
        {
            AddMoreFoodsButton.gameObject.SetActive(true);
            AddMoreFoodsButton.onClick.AddListener(MenuManager.OpenFoodSelectionMenu);
        }
        else
        {
            AddMoreFoodsButton.onClick.RemoveListener(MenuManager.OpenFoodSelectionMenu);
            AddMoreFoodsButton.gameObject.SetActive(false);
        }

     
        _currentIndex = 0;
        ScrollToCurrentIndex();

        PrevButton.gameObject.SetActive(false);
        if (foodCount > 1)
        {
            NextButton.gameObject.SetActive(true);
        }
        else
        {
            NextButton.gameObject.SetActive(false);
        
        }
    }

    private void PopulateFields(List<Food> foodList) 
    {
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

    public override void OnActive()
    {
        bool checking = FoodDisplayMenu.displaySchema.displayType == FoodDisplaySchema.DisplayType.CheckingFood;
        var foodList = FoodDisplayMenu.displaySchema.foods;

        ManageButtons(checking, foodList.Count);
        PopulateFields(foodList);

    }
    
    private void ScrollToCurrentIndex()
    {
        float newPositionX = (-_currentIndex * (_spacing + _baseWidth));

        displayHolder.anchoredPosition =  new Vector2(newPositionX, 0f); // todo : import tween and lerp it
        
      
    }
    
    public void MoveToNext()
    {
     
        if (_currentIndex <= _modifierList.Count-2)
        {
            _currentIndex++;
            ScrollToCurrentIndex();
            PrevButton.gameObject.SetActive(_currentIndex > 0);
            NextButton.gameObject.SetActive(_currentIndex <= _modifierList.Count-2);
        }

     
    }
    

    public void MoveToPrevious()
    {
 
        if (_currentIndex > 0)
        {
            _currentIndex--;
            ScrollToCurrentIndex();
       
            PrevButton.gameObject.SetActive(_currentIndex > 0);
            NextButton.gameObject.SetActive(_currentIndex <= _modifierList.Count-2);
        }
    }



    public override void OnInactive()
    {
        
        AddMoreFoodsButton.onClick.RemoveListener(MenuManager.OpenFoodSelectionMenu);
        AddMoreFoodsButton.gameObject.SetActive(false);
        foreach (var displayer in _modifierList)
        {
            displayer.ClearFields();
            displayer.DeInit();
            _availableModifiers.Enqueue(displayer);
        }

        _modifierList.Clear();
    }
    
}