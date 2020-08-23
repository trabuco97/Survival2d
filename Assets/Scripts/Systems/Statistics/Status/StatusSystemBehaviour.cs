using System;
using UnityEngine;


namespace Survival2D.Systems.Statistics.Status
{
    public class StatusSystemBehaviour : MonoBehaviour
    {
        [SerializeField] private SystemStatusHolderBehaviour system_status_holder = null;

        public StatusSystem StatusSystem { get; private set; } = null;

        public event EventHandler OnSystemInitialized;


        private void Awake()
        {
#if UNITY_EDITOR
            if (system_status_holder == null)
            {
                Debug.LogWarning($"{nameof(system_status_holder)} is not assigned to {nameof(StatusSystemBehaviour)} of {name}");
            }
#endif

            // Inicialize the system
            system_status_holder.OnSystemStatusInicialized += InicializeSystem;
        }

        private void Update()
        {
            StatusSystem.UpdateStatusDuration();
        }

        private void OnDestroy()
        {
            system_status_holder.OnSystemStatusInicialized -= InicializeSystem;
        }

        private void InicializeSystem(object e, EventArgs args)
        {
            StatusSystem = new StatusSystem(GetSystem);
            OnSystemInitialized.Invoke(this, EventArgs.Empty);
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