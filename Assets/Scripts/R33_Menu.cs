using Instrumentation;
using JetBrains.Annotations;
using StarterAssets;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

public class R33_Menu : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI resistance;
    [SerializeField] private StarterAssetsInputs inputs;
    [SerializeField] private TMP_InputField inputField;
    
    private R33ResistanceBox _resistanceBox;
    private float _curValue;

    private void Start()
    {
        inputs.OnPauseClicked += OnPauseHandler;
        inputField.onValueChanged.AddListener(OnValueChanged);
        Toggle(false);
    }
    
    public void SetupAndShow(R33ResistanceBox resistanceBox)
    {
        _resistanceBox = resistanceBox;
        _curValue = resistanceBox.R;
        UpdateInputFieldValue();
        UpdateCurrentBoxValue();
    }

    private void OnValueChanged(string newValue)
    {
        var floatValue = float.Parse(newValue);
        _curValue = Mathf.Clamp(floatValue, 0, 30_000);
        UpdateInputFieldValue();
    }

    private void UpdateInputFieldValue() => inputField.text = _curValue.ToString("0.0");

    private void UpdateCurrentBoxValue() => resistance.text = _curValue.ToString("0.0");
    
    private void OnPauseHandler()
    {
        Toggle(false);
    }

    public void Toggle(bool toggle)
    {
        inputs.cursorInputForLook = !toggle;
        inputs.inputForMove = !toggle;
        gameObject.SetActive(toggle);

        if (toggle)
        {
            inputField.Select();
            inputField.ActivateInputField();
            EventSystem.current.SetSelectedGameObject(inputField.gameObject);
        }
    }

    [UsedImplicitly]
    public void Apply()
    {
        _resistanceBox.R = _curValue;
        UpdateCurrentBoxValue();
    }
}