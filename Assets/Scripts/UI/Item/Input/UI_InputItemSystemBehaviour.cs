using UnityEngine;
using System.Collections;

using Survival2D.Input;

namespace Survival2D.UI.Item
{
    public class UI_InputItemSystemBehaviour : IInputClientUser
    {
        [SerializeField] private UI_ItemSystem ui_item_system = null;
        private void Awake()
        {
#if UNITY_EDITOR
            if (ui_item_system == null)
            {
                Debug.LogWarning($"{nameof(ui_item_system)} is not assigned to {typeof(UI_InputItemSystemBehaviour)} of {gameObject.GetFullName()}");
            }
#endif
        }

        protected override void Start()
        {
            base.Start();
        }


        public void PerformToogleInventory()
        {
            ui_item_system.ToogleDisplay();
            bool new_action_map_state = !ui_item_system.IsDisplaying;
            manager.CurrentClient.SetInputTypeState(new_action_map_state, new InputType[] { InputType.UI, InputType.Gameplay }, CurrentActionMaps.Inventory, CurrentActionMaps.No_SwimmableMovement);
        }

        protected override void SetupCallbacks()
        {
            var action_map = manager.CurrentClient.UIInput.Inventory;

            action_map.ToogleView.started += var => { PerformToogleInventory(); };
        }
    }
}