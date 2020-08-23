using UnityEngine;
using UnityEngine.Events;
using Survival2D.Systems.Statistics.Status;
using Survival2D.Systems.Item.Equipment;
using System;

namespace Survival2D.Systems.HealthArmor
{

    public class HealthArmorSystemBehaviour : MonoBehaviour, ISystemWithStatusBehaviour
    {
        // workaround, modifiy later
        [SerializeField] private float base_health = 0f;

        [SerializeField] private EquipmentSystemBehaviour equipment_behaviour = null;
        [SerializeField] private StatusSystemBehaviour status_system_behaviour = null;


        public HealthArmorSystem HealthSystem { get; private set; } = null;

        public ISystemWithStatus System => HealthSystem;

        public SystemType SystemType => SystemType.Health;

        public event EventHandler OnSystemInicialized;

        private void Awake()
        {
#if UNITY_EDITOR
            if (equipment_behaviour == null)
            {
                Debug.LogWarning($"{nameof(equipment_behaviour)} is not assigned to {nameof(HealthArmorSystemBehaviour)} of {name}");
            }

            if (status_system_behaviour == null)
            {
                Debug.LogWarning($"{nameof(status_system_behaviour)} is not assigned to {nameof(HealthArmorSystemBehaviour)} of {name}");
            }
#endif
            equipment_behaviour.OnSystemInitialized += InicializeSystem;
        }

        private void OnDestroy()
        {
            equipment_behaviour.OnSystemInitialized -= InicializeSystem;

        }

        private void InicializeSystem(object e, EventArgs args)
        {
            HealthSystem = new HealthArmorSystem(status_system_behaviour.StatusSystem, equipment_behaviour.Equipment, base_health);
            OnSystemInicialized.Invoke(this, EventArgs.Empty);
        }

    }
}