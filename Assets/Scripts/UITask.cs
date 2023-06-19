using System;
using UnityEngine;

namespace StarterAssets
{
    public class UITask : MonoBehaviour
    {
        [SerializeField] private GameObject mark;

        public event Action<int> OnClick;
        public int Index { get; set; }

        private void Awake()
        {
            SetTaskPassState(false);
        }

        public void SetTaskPassState(bool state)
        {
            if(mark == null) return;
            mark.SetActive(state);
        }

        public void OnClickHandler() => OnClick?.Invoke(Index);
    }
}