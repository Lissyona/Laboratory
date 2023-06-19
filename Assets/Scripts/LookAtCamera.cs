using UnityEngine;

namespace StarterAssets
{
    public class LookAtCamera : MonoBehaviour
    {
        [SerializeField] private Transform cam;
        
        private void Update() => transform.LookAt(cam);
    }
}