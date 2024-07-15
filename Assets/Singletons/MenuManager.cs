using System;
using System.Collections.Generic;
using Menus;
using UnityEngine;

namespace Singletons
{
    public class MenuManager : MonoSingleton<MenuManager>
    {
        public readonly Dictionary<Type, BaseMenu> MDictOfMenus = new Dictionary<Type, BaseMenu>();
        private BaseMenu _currentMenu;
        private BaseMenu _currentSubMenu;
        public Action<BaseMenu> OnMenuChanged;
        public void OpenMenu<TMenuType>() where TMenuType : BaseMenu
        {
            ChangeMenu(typeof(TMenuType));
        }

        public void AddMenu<TMenuType>(BaseMenu menu, bool setState = false) where TMenuType : BaseMenu
        {
            Type menuType = typeof(TMenuType);
            if(MDictOfMenus.ContainsKey(menuType))
            {
                Debug.LogWarning($"Menu Manager: already has a menu of type {menuType} stored, check if your loading duplicate menus");
            }

            //Create or replace the menu
            MDictOfMenus[menuType] = menu;
        
            if(setState)
            {
                OpenMenu<TMenuType>();
            }
            else
            {
                menu.CloseMenu();
            }
        }
    
        public BaseMenu GetMenu(Type menuType)
        {
            if(MDictOfMenus.TryGetValue(menuType, out var menu))
            {
                return menu;
            }

            return null;
        }
        
        public void OpenAsSubMenu(Type menuType)
        {
            if (_currentSubMenu != null)
            {
                _currentSubMenu.OnInactive();
                _currentSubMenu.CloseMenu();
            }
            
            _currentSubMenu = GetMenu(menuType);
            _currentSubMenu.OpenMenu();
            _currentSubMenu.OnActive();
        }
        public void ChangeMenu(Type menuType)
        {
            if (_currentMenu != null)
            {
                _currentMenu.OnInactive();
                _currentMenu.CloseMenu();
            }

   
            _currentMenu = GetMenu(menuType);
            _currentMenu.OpenMenu();
            _currentMenu.OnActive();
            OnMenuChanged?.Invoke(_currentMenu);
        }

        private void OnDisable()
        {
            MDictOfMenus.Clear();
        }
    }
}
