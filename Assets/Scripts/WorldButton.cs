using System;
using UnityEngine;

namespace StarterAssets
{
    public class WorldButton : MonoBehaviour
    {
        public event Action OnButtonPressed;
        public event Action OnButtonPlus;
        public event Action OnButtonMinus;

        [SerializeField] private GameObject buttonGO;
        private bool showOneMoreFrame;

        private void Start()
        {
            Show(false);
        }

        public void Show(bool show)
        {
            if(buttonGO != null) buttonGO.SetActive(show);
            showOneMoreFrame = show;
        }

        public void Plus()
        {
            OnButtonPlus?.Invoke();
        }

        public void Minus()
        {
            OnButtonMinus?.Invoke();
        }

        public void Interact()
        {
            OnButtonPressed?.Invoke();
        }
        
        private void LateUpdate()
        {
            if (showOneMoreFrame)
            {
                showOneMoreFrame = false;
                return;
            }
            
            Show(false);
        }
        
        public void Press()
        {
            OnButtonPressed?.Invoke();
        }
    }
}