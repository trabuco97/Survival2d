using System.Collections.Generic;
using UnityEngine;
using Survival2D.Systems.Item.Inventory;

namespace Survival2D.UI.Item.Inventory
{
    public class InventoryDisplay : MonoBehaviour
    {

        public InventorySystem inventory;


        [SerializeField] private CanvasGroup ui_canvas_group = null;
        [SerializeField] private ItemDragDisplay item_drag_display = null;
        [SerializeField] private GameObject slot_display_prefab = null;
        [SerializeField] private Transform grid_transform = null;

        private Dictionary<InventorySlot, InventorySlotDisplay> slot_display_database = null;

        public bool IsInicialized { get; private set; } = false;

        private void Awake()
        {
            if (ui_canvas_group == null)
            {
                Debug.LogWarning($"{nameof(ui_canvas_group)} is not assigned to {nameof(InventoryDisplay)} of {name}");
            }


            if (item_drag_display == null)
            {
                Debug.LogWarning($"{nameof(item_drag_display)} is not assigned to {nameof(InventoryDisplay)} of {name}");
            }

            if (slot_display_prefab == null)
            {
                Debug.LogWarning($"{nameof(slot_display_prefab)} is not assigned to {nameof(InventoryDisplay)} of {name}");
            }

            inventory.onSlotModified.AddListener(delegate (InventorySlot slot_modified)
            {
                if (IsInicialized && slot_display_database.TryGetValue(slot_modified, out var slot_display))
                {
                    slot_display.InicializeDisplay(slot_modified);
                }
            });

            inventory.onSpaceModified.AddListener(InicializeSlots);
            inventory.onSpaceModified.AddListener(UpdateAllDisplays);

        }


        public void InicializeSlots()
        {
            if (slot_display_database != null) DestroySlots();
            slot_display_database = new Dictionary<InventorySlot, InventorySlotDisplay>();

            var inventory_spaces_array = inventory.InventoryStack.ToArray();

            int slot_number_count = 0;

            for (int i = inventory_spaces_array.Length - 1; i >= 0; i--)
            {
                foreach (var slot in inventory_spaces_array[i].Slots)
                {

                    GameObject slot_display_instance = Instantiate(slot_display_prefab, grid_transform);
                    InventorySlotDisplay slot_display = slot_display_instance.GetComponent<InventorySlotDisplay>();

                    slot_display_database.Add(slot, slot_display);

                    slot_display.InicializeDisplay(slot);

                    slot_display.SlotNumberDisplaying = slot_number_count++;
                    slot_display.inventory_display = this;
                    slot_display.ui_canvas_group = ui_canvas_group;
                    slot_display.item_drag_display = item_drag_display;

                }
            }

            IsInicialized = true;
        }


        public void UpdateAllDisplays()
        {
            foreach (var pair in slot_display_database)
            {
                pair.Value.InicializeDisplay(pair.Key);
            }
        }

        private void DestroySlots()
        {
            var slot_displays = slot_display_database.Values;
            foreach (var slot_display in slot_displays)
            {
                Destroy(slot_display.gameObject);
            }
        }

    }
}