namespace Survival2D.Systems.Item.Inventory
{
    public class InventorySpace 
    {
        public uint Size { get; private set; }
        public bool IsEmpty { get { return CheckIfEmpty(); } }
        public InventorySlot[] Slots { get; private set; } = null;

        public InventorySpace(uint space_size, InventorySystem inventory)
        {
            Slots = new InventorySlot[space_size];
            for (int i = 0; i < space_size; i++)
            {
                Slots[i] = new InventorySlot(inventory);
            }

            Size = space_size;
        }

        public bool CheckIfContainsSlot(InventorySlot slot)
        {
            for (int i = 0; i < Slots.Length; i++)
            {
                if (slot == Slots[i]) return true;
            }

            return false;
        }

        private bool CheckIfEmpty()
        {
            foreach (var slot in Slots)
            {
                if (slot.ItemContained.CurrentStack > 0)
                    return false;
            }

            return true;
        } 
    }
}