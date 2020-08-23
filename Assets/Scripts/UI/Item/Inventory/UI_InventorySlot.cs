using UnityEngine;
using UnityEngine.UI;

using Survival2D.Systems.Item;
using Survival2D.Systems.Item.Inventory;

using TMPro;

namespace Survival2D.UI.Item.Inventory
{
    public class UI_InventorySlot : UI_IItemSlot_Draggable
    {
        [SerializeField] private TMP_Text ui_stack = null;

        public int SlotNumberDisplaying { get; set; }
        public InventorySystem Inventory { private get; set; }

        public override bool CanItemToSystemDisplayed(ItemObject item_sentToSystem)
        {
            return Inventory.IsItemUsedInSystem(item_sentToSystem);
        }

        public override ItemObject SendItemToSystem(ItemObject other_slot_item)
        {
            if (Inventory.AddItemToSForcedSlot(other_slot_item, SlotNumberDisplaying, out var last_item_slot))
            {
                return last_item_slot;
            }

            return null;
        }

        protected override void Awake()
        {
            base.Awake();
#if UNITY_EDITOR
            if (ui_stack == null)
            {
                Debug.LogWarning($"{nameof(ui_stack)} is not assigned to {nameof(UI_InventorySlot)} of {name}");
            }
#endif
        }

        protected override void OnObjectInicialized(ItemObject item_object)
        {
            base.OnObjectInicialized(item_object);
            if (item_object.CurrentStack > 1)
                ui_stack.text = item_object.CurrentStack.ToString().PadLeft(2, '0');
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