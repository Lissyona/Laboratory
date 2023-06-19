using StarterAssets;
using StarterAssets.Instrumentation;
using UnityEngine;

namespace Instrumentation
{
    /// <summary>
    /// вольтметр типа М-1106 класса точности 0.2
    /// </summary>
    public class M1106 : Instrument
    {
        [SerializeField] private Arrow arrow;
        [SerializeField] private Limits limits;

        public Arrow Arrow => arrow;
        
        private void OnEnable() => limits.SubscribeAndSetup();

        private void OnDisable() => limits.Unsubscribe();
    }
}