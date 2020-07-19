using UnityEngine;
using System.Collections;

using Survival2D.UI.Item.Inventory;
using Survival2D.UI.Item.Equipment;

namespace Survival2D.UI.Item
{
    public class UIItemDisplay : MonoBehaviour
    {
        [SerializeField] private Canvas ui_inventory_canvas = null;

        [SerializeField] private InventoryDisplay inventory_display = null;
        [SerializeField] private EquipmentDisplay equipment_display = null;


        public bool IsDisplaying { get { return ui_inventory_canvas.gameObject.activeSelf; } }

        private void Awake()
        {
#if UNITY_EDITOR
            if (ui_inventory_canvas == null)
            {
                Debug.LogWarning($"{nameof(ui_inventory_canvas)} is not assigned to {nameof(UIItemDisplay)} of {name}");
            }
            if (inventory_display == null)
            {
                Debug.LogWarning($"{nameof(inventory_display)} is not assigned to {nameof(UIItemDisplay)} of {name}");
            }
            if (equipment_display == null)
            {
                Debug.LogWarning($"{nameof(equipment_display)} is not assigned to {nameof(UIItemDisplay)} of {name}");
            }
#endif
        }

        public void ToogleDisplay()
        {
            bool new_state = !ui_inventory_canvas.gameObject.activeSelf;
            ui_inventory_canvas.gameObject.SetActive(new_state);

            if (new_state)
            {
                if (!inventory_display.IsInicialized)
                {
                    inventory_display.InicializeSlots();
                }

                if (!equipment_display.IsInicialized)
                {
                    equipment_display.InicializeGroups();
                }
            }
        }
    }
}