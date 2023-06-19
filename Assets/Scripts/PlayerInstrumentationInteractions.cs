using UnityEngine;

namespace StarterAssets
{
    public class PlayerInstrumentationInteractions : MonoBehaviour
    {
        private enum InteractionState
        {
            None,
            TryingToConnect
        }

        [SerializeField] private GameController gameController;
        [SerializeField] private Camera camera;
        [SerializeField] private LayerMask layerMask;
        [SerializeField] private LineRenderer tempWire;
        [SerializeField] private UI ui;

        private StarterAssetsInputs input;
        private InteractionState currentState;
        private bool updateTempWire;
        private float nodeDistance;
        private ConnectionNode firstNode;

        private void Start()
        {
            input = GetComponent<StarterAssetsInputs>();
        }

        public void Update()
        {
            Interactions();
            UpdateWire();
        }

        private void Interactions()
        {
            var ray = camera.ScreenPointToRay(new Vector3(Screen.width / 2, Screen.height / 2));

            if (Physics.Raycast(ray, out var hit, layerMask))
            {
                var go = hit.transform.gameObject;

                if (go.GetComponent<ConnectionNode>() != null)
                {
                    var currentNode = go.GetComponent<ConnectionNode>();
                    currentNode.ShowConnectAbility(true);

                    if (currentState == InteractionState.None && input.interact)
                    {
                        updateTempWire = true;
                        nodeDistance = hit.distance;
                        currentState = InteractionState.TryingToConnect;
                        firstNode = currentNode;
                        tempWire.SetPosition(0, firstNode.transform.position);
                    }
                    else if (currentState == InteractionState.TryingToConnect && input.interact)
                    {
                        if (currentNode != firstNode &&
                            currentNode.Parent != null &&
                            !currentNode.Parent.IsMyNode(firstNode))
                        {
                            currentState = InteractionState.None;
                            TryConnectNodes(firstNode, currentNode);
                        }
                        else
                        {
                            currentState = InteractionState.None;
                        }

                        updateTempWire = false;
                        firstNode = null;
                    }
                    
                    ui.ShowInteractBtn(true, "E");
                }
                else if (currentState != InteractionState.TryingToConnect && go.GetComponent<WorldButton>() != null)
                {
                    var btn = go.GetComponent<WorldButton>();
                    btn.Show(true);
                    
                    if (input.plus)
                    {
                        btn.Plus();
                    }
                    else if (input.minus)
                    {
                        btn.Minus();
                    }
                    else if (input.interact)
                    {
                        btn.Interact();
                    }
                    
                    ui.ShowInteractBtn(true, "+/-/E");
                }
                else
                {
                    ui.ShowInteractBtn(false);
                }
            }

            input.interact = false;
            input.plus = false;
            input.minus = false;
        }

        private void UpdateWire()
        {
            if (!updateTempWire)
            {
                tempWire.gameObject.SetActive(false);
                return;
            }

            tempWire.gameObject.SetActive(true);
            var cameraTransform = camera.transform;
            tempWire.SetPosition(1, cameraTransform.position + cameraTransform.forward * nodeDistance);
        }

        private void TryConnectNodes(ConnectionNode one, ConnectionNode two)
        {
            if (!gameController.Current.CheckConnectionAvailability(one, two))
                return;

            one.Connect(two);
            two.Connect(one);
            gameController.UpdateSchema();
        }
    }
}