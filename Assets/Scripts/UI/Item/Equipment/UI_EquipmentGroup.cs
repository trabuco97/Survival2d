using System.Collections.Generic;

using UnityEngine;
using UnityEngine.UI;

using Survival2D.Systems.Item;
using Survival2D.Systems.Item.Equipment;

namespace Survival2D.UI.Item.Equipment
{
    public class UI_EquipmentGroup : MonoBehaviour
    {

        public Sprite ui_equipmentSlot_background_toPlace = null;

        public ItemType Type = ItemType.MAX_TYPES;
        public GameObject slot_display_prefab { private get; set; }

        public Dictionary<EquipmentSlot, UI_EquipmentSlot> slot_display_database = null;

        private void Awake()
        {
#if UNITY_EDITOR
            if (Type == ItemType.MAX_TYPES)
            {
                Debug.LogError($"Error: ItemType not selected in {name}");
            }
#endif
        }

        public void InicializeSlots(EquipmentSystem equipment, EquipmentGroupType group_type, CanvasGroup ui_canvas_group, UI_ItemDrag item_drag_display)
        {
            if (slot_display_database != null) DestroySlotDisplays();
            slot_display_database = new Dictionary<EquipmentSlot, UI_EquipmentSlot>();

            var slots_array = group_type.GetSlotArray();
            int slot_number = 0;
            for (int i = slots_array.Length - 1; i >= 0; i--)
            {
                var instance = Instantiate(slot_display_prefab, transform);
                var slot_display = instance.GetComponent<UI_EquipmentSlot>();

                slot_display.ui_canvas_group = ui_canvas_group;
                slot_display.item_drag_display = item_drag_display;

                var slot = slots_array[i];

                SetSlotValues(slot_display, equipment, slot, slot_number);

                slot_number++;

                slot_display_database.Add(slot, slot_display);
            }
        }

        // Pre: slot_number ranges [0, group_type.GetSlotArray().Length - 1]
        public void InicializeSlot(EquipmentSystem equipment, EquipmentGroupType group_type, int slot_number)
        {
            var slot = group_type.GetSlot(slot_number);
            var display_array = new UI_EquipmentSlot[slot_display_database.Values.Count];
            slot_display_database.Values.CopyTo(display_array, 0);

            SetSlotValues(display_array[slot_number], equipment, slot, slot_number);
        }

        private void SetSlotValues(UI_EquipmentSlot display, EquipmentSystem equipment, EquipmentSlot slot, int slot_number)
        {
            display.InicializeDisplay(slot, ui_equipmentSlot_background_toPlace);
            display.Slot_Type = Type;
            display.SlotNumberDisplayed = slot_number;
            display.Equipment = equipment;
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