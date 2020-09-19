using UnityEngine;
using System.Collections;

namespace Survival2D.Systems.Item.Inventory
{
    public class ItemPickupGetterBehaviour : MonoBehaviour
    {
        [SerializeField] private InventorySystemBehaviour inventory_linked = null;

        private void Awake()
        {
#if UNITY_EDITOR
            if (inventory_linked == null)
            {
                Debug.LogWarning($"{nameof(inventory_linked)} is not assigned to {nameof(InventorySystem)} of {gameObject.GetFullName()}");
            }
#endif
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.TryGetComponent(out ItemPickupBehaviour pickup))
            {
                inventory_linked.Inventory.AddItemToAvailable(pickup.ItemObject);
                pickup.OnPickup();
            }
        }


    }
}