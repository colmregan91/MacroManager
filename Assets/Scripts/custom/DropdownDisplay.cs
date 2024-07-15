using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using TMPro;
using UnityEngine;

public class DropdownDisplay : MonoBehaviour
{
    private TMP_Dropdown _dropdown;
    private List<TMP_Dropdown.OptionData> _options = new List<TMP_Dropdown.OptionData>();
    protected readonly List<Food> DisplayedFoods = new List<Food>();

    [SerializeField] private Transform toggleHolder;
    private const string FOODTOGGLEPATH = "FoodToggle";
    private void Awake()
    {
        // var types = (FoodType[])Enum.GetValues(typeof(FoodType)); // todo move to dedicated class rather that using it everywhere - enum utils
        //
        // for (int i = 1; i < types.Length; i++)
        // {
        //     var toggle = Resources.Load<FoodToggle>(FOODTOGGLEPATH);
        //     var FoodToggle = Instantiate(toggle, toggleHolder);
        //     FoodToggle.Init(types[i], FilterFoods);
        // }
        
        
        Init();
        

    }

    private void FilterFoods(bool isOn,FoodType type)
    {
        var removedDisplay = DisplayedFoods.Where((t => isOn ?  t.foodType != type :  t.foodType == type)).ToArray();
        
        // needs thinking 
    }

    public void Init()
    {
        _dropdown = GetComponent<TMP_Dropdown>();
        _options.Clear();
        _dropdown.ClearOptions();
        DisplayedFoods.Clear();

        var filePaths = PathUtils.GetAllFoods();
        if (filePaths == null)
        {
            return;
        }
        
        foreach (string filePath in filePaths)
        {
            string json = File.ReadAllText(filePath);

            Food food = JsonUtility.FromJson<Food>(json);

            TMP_Dropdown.OptionData option = new TMP_Dropdown.OptionData(food.name);
            _options.Add(option);
            DisplayedFoods.Add(food);
        }

        _dropdown.AddOptions(_options);
        _dropdown.onValueChanged.AddListener(HandleDropdownSelection);
        _dropdown.value = 0;
    }

    private void OnDisable()
    {
        _dropdown.onValueChanged.RemoveListener(HandleDropdownSelection);
    }

    public virtual void HandleDropdownSelection(int selection)
    {
        _dropdown.value = selection;
        var food = DisplayedFoods[selection];
     //   OnFoodSelected?.Invoke(food);
    }

    //
    //   dropdown.ClearOptions();

    // var chainEnumNames = Enum.GetNames(typeof(DexToolsChain));
    //
    // List<TMP_Dropdown.OptionData> options = new List<TMP_Dropdown.OptionData>();
    //
    //     foreach (var chain in TickerManager.Instance.GetDexToolsChains)
    // {
    //     var id = char.IsDigit(chain.Value[0]) ? $"_{chain.Value}" : chain.Value;
    //
    //     if (chainEnumNames.Contains(id) == false)
    //     {
    //         continue;
    //     }
    //
    //     existingChains.Add(chain.Key, chain.Value);
    //     TMP_Dropdown.OptionData option = new TMP_Dropdown.OptionData(chain.Key.ToUpper());
    //     options.Add(option);
    // }
    //
    // chainIDs = existingChains.Values.ToList();
    //
    // // Add the options to the dropdown
    // dropdown.AddOptions(options);
    //
    //
    // // Refresh the dropdown to update the UI
    // dropdown.RefreshShownValue();
    // dropdown.onValueChanged.AddListener(ChangeNetwork);
    // dropdown.value = chainIDs.IndexOf(_data.chain.ToString());
}