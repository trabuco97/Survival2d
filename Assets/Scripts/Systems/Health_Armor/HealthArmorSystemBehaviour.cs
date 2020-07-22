using UnityEngine;
using UnityEngine.Events;
using Survival2D.Systems.Statistics.Status;
using Survival2D.Systems.Item.Equipment;

namespace Survival2D.Systems.HealthArmor
{

    public class HealthArmorSystemBehaviour : MonoBehaviour, ISystemWithStatusBehaviour
    {
        // workaround, modifiy later
        [SerializeField] private float base_health = 0f;

        [SerializeField] private EquipmentSystemBehaviour equipment_behaviour = null;
        [SerializeField] private StatusSystemBehaviour status_system_behaviour = null;

        public UnityEvent OnSystemInicialized { get; } = new UnityEvent();

        public HealthArmorSystem HealthSystem { get; private set; } = null;

        public ISystemWithStatus System => HealthSystem;

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
            status_system_behaviour.onSystemInicialized.AddListener(InicializeSystem);
        }

        private void InicializeSystem()
        {
            HealthSystem = new HealthArmorSystem(status_system_behaviour.StatusSystem, equipment_behaviour.Equipment, base_health);
            OnSystemInicialized.Invoke();
        }

    }
}