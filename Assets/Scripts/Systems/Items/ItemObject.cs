using UnityEngine;
using System.Collections;

namespace Survival2D.Systems.Item
{

    [System.Serializable]
    public class ItemObject 
    {
        public ItemType type = ItemType.MAX_TYPES;
        public int id = -1;
        public uint current_stack = 0;

        private IItemData item_data = null;

        public bool IsInicialized { get { return (type != ItemType.MAX_TYPES && id != -1); } }
        public IItemData ItemData 
        {
            get 
            {
                if (item_data == null && IsInicialized)
                {
                    item_data = ItemDatabaseBehaviour.Instance.GetItemData(type, id);
                }

                return item_data;
            }
        }

        public virtual void Inicialize(ItemType type, int id) 
        {
            this.type = type;
            this.id = id;           

            item_data = ItemDatabaseBehaviour.Instance.GetItemData(type, id);
        }

        public bool HasSameData(ItemObject other)
        {
            return type == other.type && id == other.id;
        }
    }
}