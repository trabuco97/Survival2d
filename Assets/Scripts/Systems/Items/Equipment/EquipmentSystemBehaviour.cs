using System;
using UnityEngine;

using Survival2D.Systems.Statistics.Status;

namespace Survival2D.Systems.Item.Equipment
{
    public class EquipmentSystemBehaviour : MonoBehaviour, IOrderedBehaviour
    {
        [SerializeField] private StatusSystemBehaviour status_behaviour = null;

        public EquipmentSystem Equipment { get; private set; } = null;

        public int Order => 3;

        private void Awake()
        {
#if UNITY_EDITOR
            if (status_behaviour == null)
            {
                Debug.LogWarning($"{nameof(status_behaviour)} is not assigned to {nameof(EquipmentSystemBehaviour)} of {gameObject.GetFullName()}");
            }
#endif
        }

        public void Initialize()
        {
            Equipment = new EquipmentSystem(status_behaviour.StatusSystem);

        }
    }
}