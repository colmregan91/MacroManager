using System;
using System.Collections;
using System.Collections.Generic;
using Menus;
using Singletons;
using UnityEngine;

public class BootUp : MonoBehaviour
{
    private void Awake()
    {
        WebRequestManager.CreateInstance();
        MenuManager.CreateInstance();
        PopUpManager.CreateInstance();
         FoodFetcher.CreateInstance();
         FoodFetcher.Instance.Init();
    }
}
