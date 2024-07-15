using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class FoodToggle : MonoBehaviour
{
    private TextMeshProUGUI _labelText;
    private Toggle _toggle;
    private Action<bool, FoodType> _callback;
    private FoodType _foodType;
    private void Awake()
    {
        _labelText = GetComponentInChildren<TextMeshProUGUI>();
        _toggle = GetComponent<Toggle>();
    }

    public void Init(FoodType type, Action<bool,FoodType> onToggleChanged)
    {
        _foodType = type;
        _labelText.text = type.ToString();
        _toggle.isOn = true;
        _callback = onToggleChanged;
        _toggle.onValueChanged.AddListener((isOn)=> _callback(isOn, _foodType));
    }

    private void OnDisable()
    {
        _toggle.onValueChanged.RemoveListener((isOn)=> _callback(isOn,_foodType));
    }
}
