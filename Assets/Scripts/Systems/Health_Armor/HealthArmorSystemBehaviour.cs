using UnityEngine;
using UnityEngine.Events;
using Survival2D.Systems.Statistics.Status;
using Survival2D.Systems.Item.Equipment;
using System;

namespace Survival2D.Systems.HealthArmor
{

    public class HealthArmorSystemBehaviour : MonoBehaviour, ISystemWithStatusBehaviour, IOrderedBehaviour
    {
        // workaround, modifiy later
        [SerializeField] private float base_health = 0f;

        [SerializeField] private EquipmentSystemBehaviour equipment_behaviour = null;
        [SerializeField] private StatusSystemBehaviour status_system_behaviour = null;

        public HealthArmorSystem HealthSystem { get; private set; } = null;

        public ISystemWithStatus System => HealthSystem;

        public SystemType SystemType => SystemType.Health;

        public int Order => 4;

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
        }

        public void Initialize()
        {
            HealthSystem = new HealthArmorSystem(status_system_behaviour.StatusSystem, equipment_behaviour.Equipment);
        }

#if UNITY_EDITOR
        [ContextMenu("Reduce 10 hp")]
        private void Reduce10HP()
        {
            HealthSystem.ModifyHealth(new HealthModificationInfo(Statistics.IncrementalStat.AdditiveTemporaryType.Flat, -10));
        }
#endif
    }
}