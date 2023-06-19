using System.Collections.Generic;
using StarterAssets.Instrumentation;
using UnityEngine;

public class ConnectionNode : MonoBehaviour
{
    [SerializeField] private GameObject markerFree;
    [SerializeField] private GameObject markerBusy;

    private List<ConnectionNode> connectedNodes = new List<ConnectionNode>();
    private bool showOneMoreFrame;
    private LineRenderer wire;
    
    public Instrument Parent { get; private set; }
    public bool Busy => connectedNodes != null;
    
    private void Start()
    {
        markerBusy.SetActive(false);
        markerFree.SetActive(false);
    }

    public bool ConnectedTo(ConnectionNode node) => node != null && connectedNodes.Count > 0 && connectedNodes.Contains(node);
    
    public void ShowConnectAbility(bool show, ConnectionNode node = null)
    {
        showOneMoreFrame = true;
        markerBusy.SetActive(show && ConnectedTo(node));
        markerFree.SetActive(show && !ConnectedTo(node));
    }

    private void LateUpdate()
    {
        if (showOneMoreFrame)
        {
            showOneMoreFrame = false;
            return;
        }
        ShowConnectAbility(false);
    }

    public void Connect(ConnectionNode node, LineRenderer connectedWire = null)
    {
        if (connectedNodes.Contains(node)) return;

        connectedNodes.Add(node);
        wire = connectedWire;
        OnConnect();
        //connectedNodes.OnConnect();
    }

    public void Disconnect()
    {
        if(connectedNodes == null) return;
        
        /*connectedNodes.OnDisconnect();*/
        OnDisconnect();
        
        connectedNodes = null;
    }

    private void OnConnect()
    {
        
    }

    private void OnDisconnect()
    {
        connectedNodes = new List<ConnectionNode>();
        
        if(wire != null) Destroy(wire.gameObject);
    }

    public void SetParent(Instrument instrument)
    {
        Parent = instrument;
    }
}
