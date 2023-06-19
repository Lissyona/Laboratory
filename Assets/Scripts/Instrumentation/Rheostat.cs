using StarterAssets;
using StarterAssets.Instrumentation;
using TMPro;
using UnityEngine;

namespace Instrumentation
{
    public class Rheostat : Instrument
    {
        [SerializeField] private float maxResistance;
        [SerializeField] private TextMeshProUGUI resText;
        [SerializeField] private WorldButton controlBtn;
        [SerializeField] private GameObject block;
        [SerializeField] private Vector2 blockPositions;

        private float currentRes;
        private float value;
        
        private void Start()
        {
            block.transform.localPosition =
                new Vector3(
                    block.transform.localPosition.x,
                    block.transform.localPosition.y,
                    blockPositions[0]);
            
            value = (Mathf.Abs(blockPositions[0]) + Mathf.Abs(blockPositions[1])) / 20f;
            controlBtn.OnButtonPlus += ControlBtnOnOnButtonPlus;
            controlBtn.OnButtonMinus += ControlBtnOnOnButtonMinus;
        }

        private void Update()
        {
            currentRes = Mathf.InverseLerp(blockPositions[0], blockPositions[1],block.transform.localPosition.z) * maxResistance;
            resText.text = currentRes.ToString("0.00") + "Ом";
        }

        private void ControlBtnOnOnButtonPlus()
        {
           SetBlockZPos(true);
        }

        private void ControlBtnOnOnButtonMinus()
        {
            SetBlockZPos(false);
        }

        private void SetBlockZPos(bool plus)
        {
            block.transform.localPosition =
                new Vector3(
                    block.transform.localPosition.x,
                    block.transform.localPosition.y,
                    Mathf.Clamp(block.transform.localPosition.z + (plus ? 1 : -1) * value, blockPositions[0], blockPositions[1]));
        }
    }
}