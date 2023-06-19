using StarterAssets.Instrumentation;
using TMPro;
using UnityEngine;

namespace Instrumentation
{
    public class GDM8135 : Instrument
    {
        [SerializeField] private GameController gameController;
        [SerializeField] private TextMeshProUGUI numbers;
        [SerializeField] private R33ResistanceBox resistanceBox;
        [SerializeField] private R329 r329;

        private bool isReady; 
        
        private void OnEnable()
        {
            gameController.SchemaIsReady += GameControllerOnSchemaIsReady;
        }

        private void OnDisable()
        {
            gameController.SchemaIsReady -= GameControllerOnSchemaIsReady;
        }

        private void Update()
        {
            if (!isReady)
            {
                numbers.gameObject.SetActive(false);
                return;
            }
            
            numbers.gameObject.SetActive(true);
            var value = Mathf.Clamp(((r329.R2 / r329.R3) * r329.R1), 0.1f, float.MaxValue) / Mathf.Clamp(resistanceBox.R, 0.1f, float.MaxValue);
            value -= value * 0.05f;
            value -= 1;
            value = Mathf.Abs(value);
            numbers.text = value.ToString("0.000");
        }

        private void GameControllerOnSchemaIsReady(bool isReady)
        {
            this.isReady = isReady;
        }
    }
}