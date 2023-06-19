using System;
using UnityEngine;

namespace Schemas
{
    /// <summary>
    /// Конфиг схемы с ампреметром и вольтметром
    /// </summary>
    [CreateAssetMenu(fileName = "SchemaConfig", menuName = "ScriptableObject/SchemaConfig", order = 0)]
    public class SchemaConfig : ScriptableObject
    {
        [SerializeField] private Indication[] _indication;
        [SerializeField] private AnimationCurve _voltage;
        [SerializeField] private AnimationCurve _amperage;

        public float GetVoltageValue(float resistance) => 
            _voltage.Evaluate(Mathf.InverseLerp(_indication[0].ResistanceVal, _indication[^1].ResistanceVal, resistance));

        public float GetAmperageValue(float resistance) => 
            _amperage.Evaluate(Mathf.InverseLerp(_indication[0].ResistanceVal, _indication[^1].ResistanceVal, resistance));

        private void OnValidate()
        { 
            ClearAnimationCurve(_voltage);
            ClearAnimationCurve(_amperage);

            foreach (var i in _indication)
            {
                var valueAmperage = i.AmperageVal;
                var valueVoltage = i.VoltageVal;
                
                _amperage.AddKey(
                    Mathf.InverseLerp(
                        _indication[0].ResistanceVal, _indication[^1].ResistanceVal, i.ResistanceVal), valueAmperage);
                _voltage.AddKey(
                    Mathf.InverseLerp(
                        _indication[0].ResistanceVal, _indication[^1].ResistanceVal, i.ResistanceVal), valueVoltage);
            }
        }

        private void ClearAnimationCurve(AnimationCurve animationCurve)
        {
            if(animationCurve == null) return;
            
            for (var index = 0; index < animationCurve.keys.Length; index++)
            {
                animationCurve.RemoveKey(index);
            }
        }
    }
    
    [Serializable]
    public class Indication
    {
        [SerializeField] private float Resistance;
        [SerializeField] private float Amperage;
        [SerializeField] private float LimitAmperage;
        [SerializeField] private float Voltage;
        [SerializeField] private float LimitVoltage;

        public float ResistanceVal => Resistance;

        public float AmperageVal => Amperage;

        public float LimitAmperageVal => LimitAmperage;

        public float VoltageVal => Voltage;

        public float LimitVoltageVal => LimitVoltage;
    }
}