using System;
using Schemas;
using UnityEngine;

/// <summary>
/// Контроллер состояния игры
/// Проверяет готовность схемы к измерениям
/// </summary>
public class GameController : MonoBehaviour
{
    [SerializeField] private SchemaBase currentSchema;
    [SerializeField] private SchemaBase[] schemas;

    public int ActiveSchema { get; private set; } = -1;

    public event Action<bool> SchemaIsReady;

    public SchemaBase Current => currentSchema;

    private void Start()
    {
        SetupSchema(0);
    }

    public void SetupSchema(int id)
    {
        if (currentSchema != null)
        {
            currentSchema.Deactivate();
            currentSchema.gameObject.SetActive(false);
        }
            
        ActiveSchema = id;
        currentSchema = schemas[ActiveSchema]; 
        currentSchema.gameObject.SetActive(true);
            
        UpdateSchema();
    }

    [ContextMenu("test")]
    public void HackConnect()
    {
        currentSchema.ConnectAllNodes();
        UpdateSchema();
    }

    public void UpdateSchema()
    {
        var schemaIsReady = Current.CheckSchema();
        SchemaIsReady?.Invoke(schemaIsReady);
    }
}