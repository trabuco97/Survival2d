using System;
using System.Collections.Generic;
using UnityEngine;

using Survival2D.Systems.Item.Inventory;

namespace Survival2D.UI.Item.Inventory
{
    public class UI_Inventory : MonoBehaviour
    {
        [SerializeField] private CanvasGroup ui_canvas_group = null;
        [SerializeField] private UI_ItemDragImage item_drag_display = null;
        [SerializeField] private GameObject slot_display_prefab = null;
        [SerializeField] private Transform grid_transform = null;

        private InventorySystem current_inventory = null;
        private Dictionary<InventorySlot, UI_InventorySlot> slot_display_database = null;

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

        private void OnDestroy()
        {
            if (current_inventory != null)
            {
                TerminateCallbacks(current_inventory);
            }
        }


        public void InitializeSlots(InventorySystem inventory)
        {
            if (IsInicialized) return;
            if (slot_display_database != null) DestroySlots();
            if (this.current_inventory != null)
            {
                TerminateCallbacks(this.current_inventory);
            }

            this.current_inventory = inventory;
            InitializeCallbacks(inventory);


            slot_display_database = new Dictionary<InventorySlot, UI_InventorySlot>();
            var inventory_spaces_array = inventory.ReorderedInventorySpaceArray;
            int slot_number_count = 0;
            for (int i = 0; i < inventory_spaces_array.Length; i++)
            {
                foreach (var slot in inventory_spaces_array[i].Slots)
                {
                    GameObject slot_display_instance = Instantiate(slot_display_prefab, grid_transform);
                    UI_InventorySlot slot_display = slot_display_instance.GetComponent<UI_InventorySlot>();

                    slot_display_database.Add(slot, slot_display);

                    slot_display.InitializeDisplay(slot);

                    slot_display.Inventory = inventory;
                    slot_display.SlotNumberDisplaying = slot_number_count++;
                    slot_display.ui_canvas_group = ui_canvas_group;
                    slot_display.ItemDragDisplay = item_drag_display;

                }
            }

            IsInicialized = true;
        }



        private void InitializeCallbacks(InventorySystem inventory)
        {
            inventory.OnSlotModified += SlotModifiedCallback;
            inventory.OnSpaceContainerModified += SpaceModifiedCallback;
        }

        private void TerminateCallbacks(InventorySystem previous_inventory)
        {
            previous_inventory.OnSlotModified -= SlotModifiedCallback;
            previous_inventory.OnSpaceContainerModified -= SpaceModifiedCallback;
        }

        private void SlotModifiedCallback(InventoryEventArgs args)
        {
            if (IsInicialized && slot_display_database.TryGetValue(args.SlotModified, out var slot_display))
            {
                slot_display.InitializeDisplay(args.SlotModified);
            }
        }

        private void SpaceModifiedCallback(object e, EventArgs args)
        {
            IsInicialized = false;
            InitializeSlots(current_inventory);
            UpdateAllDisplays();
        }


        private void UpdateAllDisplays()
        {
            foreach (var pair in slot_display_database)
            {
                pair.Value.InitializeDisplay(pair.Key);
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