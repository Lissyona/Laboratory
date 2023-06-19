using System;
using StarterAssets;
using UnityEngine;

namespace Instrumentation
{
    /// <summary>
    /// Переключаткель по заданным сопротивлениям
    /// </summary>
    public class ResistanceSwitch : MonoBehaviour
    {
        [SerializeField] private ResistanceSwitchValue[] resistanceSwitchValues;

        private ResistanceSwitchValue currentValue;
        
        public int R => currentValue?.Value ?? 0;
        
        private void OnEnable()
        {
            foreach (var resistanceSwitchValue in resistanceSwitchValues)
            {
                resistanceSwitchValue.Cylinder.SetActive(false);
                resistanceSwitchValue.OnPressed += ResistanceSwitchValueOnOnPressed;
                resistanceSwitchValue.Subscribe();
            }
            
            ResistanceSwitchValueOnOnPressed(resistanceSwitchValues[0]);
        }
        
        private void OnDisable()
        {
            foreach (var resistanceSwitchValue in resistanceSwitchValues)
            {
                resistanceSwitchValue.Unsubscribe();
            }
        }
        
        private void ResistanceSwitchValueOnOnPressed(ResistanceSwitchValue resistanceSwitchValue)
        {
            currentValue?.Cylinder.SetActive(false);
            currentValue = resistanceSwitchValue;
            currentValue.Cylinder.SetActive(true);
        }
    }

    [Serializable]
    public class ResistanceSwitchValue
    {
        public int Value;
        public GameObject Cylinder;
        public WorldButton Button;

        public event Action<ResistanceSwitchValue> OnPressed;
        
        public void Subscribe()
        {
            Button.OnButtonPressed += ButtonOnOnButtonPressed;
        }

        public void Unsubscribe()
        {
            Button.OnButtonPressed -= ButtonOnOnButtonPressed;
        }

        private void ButtonOnOnButtonPressed()
        {
            OnPressed?.Invoke(this);
        }
    }
}