using System.Collections.Generic;

using UnityEngine;
using UnityEngine.UI;

using Survival2D.Systems.Item;
using Survival2D.Systems.Item.Equipment;

namespace Survival2D.UI.Item.Equipment
{
    public class EquipmentGroupDisplay : MonoBehaviour
    {

        public Sprite ui_slot_background = null;

        public ItemType Type = ItemType.MAX_TYPES;
        public EquipmentDisplay EquipmentDisplay { private get; set; }
        public GameObject slot_display_prefab { private get; set; }

        [HideInInspector] public Dictionary<EquipmentSlot, EquipmentSlotDisplay> slot_display_database = null;

        private void Awake()
        {
            if (Type == ItemType.MAX_TYPES)
            {
                Debug.LogError($"Error: ItemType not selected in {name}");
            }
        }

        public void InicializeSlots(EquipmentGroupType group_type, CanvasGroup ui_canvas_group, ItemDragDisplay item_drag_display)
        {
            if (slot_display_database != null) DestroySlotDisplays();
            slot_display_database = new Dictionary<EquipmentSlot, EquipmentSlotDisplay>();

            var slots_array = group_type.GetSlotArray();
            int slot_number = 0;
            for (int i = slots_array.Length - 1; i >= 0; i--)
            {
                var instance = Instantiate(slot_display_prefab, transform);
                var slot_display = instance.GetComponent<EquipmentSlotDisplay>();

                slot_display.ui_canvas_group = ui_canvas_group;
                slot_display.item_drag_display = item_drag_display;

                var slot = slots_array[i];

                slot_display.InicializeDisplay(slot, ui_slot_background);
                slot_display.Slot_Type = Type;
                slot_display.SlotNumberDisplayed = slot_number++;
                slot_display.EquipmentDisplay = this.EquipmentDisplay;
                
                slot_display_database.Add(slot, slot_display);
            }
        }

        // Pre: slot_number ranges [0, group_type.GetSlotArray().Length - 1]
        public void InicializeSlot(EquipmentGroupType group_type, int slot_number)
        {
            var slot = group_type.GetSlot(slot_number);
            var display_array = new EquipmentSlotDisplay[slot_display_database.Values.Count];
            slot_display_database.Values.CopyTo(display_array, 0);

            display_array[slot_number].InicializeDisplay(slot, ui_slot_background);
        }

        private void DestroySlotDisplays()
        {
            foreach (var slot_display in slot_display_database.Values)
            {
                Destroy(slot_display.gameObject);
            }
        }
    }
}