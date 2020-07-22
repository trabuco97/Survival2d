namespace Survival2D.Systems.Item.Inventory
{
    public class InventorySlot : IItemContainer
    {
        public InventorySystem inventory = null;

        public InventorySlot(InventorySystem inventory)
        {
            this.inventory = inventory;
        }

        public StoreResultContext AddItem(ItemObject item_toStore, StoreType type)
        {
            StoreResultContext storeResult = new StoreResultContext();
            storeResult.slot_item = ItemContained;

            if (item_toStore == null)
            {
                RemoveItem();
                storeResult.result = StoreResult.Success;
            }
            else
            {
                if (IsEmpty)
                {
                    AddItem(item_toStore);
                    storeResult.result = StoreResult.Success;
                }
                else if (!item_toStore.HasSameData(ItemContained))
                {
                    if (type == StoreType.Fill)
                    {
                        storeResult.result = StoreResult.Different;
                    }
                    else if (type == StoreType.Force)
                    {
                        RemoveItem();
                        AddItem(item_toStore);
                        storeResult.result = StoreResult.Success;
                    }
                }
                else
                {
                    // they are the same type of item

                    var item_data = ItemContained.ItemData;
                    uint total_stack = ItemContained.current_stack + item_toStore.current_stack;

                    if (total_stack > item_data.max_stack)
                    {
                        ItemContained.current_stack = item_data.max_stack;
                        item_toStore.current_stack = total_stack - item_data.max_stack;
                        storeResult.result = StoreResult.Partial;

                    }
                    else
                    {
                        ItemContained.current_stack = total_stack;
                        storeResult.result = StoreResult.Success;
                    }
                }
            }

            return storeResult;
        }
    }
}