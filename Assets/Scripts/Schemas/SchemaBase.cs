using System;
using UnityEngine;

namespace Schemas
{
    /// <summary>
    /// Схема - набор связей между приборами
    /// </summary>
    public class SchemaBase : MonoBehaviour
    {
        [SerializeField] private SchemaElement[] elements;

        public bool CheckConnectionAvailability(ConnectionNode n1, ConnectionNode n2)
        {
            foreach (var element in elements)
            {
                var isThisConnection = element.IsThisConnection(n1, n2);
                if (isThisConnection) return true;
            }

            return false;
        }
        
        public bool CheckSchema()
        {
            bool allConnected = true;
            foreach (var element in elements)
            {
                if(element.n1 == null || element.n2 == null) continue;
                
                var connectedTo = element.n1.ConnectedTo(element.n2);
                allConnected &= connectedTo;
                element.spline.SetActive(connectedTo);
            }
            
            return allConnected;
        }

        public void Deactivate()
        {
        }

        public void ConnectAllNodes()
        {
            foreach (var element in elements)
            {
                element.n1.Connect(element.n2);
                element.n2.Connect(element.n1);
            }
        }
    }

    [Serializable]
    public class SchemaElement
    {
        public ConnectionNode n1;
        public ConnectionNode n2;
        public GameObject spline;

        public bool IsThisConnection(ConnectionNode node1, ConnectionNode node2)
        {
            if (node1 != n1 && node1 != n2)
                return false;

            if (node2 != n1 && node2 != n2)
                return false;

            return node1 != node2;
        }
    }
}