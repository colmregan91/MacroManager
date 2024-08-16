using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
public class FieldInteractabilityManager : MonoBehaviour
{
    
    [SerializeField] private Selectable[] nutrition;
    [SerializeField] private Selectable[] Weight;

    private UISwitcher.UISwitcher _switcher;

    [SerializeField] private TextMeshProUGUI switcherText;

    [SerializeField] private Color UniteractableColor;

    public UISwitcher.UISwitcher Switcher => _switcher;
    private void Awake()
    {
        _switcher = GetComponentInChildren<UISwitcher.UISwitcher>();

        HandleSwitcherChanged(true);
    }

    public void DeInit()
    {
        _switcher.interactable = false;
        _switcher.OnValueChanged -= HandleSwitcherChanged;
        _switcher.gameObject.SetActive(false);
    }
    public void Init()
    {
        _switcher.interactable = true;
        _switcher.OnValueChanged += HandleSwitcherChanged;
        _switcher.gameObject.SetActive(true);
    }

    // public void AddCallback(Action<bool> callback)
    // {
    //     _switcher.OnValueChanged += callback;
    //    
    // }
    //
    // public void RemoveCallback(Action<bool> callback)
    // {
    //     _switcher.OnValueChanged -= callback;
    //     _switcher.OnValueChanged -= HandleSwitcherChanged;
    // }

    private void HandleSwitcherChanged(bool val)
    {
        if (val)
        {
            SetNutritionInteractability();
            
        }
        else
        {
            SetWeightInteractability();
        }
    }
    public void SetNutritionInteractability()
    {
        foreach (var obj in Weight)
        {
            ColorBlock colors = obj.colors;
            colors.normalColor = UniteractableColor;
            colors.highlightedColor = UniteractableColor;
            colors.pressedColor = UniteractableColor;
            colors.selectedColor = UniteractableColor;
            colors.disabledColor = UniteractableColor;
            obj.colors = colors; 
            obj.interactable = false;
        }
        
        foreach (var obj in nutrition)
        {
            ColorBlock colors = obj.colors;
            colors.normalColor = _switcher.OnColor;
            colors.highlightedColor =_switcher.OnColor;
            colors.pressedColor = _switcher.OnColor;
            colors.selectedColor = _switcher.OnColor;
            colors.disabledColor = _switcher.OnColor;
            obj.colors = colors; 
            obj.interactable = true;
        }

        switcherText.text = "NUTRITION";
    }
    
    public void SetWeightInteractability()
    {
        foreach (var obj in Weight)
        {
            ColorBlock colors = obj.colors;
            colors.normalColor = _switcher.OnColor;
            colors.highlightedColor =_switcher.OnColor;
            colors.pressedColor = _switcher.OnColor;
            colors.selectedColor = _switcher.OnColor;
            colors.disabledColor = _switcher.OnColor;
    
            obj.interactable = true;
            obj.colors = colors; 
        }
        
        foreach (var obj in nutrition)
        {
            ColorBlock colors = obj.colors;
            colors.normalColor = UniteractableColor;
            colors.highlightedColor = UniteractableColor;
            colors.pressedColor = UniteractableColor;
            colors.selectedColor = UniteractableColor;
            colors.disabledColor = UniteractableColor;
            obj.colors = colors; 
            obj.interactable = false;
        }
        switcherText.text = "GRAMS";
    }
}
