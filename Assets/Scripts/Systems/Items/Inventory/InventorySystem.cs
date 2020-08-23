using System;
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
        private Stack<InventorySpace> inventory_space_container = null;


        public event InventoryMethods OnSlotModified;
        public event EventHandler OnSpaceContainerModified;

        public InventorySpace[] ReorderedInventorySpaceArray
        {
            get
            {
                var ouptut = new InventorySpace[inventory_space_container.Count];
                int i = ouptut.Length - 1;
                foreach (var space in inventory_space_container)
                {
                    ouptut[i] = space;
                    i--;
                }

                return ouptut;
            }
        }

        public InventorySystem()
        {
            inventory_space_container = new Stack<InventorySpace>();
        }

        public bool AddItemToAvailable(ItemObject item)
        {
            var space_array = inventory_space_container.ToArray();
            for (int i = space_array.Length - 1; i >= 0; i--)
            {
                foreach (var slot in space_array[i].Slots)
                {
                    StoreResultContext storeContext = slot.AddItem(item, StoreType.Fill);
                    if (storeContext.result == StoreResult.Success)
                    {
                        OnSlotModified.Invoke(new InventoryEventArgs(slot));
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
                    OnSlotModified.Invoke(new InventoryEventArgs(slot));
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
                    OnSlotModified.Invoke(new InventoryEventArgs(slot));

                    return true;
                }
            }

            last_item_slot = null;
            return false;
        }

        public void AddInventorySpace(uint space_size)
        {
            var space = new InventorySpace(space_size, this);
            inventory_space_container.Push(space);

            if (OnSpaceContainerModified != null)
            {
                OnSpaceContainerModified.Invoke(this, EventArgs.Empty);
            }
        }


        public void RemoveInventorySpace()
        {
            // the player always has one space, the initial one
            if (inventory_space_container.Count == 1) return;

            if (inventory_space_container.Peek().IsEmpty)
            {
                inventory_space_container.Pop();


                if (OnSpaceContainerModified != null)
                {
                    OnSpaceContainerModified.Invoke(this, EventArgs.Empty);
                }

            }
        }

        // TODO
        public void ReorderSlots()
        {

        }

        public ItemObject[] GetAllItems()
        {
            var item_list = new List<ItemObject>();
            foreach (var space in inventory_space_container)
            {
                foreach (var slot in space.Slots)
                {
                    if (!slot.IsEmpty)
                    {
                        item_list.Add(slot.ItemContained);
                    }
                }
            }

            return item_list.ToArray();
        }

        private bool TryGetSlot(int slot_toAcces, out InventorySlot slot)
        {
            int current_slot = slot_toAcces;
            foreach (var space in inventory_space_container)
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