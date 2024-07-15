using System.Collections;
using System.Collections.Generic;
using Singletons;
using UnityEngine;

namespace Menus
{
    public class DailyMealMenu : BaseMenu
    {
        private const string PATH = "FoodInfoDisplay";
        private GameObject _fieldObj;
        protected override void Start()
        {
            base.Start();
            MenuManager.Instance.AddMenu<DailyMealMenu>(this);

        _fieldObj = Resources.Load<GameObject>(PATH);
        }


   
    }
}