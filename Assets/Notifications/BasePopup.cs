using TMPro;
using UnityEngine;

namespace Notifications
{
    public class BasePopup : MonoBehaviour
    {
        private CanvasGroup _group;
        protected TextMeshProUGUI _messageText;


        // Start is called before the first frame update
        public virtual void Awake()
        {
            _group = GetComponentInChildren<CanvasGroup>();
            _messageText = GetComponentInChildren<TextMeshProUGUI>();
          

       
        }
        
        public virtual void OnEnable()
        {
           
        }

        public void HideCanvasGroup()
        {
            _group.interactable = false;
            _group.alpha = 0;
            _group.blocksRaycasts = false;
        }

        public void ShowCanvasGroup()
        {
            _group.interactable = true;
            _group.alpha = 1;
            _group.blocksRaycasts = true;
        }

        protected virtual void HidePopup()
        {
            HideCanvasGroup();
            _messageText.text = string.Empty;
        }

        public virtual void OnDisable()
        {
           
        }
    }
}