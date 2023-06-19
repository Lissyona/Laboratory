using Instrumentation;
using UnityEngine;

namespace Schemas
{
    /// <summary>
    /// Схема с амперметром и вольтметром в двух конфигурациях
    /// </summary>
    public class Schema_1 : SchemaBase
    {
        [SerializeField] private GameController gameController;
        [SerializeField] private R33ResistanceBox resistanceBox;
        [SerializeField] private M104 amp;
        [SerializeField] private M1106 volt;
        [SerializeField] private SchemaConfig schemaConfig;

        private bool _update = false; 

        private void OnEnable()
        {
            gameController.SchemaIsReady += GameControllerOnSchemaIsReady;
        }

        private void OnDisable()
        {
            gameController.SchemaIsReady -= GameControllerOnSchemaIsReady;
        }

        /// <summary>
        /// Обновление данных на приборах
        /// </summary>
        private void Update()
        {
            if (!_update) return;

            var r = resistanceBox.R;
            amp.Arrow.SetAngleByValue(schemaConfig.GetAmperageValue(r));
            volt.Arrow.SetAngleByValue(schemaConfig.GetVoltageValue(r));
        }

        private void GameControllerOnSchemaIsReady(bool isReady) => _update = isReady;
    }
}