using UnityEngine;

namespace Instrumentation
{
    /// <summary>
    /// Мост измерения сопротивления
    /// </summary>
    public class R329 : MonoBehaviour
    {
        [SerializeField] private ResistanceSwitch _resistanceSwitchR2;
        [SerializeField] private ResistanceSwitch _resistanceSwitchR3;
        [SerializeField] private Switch _switch_100;
        [SerializeField] private Switch _switch_10;
        [SerializeField] private Switch _switch_1;
        [SerializeField] private Switch _switch_01;
        [SerializeField] private Switch _switch_001;
        
        public float R1, R2, R3;
        
        private void Update()
        {
            R1 = _switch_1.Value + _switch_01.Value * 0.1f + _switch_001.Value * 0.01f + _switch_10.Value * 10 + _switch_100.Value * 100;
            R2 = _resistanceSwitchR2.R;
            R3 = _resistanceSwitchR3.R;
        }
    }
}