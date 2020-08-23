using System;

namespace Survival2D.Systems.Item.Inventory
{

    public delegate void InventoryMethods(InventoryEventArgs args);

    public class InventoryEventArgs : EventArgs
    {
        public InventorySlot SlotModified { get; private set; }
        public InventorySlot LastSlotModified { get; private set; }
        public InventorySpace Space { get; private set; }

        public InventoryEventArgs(InventorySpace space)
        {
            this.Space = space;
        }

        public InventoryEventArgs(InventorySlot slot)
        {
            this.SlotModified = slot;
        }

        public InventoryEventArgs(InventorySlot current_slot, InventorySlot last_slot)
        {
            this.SlotModified = current_slot;
            this.LastSlotModified = last_slot;
        }

    }
}