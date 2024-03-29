﻿using UnityEngine;
using UnityEngine.Events;

namespace Survival2D.Systems.Item
{
    public class ItemPickupBehaviour : MonoBehaviour
    {

        [SerializeField] private BoxCollider2D box_collider = null;
        [SerializeField] private SpriteRenderer pickup_display = null;
        [SerializeField] private UnityEvent onPickupEvent = new UnityEvent();

        public ItemObject ItemObject { get; set; } = null;

        public bool IsMaxStack 
        { 
            get 
            {
                return ItemObject.CurrentStack == ItemObject.ItemData.max_stack; 
            } 
        }

        private void Awake()
        {
#if UNITY_EDITOR
            if (box_collider == null)
            {
                Debug.LogWarning($"{nameof(box_collider)} is not assigned to {nameof(ItemPickupBehaviour)} of {name}");
            }

            if (pickup_display == null)
            {
                Debug.LogWarning($"{nameof(pickup_display)} is not assigned to {nameof(ItemPickupBehaviour)} of {name}");
            }
#endif
        }

        private void Start()
        {
            pickup_display.sprite = ItemObject.ItemData.ui_display;
        }

        public void OnPickup()
        {
            onPickupEvent.Invoke();
            Destroy(this.gameObject);
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (IsMaxStack) return;

            bool is_pickup_found = collision.TryGetComponent(out ItemPickupBehaviour other_pickup);
            if (!is_pickup_found || other_pickup.IsMaxStack) return;

            var other_object = other_pickup.ItemObject;

            if (ItemObject.ItemData.is_stackable && ItemObject.HasSameData(other_object))
            {

                uint new_stack = ItemObject.CurrentStack + other_object.CurrentStack;
                if (new_stack > ItemObject.ItemData.max_stack)
                {
                    other_object.CurrentStack = new_stack - ItemObject.ItemData.max_stack;
                    ItemObject.CurrentStack = ItemObject.ItemData.max_stack;
                }
                else
                {
                    Destroy(collision.gameObject);
                    ItemObject.CurrentStack = new_stack;
                }
            }
        }


        private void GenerateNewPickup(Scriptable_IItemData itemData, int new_stack)
        {

        }


#if UNITY_EDITOR
        [HideInInspector] public bool is_boundaries_shown = false;

        private void OnDrawGizmos()
        {
            if (box_collider == null || !is_boundaries_shown) return;

            Gizmos.color = Color.blue;
            Bounds box_bounds = box_collider.bounds;
            Gizmos.DrawCube(box_bounds.center, box_bounds.size);
        }
#endif
    }
}