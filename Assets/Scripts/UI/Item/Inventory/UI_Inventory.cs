using System.Collections.Generic;
using UnityEngine;

using Survival2D.Systems.Item.Inventory;

namespace Survival2D.UI.Item.Inventory
{
    public class UI_Inventory : MonoBehaviour
    {
        [SerializeField] private CanvasGroup ui_canvas_group = null;
        [SerializeField] private UI_ItemDrag item_drag_display = null;
        [SerializeField] private GameObject slot_display_prefab = null;
        [SerializeField] private Transform grid_transform = null;

        private Dictionary<InventorySlot, UI_InventorySlot> slot_display_database = null;

        private bool are_callbacks_inicialized = false;

        public bool IsInicialized { get; private set; } = false;

        private void Awake()
        {
#if UNITY_EDITOR

            if (ui_canvas_group == null)
            {
                Debug.LogWarning($"{nameof(ui_canvas_group)} is not assigned to {nameof(UI_Inventory)} of {name}");
            }
            if (item_drag_display == null)
            {
                Debug.LogWarning($"{nameof(item_drag_display)} is not assigned to {nameof(UI_Inventory)} of {name}");
            }

            if (slot_display_prefab == null)
            {
                Debug.LogWarning($"{nameof(slot_display_prefab)} is not assigned to {nameof(UI_Inventory)} of {name}");
            }
            if (grid_transform == null)
            {
                Debug.LogWarning($"{nameof(grid_transform)} is not assigned to {nameof(UI_Inventory)} of {name}");
            }
#endif
        }


        public void InicializeSlots(InventorySystem inventory)
        {
            if (IsInicialized) return;

            if (slot_display_database != null) DestroySlots();
            if (!are_callbacks_inicialized) InicializeCallbacks(inventory);

            slot_display_database = new Dictionary<InventorySlot, UI_InventorySlot>();

            var inventory_spaces_array = inventory.InventorySpaceContainer.ToArray();

            int slot_number_count = 0;

            for (int i = inventory_spaces_array.Length - 1; i >= 0; i--)
            {
                foreach (var slot in inventory_spaces_array[i].Slots)
                {

                    GameObject slot_display_instance = Instantiate(slot_display_prefab, grid_transform);
                    UI_InventorySlot slot_display = slot_display_instance.GetComponent<UI_InventorySlot>();

                    slot_display_database.Add(slot, slot_display);

                    slot_display.InicializeDisplay(slot);

                    slot_display.Inventory = inventory;
                    slot_display.SlotNumberDisplaying = slot_number_count++;
                    slot_display.ui_canvas_group = ui_canvas_group;
                    slot_display.item_drag_display = item_drag_display;

                }
            }

            IsInicialized = true;
        }

        private void InicializeCallbacks(InventorySystem inventory)
        {
            inventory.onSlotModified.AddListener(delegate (InventorySlot slot_modified)
            {
                if (IsInicialized && slot_display_database.TryGetValue(slot_modified, out var slot_display))
                {
                    slot_display.InicializeDisplay(slot_modified);
                }
            });

            inventory.onSpaceModified.AddListener(delegate
            {
                IsInicialized = false;
                InicializeSlots(inventory);
                UpdateAllDisplays();
            });
        }

        private void UpdateAllDisplays()
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