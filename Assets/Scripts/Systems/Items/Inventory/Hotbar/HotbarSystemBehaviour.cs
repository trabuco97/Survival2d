using System;
using UnityEngine;

namespace Survival2D.Systems.Item.Inventory.Hotbar
{
    public class HotbarSystemBehaviour : MonoBehaviour
    {
        [SerializeField] private InventorySystemBehaviour inventory_behaviour = null;

        public HotbarSystem Hotbar { get; private set; } = null;

        public event EventHandler OnHotbarInicialized;

        private void Awake()
        {
#if UNITY_EDITOR
            if (inventory_behaviour == null)
            {
                Debug.LogWarning($"{nameof(inventory_behaviour)} is not assigned to {nameof(HotbarSystemBehaviour)} of {gameObject.GetFullName()}");
            }
#endif
        }

        private void Start()
        {
            var hotbar_space = inventory_behaviour.Inventory.ReorderedInventorySpaceArray[0];
            Hotbar = new HotbarSystem(inventory_behaviour.Inventory, hotbar_space);
            OnHotbarInicialized.Invoke(this, EventArgs.Empty);
        }

    }
}