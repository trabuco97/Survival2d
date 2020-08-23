using System.Collections.Generic;
using UnityEngine;

using Survival2D.Systems.Item;
using Survival2D.Systems.Item.Equipment;

namespace Survival2D.UI.Item.Equipment
{
    public class UI_Equipment : MonoBehaviour
    {
        [SerializeField] private GameObject slot_display_prefab = null;
        [SerializeField] private CanvasGroup ui_canvas_group = null;
        [SerializeField] private UI_ItemDragImage item_drag_display = null;

        public List<UI_EquipmentGroup> equipment_group_container = new List<UI_EquipmentGroup>();
        public bool IsInicialized { get; private set; } = false;


        private void Awake()
        {
#if UNITY_EDITOR
            if (slot_display_prefab == null)
            {
                Debug.LogWarning($"{nameof(slot_display_prefab)} is not assigned to {typeof(UI_Equipment)} of {name}");
            }
            if (ui_canvas_group == null)
            {
                Debug.LogWarning($"{nameof(ui_canvas_group)} is not assigned to {typeof(UI_Equipment)} of {name}");
            }
            if (item_drag_display == null)
            {
                Debug.LogWarning($"{nameof(item_drag_display)} is not assigned to {typeof(UI_Equipment)} of {name}");
            }
#endif
        }

        public void InicializeGroups(EquipmentSystem equipment)
        {
            if (IsInicialized) return;

            foreach (var group_display in equipment_group_container)
            {
                var group_type = equipment.GetGroup(group_display.Type);

                group_display.slot_display_prefab = slot_display_prefab;
                group_display.InicializeSlots(equipment, group_type, ui_canvas_group, item_drag_display);

                EquipmentMethods handler = null;
                handler = (args) =>
                {
                    if (group_display.slot_display_database.TryGetValue(args.Slot, out var slot_display))
                    {
                        slot_display.InitializeDisplay(args.Slot, group_display.ui_equipmentSlot_background_toPlace);
                    }
                };

                if (!equipment.TryAddMethodToEquipType(group_display.Type, handler))
                {
#if UNITY_EDITOR
                    Debug.LogError($"error trying to assign method of type {group_display.Type} in equipment system");
#endif
                }
            }

            IsInicialized = true;
        }
    }
}