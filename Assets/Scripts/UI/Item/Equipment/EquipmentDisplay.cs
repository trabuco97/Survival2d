using System.Collections.Generic;
using UnityEngine;

using Survival2D.Systems.Item;
using Survival2D.Systems.Item.Equipment;

namespace Survival2D.UI.Item.Equipment
{
    public class EquipmentDisplay : MonoBehaviour
    {
        public EquipmentSystem equipment_system = null;

        [SerializeField] private GameObject slot_display_prefab = null;
        [SerializeField] private CanvasGroup ui_canvas_group = null;
        [SerializeField] private ItemDragDisplay item_drag_display = null;

        public List<EquipmentGroupDisplay> equipment_group_container = new List<EquipmentGroupDisplay>();
        public bool IsInicialized { get; private set; } = false;

        public void InicializeGroups()
        {
            foreach (var group_display in equipment_group_container)
            {
                var group_type = equipment_system.GetGroup(group_display.Type);

                group_display.slot_display_prefab = slot_display_prefab;
                group_display.EquipmentDisplay = this;

                group_display.InicializeSlots(group_type, ui_canvas_group, item_drag_display);
            
                if (equipment_system.onEquipableReplacedEvents.TryGetValue(group_display.Type, out var onEvent))
                {
                    onEvent.AddListener(delegate (EquipmentSlot slot_modified, ItemObject last_equipable)
                    {
                        if (group_display.slot_display_database.TryGetValue(slot_modified, out var slot_display))
                        {
                            slot_display.InicializeDisplay(slot_modified, group_display.ui_slot_background);
                        }
                    });
                }
                
            }

            IsInicialized = true;
        }
    }
}