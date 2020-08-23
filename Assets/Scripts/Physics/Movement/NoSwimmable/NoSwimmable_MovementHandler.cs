using UnityEngine;
using UnityEngine.InputSystem;

using Survival2D.Input;

namespace Survival2D.Physics.Movement.NoSwimmable
{
    public class NoSwimmable_MovementHandler : IMovementTypeHandlerWithInput
    {
        [SerializeField] private NoSwimmable_MovementSystem movement_system = null;

        private InputAction horizontal_action = null;
        private bool is_horizontal_move = false;

        private void Awake()
        {
#if UNITY_EDITOR
            if (movement_system == null)
            {
                Debug.LogWarning($"{nameof(movement_system)} is not assigned to {nameof(NoSwimmable_MovementHandler)} of {gameObject.GetFullName()}");
            }
#endif
        }


        private void Update()
        {
            if (is_horizontal_move)
            {
                movement_system.HorizontalMove(horizontal_action.ReadValue<float>());
            }
        }


        private void PerformJump(InputAction.CallbackContext ctx)
        {
            movement_system.Jump();
        }

        private void PerformHorizontalMovement(InputAction.CallbackContext ctx)
        {
            movement_system.HorizontalMove(ctx.ReadValue<float>());
        }

        protected override void SetupCallbacks()
        {
            var action_map = manager.CurrentClient.GameplayInput.NoSwimmable_Movement;

            horizontal_action = action_map.Horizontal_Movement;

            action_map.Jump.performed += PerformJump;
            action_map.Horizontal_Movement.started += delegate { is_horizontal_move = true; };
            action_map.Horizontal_Movement.canceled += delegate { is_horizontal_move = false; };
        }
    }
}