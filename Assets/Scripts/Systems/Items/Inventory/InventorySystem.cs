using UnityEngine;
using UnityEngine.Events;
using System.Collections.Generic;

namespace Survival2D.Systems.Item.Inventory
{
    public enum StoreResult { Success, Partial, Different }
    public struct StoreResultContext
    {
        public StoreResult result;
        public ItemObject slot_item;
    }

    public enum StoreType { Fill, Force }

    public class InventorySystem : IItemSystem
    {
        private const int INITIAL_PLAYER_SLOTS = 10;

        public InventorySlotEvent onSlotModified { get; } = new InventorySlotEvent();
        public UnityEvent onSpaceModified { get; } = new UnityEvent();
        public Stack<InventorySpace> InventorySpaceContainer { get; private set; } = null;

        public InventorySystem()
        {
            InventorySpaceContainer = new Stack<InventorySpace>();
            AddInventorySpace(INITIAL_PLAYER_SLOTS);
        }

        [ContextMenu("Add 10 space")]
        private void CM_AddSpace() => AddInventorySpace(10);


        public bool AddItemToAvailable(ItemObject item)
        {
            var space_array = InventorySpaceContainer.ToArray();
            for (int i = space_array.Length - 1; i >= 0; i--)
            {
                foreach (var slot in space_array[i].Slots)
                {
                    StoreResultContext storeContext = slot.AddItem(item, StoreType.Fill);
                    if (storeContext.result == StoreResult.Success)
                    {
                        onSlotModified.Invoke(slot);
                        return true;
                    }
                }
            }


            return false;
        }

        public bool AddItemToAvailable(ItemObject item, int slot_number)
        {
            if (TryGetSlot(slot_number, out var slot))
            {
                var store_context = slot.AddItem(item, StoreType.Fill);
                if (store_context.result == StoreResult.Success)
                {
                    onSlotModified.Invoke(slot);
                    return true;
                }
            }

            return false;
        }

        public bool AddItemToSForcedSlot(ItemObject item, int slot_number, out ItemObject last_item_slot)
        {
            if (TryGetSlot(slot_number, out var slot))
            {
                var store_context = slot.AddItem(item, StoreType.Force);
                if (store_context.result == StoreResult.Success)
                {
                    last_item_slot = store_context.slot_item;
                    onSlotModified.Invoke(slot);

                    return true;
                }
            }

            last_item_slot = null;
            return false;
        }

        public void AddInventorySpace(int space_size)
        {
            var space = new InventorySpace(space_size, this);
            InventorySpaceContainer.Push(space);

            onSpaceModified.Invoke();
        }


        public void RemoveInventorySpace()
        {
            // the player always has one space, the initial one
            if (InventorySpaceContainer.Count == 1) return;

            if (InventorySpaceContainer.Peek().IsEmpty)
            {
                onSpaceModified.Invoke();
                InventorySpaceContainer.Pop();
            }
        }

        // TODO
        public void ReorderSlots()
        {

        }

        private bool TryGetSlot(int slot_toAcces, out InventorySlot slot)
        {
            int current_slot = slot_toAcces;
            foreach (var space in InventorySpaceContainer)
            {
                current_slot -= space.Slots.Length;
                if (current_slot < 0)
                {
                    slot = space.Slots[slot_toAcces];
                    return true;
                }
                else
                {
                    slot_toAcces = current_slot;
                }
            }

            slot = null;
            return false;

        }

        public bool IsItemUsedInSystem(ItemObject item_toEvaluate)
        {
            return true;
        }
    }
}