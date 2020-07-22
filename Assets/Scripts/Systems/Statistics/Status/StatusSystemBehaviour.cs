using UnityEngine;
using UnityEngine.Events;


namespace Survival2D.Systems.Statistics.Status
{
    public class StatusSystemBehaviour : MonoBehaviour
    {
        [SerializeField] private SystemStatusHolderBehaviour system_status_holder = null;

        public UnityEvent onSystemInicialized { get; } = new UnityEvent();
        public StatusSystem StatusSystem { get; private set; } = null;

        private void Awake()
        {
#if UNITY_EDITOR
            if (system_status_holder == null)
            {
                Debug.LogWarning($"{nameof(system_status_holder)} is not assigned to {nameof(StatusSystemBehaviour)} of {name}");
            }
#endif

            system_status_holder.onSystemStatusInicialized.AddListener(InicializeSystem);
        }

        private void Update()
        {
            StatusSystem.UpdateStatusDuration();
        }

        private void InicializeSystem()
        {
            StatusSystem = new StatusSystem(GetSystem);
            onSystemInicialized.Invoke();
        }

        // Used by the status system
        private ISystemWithStatus GetSystem(SystemType type)
        {
            if (system_status_holder.TryGetSystem(type, out var system))
            {
                return system;
            }
            else
            {
#if UNITY_EDITOR
                Debug.LogError("error trying to get system from SystemStatusHolder");
#endif
                return null;
            }
        }
    }
}