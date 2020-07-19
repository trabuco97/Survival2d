using UnityEngine;
using System.Collections.Generic;

namespace Survival2D.Systems.Item
{
    [CreateAssetMenu(fileName = "New Item Type Database", menuName = "Custom/ItemDatabase")]
    public class IItemTypeDatabase : ScriptableObject, ISerializationCallbackReceiver
    {
        [SerializeField] private IItemData[] draggable_data = null;
        [SerializeField] private ItemType item_type = ItemType.MAX_TYPES;

        private Dictionary<int, IItemData> item_database = null;

        public ItemType Type { get { return item_type; } }

        public void OnAfterDeserialize()
        {
#if UNITY_EDITOR
            if (!IsMatchingItemData())
            {
                Debug.LogError("database has an item not matching the type");
            }
#endif


            item_database = new Dictionary<int, IItemData>();
            for (int i = 0; i < draggable_data.Length; i++)
            {
                item_database.Add(i, draggable_data[i]);
            }
        }

        public void OnBeforeSerialize()
        {

        }

        public IItemData RetrieveData(int id_itemtype)
        {
            if (item_database.TryGetValue(id_itemtype, out IItemData item_data))
            {
                return item_data;
            }

            return null;
        }


        private bool IsMatchingItemData()
        {
            foreach (var item_data in draggable_data)
            {
                if (item_data.type != item_type) return false;
            }

            return true;
        }
    }
}