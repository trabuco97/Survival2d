using System;
using UnityEngine;

namespace Survival2D.Systems.Item.Inventory.Hotbar
{
    public class HotbarSystemBehaviour : MonoBehaviour, IOrderedBehaviour
    {
        [SerializeField] private InventorySystemBehaviour inventory_behaviour = null;

        public HotbarSystem Hotbar { get; private set; } = null;

        public int Order => 2;

        public void Initialize()
        {
            var hotbar_space = inventory_behaviour.Inventory.ReorderedInventorySpaceArray[0];
            Hotbar = new HotbarSystem(inventory_behaviour.Inventory, hotbar_space);
        }

        private void Awake()
        {
#if UNITY_EDITOR
            if (inventory_behaviour == null)
            {
                Debug.LogWarning($"{nameof(inventory_behaviour)} is not assigned to {nameof(HotbarSystemBehaviour)} of {gameObject.GetFullName()}");
            }
#endif
        }

    }
}