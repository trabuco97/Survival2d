using UnityEngine;
using System.Collections;

using Survival2D.Input;

namespace Survival2D.UI.Item
{
    public class UI_InventoryInputBehaviour : MonoBehaviour
    {
        [SerializeField] private UI_ItemSystem ui_inventory_display = null;
        private InputClientManager input_manager = null;

        private void Awake()
        {
#if UNITY_EDITOR
            if (ui_inventory_display == null)
            {
                Debug.LogWarning($"{nameof(ui_inventory_display)} is not assigned to {typeof(UI_InventoryInputBehaviour)} of {gameObject.GetFullName()}");
            }
#endif
        }

        private void Start()
        {
            input_manager = InputClientManager.Instance;
            if (input_manager.IsClientInicialized)
            {
                SetupCallbacks();
            }
        }

        public void PerformToogleInventory()
        {
            ui_inventory_display.ToogleDisplay();
            bool new_action_map_state = !ui_inventory_display.IsDisplaying;
            input_manager.CurrentClient.SetActionMapsState(new_action_map_state, CurrentActionMaps.ConsoleChat);
        }

        private void SetupCallbacks()
        {
            var action_map = input_manager.CurrentClient.UIInput.Inventory;

            action_map.ToogleView.started += var => { PerformToogleInventory(); };
        }
        
    }
}