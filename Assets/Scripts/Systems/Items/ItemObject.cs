using UnityEngine;
using System.Collections;

namespace Survival2D.Systems.Item
{

    [System.Serializable]
    public class ItemObject 
    {
        private IItemData item_data = null;

        public bool IsInicialized { get { return (Type != ItemType.MAX_TYPES && ID != -1); } }
        public ItemType Type { get; } = ItemType.MAX_TYPES;
        public int ID { get; } = -1;
        public uint CurrentStack { get; set; } = 0;
        public IItemData ItemData { get { return item_data; } }

        public ItemObject(ItemType type, int id, uint current_stack, bool inicialize_data = true)
        {
            this.Type = type;
            this.ID = id;
            this.CurrentStack = current_stack;

            if (inicialize_data)
                Inicialize(type, id);

        }

        public virtual void Inicialize(ItemType type, int id) 
        {
            item_data = ItemDatabaseBehaviour.Instance.GetItemData(type, id);
        }
        public bool HasSameData(ItemObject other)
        {
            return Type == other.Type && ID == other.ID;
        }

    }
}