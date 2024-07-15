using Singletons;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

namespace Menus
{
    public class MainMenu : BaseMenu
    {
        [SerializeField] private Button addFoodButton;
        [SerializeField] private Button checkFoodButton;
        [SerializeField] private Button addMealButton;
        [SerializeField] private Button dailyMealButton;
        protected override void Start()
        {
            base.Start();
            MenuManager.Instance.AddMenu<MainMenu>(this,true);
        }
        

        private void OnEnable()
        {
            addFoodButton.onClick.AddListener(OpenAddFoodMenu);
            checkFoodButton.onClick.AddListener(OpenCheckMenu);
            addMealButton.onClick.AddListener(OpenAddMealMenu);
            dailyMealButton.onClick.AddListener(OpenDailyMealMenu);
        }

        private void OpenDailyMealMenu()
        {
            MenuManager.Instance.ChangeMenu(typeof(DailyMealMenu));
        }

        private void OpenAddFoodMenu()
        {
            MenuManager.Instance.ChangeMenu(typeof(AddFoodMenu));
        }
        private void OpenCheckMenu()
        {
            MenuManager.Instance.ChangeMenu(typeof(CheckFoodMenu));
        }
        private void OpenAddMealMenu()
        {
            MenuManager.Instance.ChangeMenu(typeof(AddMealMenu));
        }

        private void OnDisable()
        {
            addFoodButton.onClick.RemoveListener(OpenAddFoodMenu);
            checkFoodButton.onClick.RemoveListener(OpenCheckMenu);
            addMealButton.onClick.RemoveListener(OpenAddMealMenu);
            dailyMealButton.onClick.AddListener(OpenDailyMealMenu);
        }
    }
}