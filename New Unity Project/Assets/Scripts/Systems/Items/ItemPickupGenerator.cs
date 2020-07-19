using UnityEngine;
using System.Collections;

using Survival2D.Systems.Item.Suit;

namespace Survival2D.Systems.Item
{
    public class ItemPickupGenerator : MonoBehaviour
    {
        [SerializeField] private GameObject item_pickup_prefab = null;
        [SerializeField] private Transform item_spawn = null;

        private void Awake()
        {
            if (item_pickup_prefab == null)
            {
                Debug.LogWarning($"{nameof(item_pickup_prefab)} is not assigned to {nameof(ItemPickupGenerator)} of {name}");
            }
        }

        public void GeneratePickup(ItemType type, int id, uint stack = 1)
        {
            GameObject instance = Instantiate(item_pickup_prefab, item_spawn);
            var pickup = instance.GetComponent<ItemPickup>();

            switch (type)
            {
                case ItemType.Suit:
                    pickup.ItemObject = new SuitObject();
                    break;
                default:
                    pickup.ItemObject = new ItemObject();
                    break;
            }

            pickup.ItemObject.current_stack = stack;
            pickup.ItemObject.Inicialize(type, id);
        }

        // Pre: item_object != null
        public void GeneratePickup(ItemObject item_object)
        {

        }

    }
}