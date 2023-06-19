using System;
using System.Collections.Generic;
using StarterAssets;
using StarterAssets.Instrumentation;
using TMPro;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Instrumentation
{
    public class GOM802 : Instrument
    {
        [Serializable]
        private class Limit
        {
            public float LimR;
            public float RandomMultiplier;
            public ResistanceClass RClass;
        }

        [SerializeField] private R33ResistanceBox _resistanceBox;
        [SerializeField] private TextMeshProUGUI _resistance, _resistanceClass;
        [SerializeField] private List<Limit> _limitConfig;
        [SerializeField] private WorldButton plus, minus;

        private const float RandomPoint = 0.0005f;
        private const float TimeForUpdate = 1.5f;
        private float _timeFromLastUpdate;
        private float _r = float.MinValue;
        private ResistanceClass _currentClass;
        private bool _forceUpdate;

        private void Start()
        {
            plus.OnButtonPressed += () => ChangeResistanceClass(true);
            minus.OnButtonPressed += () => ChangeResistanceClass(false);
            ShowResistanceClass();
        }

        private void ChangeResistanceClass(bool plus)
        {
            var cur = (int) _currentClass;
            cur += plus ? 1 : -1; 
            if(cur is > 2 or < 0) return;
            _currentClass = (ResistanceClass) cur;
            _forceUpdate = true;
            ShowResistanceClass();
        }

        private void ShowResistanceClass()
        {
            _resistanceClass.text = _currentClass.ToString();
        }

        private void Update()
        {
            _timeFromLastUpdate += Time.deltaTime;

            if (!_forceUpdate && Math.Abs(_r - _resistanceBox.R) < 0.001f && _timeFromLastUpdate < TimeForUpdate)
                return;

            _forceUpdate = false;
            _timeFromLastUpdate = 0;

            _r = _resistanceBox.R;
            Limit limit = null;

            for (var i = 0; i < _limitConfig.Count; i++)
            {
                var l = _limitConfig[i];

                if (i == _limitConfig.Count - 1 || _r <= l.LimR)
                {
                    limit = l;
                    break;
                }
            }

            if (limit == null) return;

            var randomSign = Random.Range(0, 1) * 2 - 1;
            var randomizedR = _r + randomSign * _r * RandomPoint +
                              randomSign * Random.Range(1, 9) * limit.RandomMultiplier;

            var format = "00 00 00";
            
            if (_currentClass != limit.RClass)
            {
                _currentClass = limit.RClass;
                ShowResistanceClass();
            }
            
            switch (_currentClass)
            {
                case ResistanceClass.mOm:
                    randomizedR *= 1000;
                    format = "0000";
                    break;
                case ResistanceClass.Om:
                    format = "00.00";
                    break;
                case ResistanceClass.KOm:
                    randomizedR /= 1000;
                    format = "0.0";
                    break;
            }
            
            _resistance.text = randomizedR.ToString(format);
        }
    }
}