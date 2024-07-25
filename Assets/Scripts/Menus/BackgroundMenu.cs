using Singletons;
using UnityEngine;
using UnityEngine.UI;

namespace Menus
{
    public class BackgroundMenu : MonoBehaviour
    {
        [SerializeField]private Button _backButton;
        private CanvasGroup _group;
        private void HandleMenuChange(BaseMenu obj)
        {
            if (obj is not MainMenu)
            {
                _backButton.gameObject.SetActive(true);
            }
            else
            {
                _backButton.gameObject.SetActive(false);
            }
        }
        
        public void CloseMenu()
        {
            _group.interactable = false;
            _group.alpha = 0;
            _group.blocksRaycasts = false;
        }

        public void OpenMenu()
        {
            _group.interactable = true;
            _group.alpha = 1;
            _group.blocksRaycasts = true;
        }

        private void Start()
        {
            _group = GetComponent<CanvasGroup>();
            OpenMenu();
            _backButton.onClick.AddListener(OpenMainMenu);
            MenuManager.Instance.OnMenuChanged += HandleMenuChange;
        }

        

        private void OpenMainMenu()
        {
            MenuManager.Instance.OpenMenu<MainMenu>();
        }

        private void OnDisable()
        {
            _backButton.onClick.RemoveListener(OpenMainMenu);
            MenuManager.Instance.OnMenuChanged -= HandleMenuChange;

        }
    }
}