using UnityEngine;
using System.Collections;

using Survival2D.Entities.Player;

using Survival2D.Systems.Item.Inventory;
using Survival2D.Systems.Item.Equipment;

using Survival2D.UI.Item.Inventory;
using Survival2D.UI.Item.Equipment;

namespace Survival2D.UI.Item
{
    public class UI_ItemSystem : IPlayerBehaviourListener_TwoArgs<EquipmentSystemBehaviour, InventorySystemBehaviour>
    {
        [SerializeField] private Canvas ui_inventory_canvas = null;

        [SerializeField] private UI_Inventory inventory_display = null;
        [SerializeField] private UI_Equipment equipment_display = null;


        public bool IsDisplaying { get { return ui_inventory_canvas.gameObject.activeSelf; } }

        protected override void Awake()
        {
            base.Awake();

#if UNITY_EDITOR
            if (ui_inventory_canvas == null)
            {
                Debug.LogWarning($"{nameof(ui_inventory_canvas)} is not assigned to {nameof(UI_ItemSystem)} of {name}");
            }
            if (inventory_display == null)
            {
                Debug.LogWarning($"{nameof(inventory_display)} is not assigned to {nameof(UI_ItemSystem)} of {name}");
            }
            if (equipment_display == null)
            {
                Debug.LogWarning($"{nameof(equipment_display)} is not assigned to {nameof(UI_ItemSystem)} of {name}");
            }
#endif
        }

        private void Start()
        {
            gameObject.SetActive(false);
        }

        public void ToogleDisplay()
        {
            bool new_state = !ui_inventory_canvas.gameObject.activeSelf;
            ui_inventory_canvas.gameObject.SetActive(new_state);

            if (new_state)
            {
                if (!inventory_display.IsInicialized)
                {
                    inventory_display.InitializeSlots(Behaviour2.Inventory);
                }

                if (!equipment_display.IsInicialized)
                {
                    equipment_display.InicializeGroups(Behaviour1.Equipment);
                }
            }
        }

        protected override void InitializeBehaviour()
        {
        }
    }
}