using StarterAssets;
using StarterAssets.Instrumentation;
using UnityEngine;

namespace Instrumentation
{
    /// <summary>
    /// амперметр типа М-104 класса точности 0.5
    /// </summary>
    public class M104 : Instrument
    {
        [SerializeField] private Arrow arrow;
        [SerializeField] private Limits limits;
        
        public Arrow Arrow => arrow;

        private void OnEnable() => limits.SubscribeAndSetup();

        private void OnDisable() => limits.Unsubscribe();
    }
}