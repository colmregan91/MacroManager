using System;
using System.Collections.Generic;
using UnityEngine;

namespace Menus
{
    public class BaseMenu : MonoBehaviour
    {
        private CanvasGroup _group;
        private Dictionary<Type, BaseMenu> MDictOfSubMenus;
        
        protected virtual void Start()
        {
            _group = GetComponent<CanvasGroup>();
        }
        
        public virtual void OnActive()
        {
        
        }
        public virtual void OnInactive()
        {
            
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



    }
}