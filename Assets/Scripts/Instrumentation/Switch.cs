using StarterAssets;
using TMPro;
using UnityEngine;

namespace Instrumentation
{
    /// <summary>
    /// Переключатель от 0 до 9
    /// </summary>
    public class Switch : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI valueText;
        [SerializeField] private WorldButton worldButton;

        private int value;
        
        public int Value
        {
            get => value;

            set
            {
                this.value = Mathf.Clamp(value, 0, 9);
                valueText.text = this.value.ToString();
            }
        }
        
        private void OnEnable()
        {
            Value = 0;
            worldButton.OnButtonPlus += WorldButtonOnOnButtonPlus;
            worldButton.OnButtonMinus += WorldButtonOnOnButtonMinus;
        }
        
        private void OnDisable()
        {
            worldButton.OnButtonPlus -= WorldButtonOnOnButtonPlus;
            worldButton.OnButtonMinus -= WorldButtonOnOnButtonMinus;
        }

        private void WorldButtonOnOnButtonPlus()
        {
            Value++;
        }

        private void WorldButtonOnOnButtonMinus()
        {
            Value--;
        }
    }
}