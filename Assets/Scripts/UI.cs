using TMPro;
using UnityEngine;

namespace StarterAssets
{
    public class UI : MonoBehaviour
    {
        [SerializeField] private GameController gameController;
        [SerializeField] private GameObject interactBtn;
        [SerializeField] private TextMeshProUGUI interactText;
        [SerializeField] private UITask[] tasks;
        [SerializeField] private UITask[] schemasMenu;
        [SerializeField] private StarterAssetsInputs inputs;
        [SerializeField] private GameObject schemasUIMenu;

        private void Awake()
        {
            gameController.SchemaIsReady += GameControllerOnSchemaIsReady;
        }

        private void Start()
        {
            inputs.OnPauseClicked += OnPauseHandler;
            int i = 0;
            foreach (var task in schemasMenu)
            {
                task.Index = i;
                task.OnClick += SchemaOnOnClick;
                task.SetTaskPassState(i == gameController.ActiveSchema);
                i++;
            }

            OnPauseHandler();
        }

        private void OnPauseHandler()
        {
            inputs.cursorInputForLook = schemasUIMenu.activeSelf;
            schemasUIMenu.SetActive(!schemasUIMenu.activeSelf);
        }

        private void SchemaOnOnClick(int index)
        {
            if (index == gameController.ActiveSchema) return;

            schemasMenu[gameController.ActiveSchema].SetTaskPassState(false);

            gameController.SetupSchema(index);

            schemasMenu[gameController.ActiveSchema].SetTaskPassState(true);
        }

        private void GameControllerOnSchemaIsReady(bool isReady)
        {
            tasks[0].SetTaskPassState(isReady);
        }

        public void ShowInteractBtn(bool show, string text = null)
        {
            interactBtn.SetActive(show);

            if (!show) return;

            interactText.text = text;
        }

        public void OpenDocumentation()
        {
            Application.OpenURL("");
        }
    }
}