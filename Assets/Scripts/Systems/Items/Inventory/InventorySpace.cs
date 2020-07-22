namespace Survival2D.Systems.Item.Inventory
{
    public class InventorySpace 
    {
        private InventorySlot[] inventory_slots_container = null;

        public bool IsEmpty { get { return CheckIfEmpty(); } }
        public InventorySlot[] Slots { get { return inventory_slots_container; } }

        public InventorySpace(int space_size, InventorySystem inventory)
        {
            inventory_slots_container = new InventorySlot[space_size];
            for (int i = 0; i < space_size; i++)
            {
                inventory_slots_container[i] = new InventorySlot(inventory);
            }
        }

        private bool CheckIfEmpty()
        {
            foreach (var slot in inventory_slots_container)
            {
                if (slot.ItemContained.current_stack > 0)
                    return false;
            }

            return true;
        } 
    }
}