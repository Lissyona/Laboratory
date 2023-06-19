using System;
using StarterAssets;
using UnityEngine;

namespace Instrumentation
{
    /// <summary>
    /// Набор пределов на приборе
    /// </summary>
    [Serializable]
    public class Limits
    {
        [SerializeField] private Limit[] limits;
        [SerializeField] private WorldButton worldButton;

        private int _curLim = 0;

        public void SubscribeAndSetup()
        {
            worldButton.OnButtonPlus += WorldButtonOnOnButtonPlus;
            worldButton.OnButtonMinus += WorldButtonOnOnButtonMinus;

            for (var i = 0; i < limits.Length; i++)
            {
                var limit = limits[i];
                limit.Indicator.SetActive(_curLim == i);
            }
        }
        
        public void Unsubscribe()
        {
            worldButton.OnButtonPlus -= WorldButtonOnOnButtonPlus;
            worldButton.OnButtonMinus -= WorldButtonOnOnButtonMinus;
        }

        private void WorldButtonOnOnButtonPlus() => ControlLim(true);

        private void WorldButtonOnOnButtonMinus() => ControlLim(false);

        public float ControlLim(bool up)
        {
            var lim = limits[_curLim];
            lim.Indicator.SetActive(false);
            
            _curLim = Mathf.Clamp(_curLim + (up ? 1 : -1 ), 0, limits.Length - 1);
            Debug.Log(up + $" {_curLim}");
            
            lim = limits[_curLim];
            lim.Indicator.SetActive(true);
            return lim.Value;
        }
    }

    [Serializable]
    public class Limit
    {
        [SerializeField] private GameObject indicator;
        [SerializeField] private float value;

        public float Value => value;
        public GameObject Indicator => indicator;
    }
}