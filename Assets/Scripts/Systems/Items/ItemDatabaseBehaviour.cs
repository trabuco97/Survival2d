using UnityEngine;
using System.Collections.Generic;

namespace Survival2D.Systems.Item
{
    public class ItemDatabaseBehaviour : MonoBehaviour
    {
        [SerializeField] private IItemTypeDatabase[] draggable_type_databases = null;
        private Dictionary<ItemType, IItemTypeDatabase> item_database = new Dictionary<ItemType, IItemTypeDatabase>();

        public static ItemDatabaseBehaviour Instance;

        private void Awake()
        {
            Instance = this;
            LoadItemData();
        }


        // first, create the iitemtypedatabase classes
        private void LoadItemData()
        {
            foreach (var type_database in draggable_type_databases)
            {
                item_database.Add(type_database.Type, type_database);
            }
        }

        public IItemData GetItemData(ItemType type, int id_itemtype)
        {
            if (item_database.TryGetValue(type, out IItemTypeDatabase database))
            {
                return database.RetrieveData(id_itemtype);
            }

            return null;
        }
    }
}