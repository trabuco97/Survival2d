using System;
using UnityEngine;


namespace Survival2D.Systems.Statistics.Status
{
    public class StatusSystemBehaviour : MonoBehaviour, IOrderedBehaviour
    {
        [SerializeField] private SystemStatusHolderBehaviour system_status_holder = null;

        public StatusSystem StatusSystem { get; private set; } = null;

        public int Order => 2;

        private void Awake()
        {
#if UNITY_EDITOR
            if (system_status_holder == null)
            {
                Debug.LogWarning($"{nameof(system_status_holder)} is not assigned to {nameof(StatusSystemBehaviour)} of {name}");
            }
#endif
        }

        private void Update()
        {
            StatusSystem.UpdateStatusDuration();
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

        public void Initialize()
        {
            StatusSystem = new StatusSystem(GetSystem);
        }
    }
}