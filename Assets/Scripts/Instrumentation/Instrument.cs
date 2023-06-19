using System.Linq;
using UnityEngine;

namespace StarterAssets.Instrumentation
{
    /// <summary>
    /// Базовый класс для приборов  
    /// </summary>
    public class Instrument : MonoBehaviour
    {
        [SerializeField] private ConnectionNode[] nodes;
        [SerializeField] private float fallibility;

        public bool IsMyNode(ConnectionNode connectionNode) => nodes.Contains(connectionNode);

        private void Awake()
        {
            foreach (var node in nodes)
            {
                node.SetParent(this);
            }
        }

        /// <summary>
        /// Получить значение value с погрешностью 
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        protected float GetValueWithFallibility(float value)
        {
            return value + value * Random.Range(-fallibility, fallibility);
        }
    }
}