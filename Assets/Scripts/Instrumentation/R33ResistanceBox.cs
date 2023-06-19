using StarterAssets;
using StarterAssets.Instrumentation;
using UnityEngine;

//Магазин сопротивлений
namespace Instrumentation
{
    public class R33ResistanceBox : Instrument
    {
        [Range(0, 100000)] public float R;
        [SerializeField] private R33_Menu _r33Menu;
        [SerializeField] private WorldButton _worldButton;

        private void Start()
        {
            _worldButton.OnButtonPressed += WorldButtonOnButtonPressed;
        }

        private void WorldButtonOnButtonPressed()
        {
            _r33Menu.SetupAndShow(this);
            _r33Menu.Toggle(true);
        }
    }
}
