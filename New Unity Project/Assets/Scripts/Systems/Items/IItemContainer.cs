using UnityEngine.Events;

namespace Survival2D.Systems.Item
{
    public abstract class IItemContainer
    {
        public ItemObject ItemContained { get; private set; } = null;
        
        public bool IsEmpty { get { return ItemContained == null; } }

        // Pre: ItemContained is empty
        public bool AddItem(ItemObject item_toContain)
        {
            if (item_toContain != null)
            {
                ItemContained = item_toContain;
                return true;
            }

            return false;
        }

        //public bool SwapContainerItems(IItemContainer other)
        //{
        //    var other_object = other.RemoveItem();
        //    var this_object = RemoveItem();

        //    if (other_object == null || this_object == null || (ItemSystemContainerChecker.ItMatches(other_object.type, container_type) &&
        //        ItemSystemContainerChecker.ItMatches(this_object.type, other.container_type)))
        //    {
        //        AddItem(other_object);
        //        other.AddItem(this_object);
        //        return true;
        //    }
        //    else
        //    {
        //        AddItem(this_object);
        //        other.AddItem(other_object);
        //    }
        //    return false;
        //}

        public ItemObject RemoveItem()
        {
            var aux = ItemContained;

            ItemContained = null;
            return aux;
        }
    }
}