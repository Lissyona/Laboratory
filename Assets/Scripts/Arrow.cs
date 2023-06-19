using UnityEngine;

namespace StarterAssets
{
    /// <summary>
    /// Стрелка на приборах
    /// </summary>
    public class Arrow : MonoBehaviour
    {
        [SerializeField] private float minAngle;
        [SerializeField] private float maxAngle;
        [SerializeField] private float minValue, maxValue;
        [SerializeField] private Transform arrow;

        private Quaternion sRot;

        private void Start()
        {
            sRot = arrow.rotation;
        }

        public void SetAngleByValue(float v)
        {
            SetPercentage(Mathf.InverseLerp(minValue, maxValue, v));
        }
        
        public void SetPercentage(float p)
        {
            var angle = Mathf.Lerp(minAngle, maxAngle, p);
            arrow.localRotation = sRot;
            arrow.localRotation *= Quaternion.Euler(0,0, 180 + angle);
        }
    }
}