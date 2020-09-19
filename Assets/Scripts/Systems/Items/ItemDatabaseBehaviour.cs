using UnityEngine;
using System.Collections.Generic;

namespace Survival2D.Systems.Item
{
    public class ItemDatabaseBehaviour : MonoBehaviour
    {
        [SerializeField] private Scriptable_ItemTypeDatabase[] draggable_type_databases = null;
        private Dictionary<ItemType, Scriptable_ItemTypeDatabase> item_database = new Dictionary<ItemType, Scriptable_ItemTypeDatabase>();

        public static ItemDatabaseBehaviour Instance { get; private set; } = null;

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

        public Scriptable_IItemData GetItemData(ItemType type, int id_itemtype)
        {
            if (item_database.TryGetValue(type, out Scriptable_ItemTypeDatabase database))
            {
                return database.RetrieveData(id_itemtype);
            }

            return null;
        }
    }
}