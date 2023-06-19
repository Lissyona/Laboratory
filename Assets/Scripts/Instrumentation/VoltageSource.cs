using TMPro;
using UnityEngine;

namespace StarterAssets.Instrumentation
{
    // Источник напряжения
    public class VoltageSource : Instrument
    {
        [SerializeField] private TextMeshProUGUI vText;
        [SerializeField] private TextMeshProUGUI aText;
        [SerializeField] private WorldButton vBtn;
        [SerializeField] private WorldButton aBtn;

        [SerializeField] private GameObject canvases;
        
        [Range(0, 3)] public float A;
        [Range(0, 15)] public float V;

        private GameController gameController;
        
        private void Start()
        {
            vBtn.OnButtonMinus += VBtnOnOnButtonMinus;
            vBtn.OnButtonPlus += VBtnOnOnButtonPlus;
            
            aBtn.OnButtonMinus += ABtnOnOnButtonMinus;
            aBtn.OnButtonPlus += ABtnOnOnButtonPlus;
        }

        private void OnEnable()
        {
            gameController = FindObjectOfType<GameController>();
            gameController.SchemaIsReady += GameControllerOnSchemaIsReady;
        }

        private void OnDisable()
        {
            gameController.SchemaIsReady -= GameControllerOnSchemaIsReady;
        }

        private void GameControllerOnSchemaIsReady(bool isReady)
        {
            canvases.SetActive(isReady);
        }

        private void ABtnOnOnButtonPlus()
        {
            A = Mathf.Clamp(A + 0.1f, 0, 3);
        }
        
        private void ABtnOnOnButtonMinus()
        {
            A = Mathf.Clamp(A - 0.1f, 0, 3);
        }

        private void VBtnOnOnButtonPlus()
        {
            V = Mathf.Clamp(V + 0.1f, 0, 15);
        }

        private void VBtnOnOnButtonMinus()
        {
            V = Mathf.Clamp(V - 0.1f, 0, 15);
        }

        public void Update()
        {
            vText.text = V.ToString("0.0");
            aText.text = A.ToString("0.00");
        }
    }
}