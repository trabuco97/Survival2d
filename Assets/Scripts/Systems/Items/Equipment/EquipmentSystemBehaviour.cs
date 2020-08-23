using System;
using UnityEngine;

using Survival2D.Systems.Statistics.Status;

namespace Survival2D.Systems.Item.Equipment
{
    public class EquipmentSystemBehaviour : MonoBehaviour
    {
        [SerializeField] private StatusSystemBehaviour status_behaviour = null;

        public EquipmentSystem Equipment { get; private set; } = null;

        public event EventHandler OnSystemInitialized;

        private void Awake()
        {
#if UNITY_EDITOR
            if (status_behaviour == null)
            {
                Debug.LogWarning($"{nameof(status_behaviour)} is not assigned to {nameof(EquipmentSystemBehaviour)} of {gameObject.GetFullName()}");
            }
#endif
            status_behaviour.OnSystemInitialized += Handler_InitializeSystem;
        }

        private void OnDestroy()
        {
            status_behaviour.OnSystemInitialized -= Handler_InitializeSystem;
        }


        private void Handler_InitializeSystem(object e, EventArgs args)
        {
            Equipment = new EquipmentSystem(status_behaviour.StatusSystem);
            OnSystemInitialized?.Invoke(this, EventArgs.Empty);
        }

    }
}