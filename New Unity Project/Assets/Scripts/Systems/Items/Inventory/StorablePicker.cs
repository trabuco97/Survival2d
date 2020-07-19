using UnityEngine;
using System.Collections;

namespace Survival2D.Systems.Item.Inventory
{
    public class StorablePicker : MonoBehaviour
    {
        [SerializeField] private InventorySystem inventory_linked = null;

        private void Awake()
        {
            if (inventory_linked == null)
            {
                Debug.LogWarning($"{nameof(inventory_linked)} is not assigned to {nameof(InventorySystem)} of {name}");
            }
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.TryGetComponent(out ItemPickup pickup))
            {
                inventory_linked.AddItemToAvailable(pickup.ItemObject);
                pickup.OnPickup();
            }
        }


    }
}