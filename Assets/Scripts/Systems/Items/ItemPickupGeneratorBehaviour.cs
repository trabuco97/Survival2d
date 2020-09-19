using UnityEngine;
using System.Collections;

using Survival2D.Systems.Item.Suit;

namespace Survival2D.Systems.Item
{
    public class ItemPickupGeneratorBehaviour : MonoBehaviour
    {
        [SerializeField] private GameObject item_pickup_prefab = null;
        [SerializeField] private Transform item_spawn = null;

        private void Awake()
        {
#if UNITY_EDITOR
            if (item_pickup_prefab == null)
            {
                Debug.LogWarning($"{nameof(item_pickup_prefab)} is not assigned to {nameof(ItemPickupGeneratorBehaviour)} of {name}");
            }
#endif
        }

        public void GeneratePickup(ItemType type, int id, uint stack = 1)
        {
            GameObject instance = Instantiate(item_pickup_prefab, item_spawn);
            var pickup = instance.GetComponent<ItemPickupBehaviour>();

            if (ItemGenerator.GenerateItem(type, id, out var item_generated, stack))
            {
                pickup.ItemObject = item_generated;
            }
        }

        // TODO
        // Pre: item_object != null
        public void GeneratePickup(ItemObject item_object)
        {

        }

    }
}