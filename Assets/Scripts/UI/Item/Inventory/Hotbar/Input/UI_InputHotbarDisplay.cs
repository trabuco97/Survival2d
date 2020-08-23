using UnityEngine;
using UnityEngine.InputSystem;

using Survival2D.Input;

namespace Survival2D.UI.Item.Inventory.Hotbar
{
    public class UI_InputHotbarDisplay : IInputClientUser
    {
        [SerializeField] private UI_Hotbar hotbar_display = null;

        private void Awake()
        {
#if UNITY_EDITOR
            if (hotbar_display == null)
            {
                Debug.LogWarning($"{nameof(hotbar_display)} is not assigned to {nameof(UI_InputHotbarDisplay)} of {gameObject.GetFullName()}");
            }
#endif
        }

        public void PerformChangeHotbarSlot(uint index)
        {
            hotbar_display.MoveSelected(index);
        }

        protected override void SetupCallbacks()
        {
            var action_map = manager.CurrentClient.GameplayInput.Hotbar;

            action_map.Scroll.performed += (ctx) => { PerformScrollHotbar(ctx.ReadValue<Vector2>().y); };
            action_map._1.performed += (ctx) => { PerformChangeHotbarSlot(0); };
            action_map._2.performed += (ctx) => { PerformChangeHotbarSlot(1); };
            action_map._3.performed += (ctx) => { PerformChangeHotbarSlot(2); };
            action_map._4.performed += (ctx) => { PerformChangeHotbarSlot(3); };
            action_map._5.performed += (ctx) => { PerformChangeHotbarSlot(4); };
            action_map._6.performed += (ctx) => { PerformChangeHotbarSlot(5); };
            action_map._7.performed += (ctx) => { PerformChangeHotbarSlot(6); };
            action_map._8.performed += (ctx) => { PerformChangeHotbarSlot(7); };
            action_map._9.performed += (ctx) => { PerformChangeHotbarSlot(8); };
            action_map._0.performed += (ctx) => { PerformChangeHotbarSlot(9); };
        }

        private void PerformScrollHotbar(float scroll_value)
        {
            if (scroll_value > 0)
            {
                hotbar_display.MoveLeftSelected();
            }
            else
            {
                hotbar_display.MoveRightSelected();
            }
        }
    }
}