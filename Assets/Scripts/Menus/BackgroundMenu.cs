using Singletons;
using UnityEngine;
using UnityEngine.UI;

namespace Menus
{
    public class BackgroundMenu : BaseMenu
    {
        [SerializeField]private Button _backButton;

        private void HandleMenuChange(BaseMenu obj)
        {
            if (obj is not MainMenu)
            {
                OpenMenu();
            }
            else
            {
                CloseMenu();
            }
        }

        protected override void Start()
        {
            base.Start();
            
            _backButton.onClick.AddListener(OpenMainMenu);
            MenuManager.Instance.OnMenuChanged += HandleMenuChange;
        }

        

        private void OpenMainMenu()
        {
            MenuManager.Instance.ChangeMenu(typeof(MainMenu));
        }

        private void OnDisable()
        {
            _backButton.onClick.RemoveListener(OpenMainMenu);
            MenuManager.Instance.OnMenuChanged -= HandleMenuChange;

        }
    }
}