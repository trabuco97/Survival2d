using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using UnityEngine.EventSystems;

using Survival2D.Systems;
using Survival2D.Systems.Item;
using Survival2D.Systems.Item.Inventory;


namespace Survival2D.UI.Item.Inventory
{


    public class InventorySlotDisplay : IItemSlotDisplay
    {
        [HideInInspector] public InventoryDisplay inventory_display = null;

        [SerializeField] private Text ui_stack = null;

        public int SlotNumberDisplaying { get; set; }

        public override bool CanItemToSystemDisplayed(ItemObject item_sentToSystem)
        {
            return inventory_display.inventory.IsItemUsedInSystem(item_sentToSystem);
        }

        public override ItemObject SendItemToSystem(ItemObject other_slot_item)
        {
            if (inventory_display.inventory.AddItemToSForcedSlot(other_slot_item, SlotNumberDisplaying, out var last_item_slot))
            {
                return last_item_slot;
            }

            return null;
        }

        protected override void Awake()
        {
            base.Awake();

            if (ui_stack == null)
            {
                Debug.LogWarning($"{nameof(ui_stack)} is not assigned to {nameof(InventorySlotDisplay)} of {name}");
            }
        }

        protected override void OnObjectInicialized(ItemObject item_object)
        {
            base.OnObjectInicialized(item_object);
            if (item_object.current_stack > 1)
                ui_stack.text = item_object.current_stack.ToString().PadLeft(2, '0');
            else
                ui_stack.text = string.Empty;
        }

        protected override void OnObjectNonInicialized(ItemObject item_object)
        {
            base.OnObjectNonInicialized(item_object);
            ui_stack.text = string.Empty;
        }
    }
}