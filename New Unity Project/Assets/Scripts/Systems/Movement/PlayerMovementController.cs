using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

using Survival2D.Input;

namespace Survival2D
{
    public class PlayerMovementController : MonoBehaviour
    {
        [SerializeField] private Rigidbody2D rgb2 = null;
        [SerializeField] private float jump_potency = 100;
        [SerializeField] private float Horizontal_velocity = 10;

        [SerializeField] private LayerMask ground_layer;
        [SerializeField] private PlayerGameplayInput player_input;

        private bool is_grounded = true;
        private bool is_moving = false;

        private void Awake()
        {
            if (rgb2 == null)
            {
                Debug.LogWarning($"{nameof(rgb2)} is not assigned to {nameof(PlayerMovementController)} of {name}");
            }

            player_input = new PlayerGameplayInput();

            player_input.Movement.Move.performed += ctx => { is_moving = true; };
            player_input.Movement.Move.canceled += ctx => { is_moving = false; };
            player_input.Movement.Jump.started += OnJump;


        }

        public void OnMove()
        {
            float horizontal_direccion = player_input.Movement.Move.ReadValue<float>();
            rgb2.velocity = new Vector2(Horizontal_velocity * horizontal_direccion, rgb2.velocity.y);
        }

        public void OnJump(InputAction.CallbackContext context)
        {
            if (is_grounded)
            {
                rgb2.velocity = new Vector2(rgb2.velocity.x, jump_potency);
            }
        }

        private void Update()
        {
            if (is_moving)
            {
                OnMove();
            }
        }

        private void OnEnable()
        {

            player_input.Enable();
        }

        private void OnCollisionEnter2D(Collision2D collision)
        {
            if (1 << collision.gameObject.layer == ground_layer.value)
            {
                is_grounded = true;
            }
        }

        private void OnCollisionExit2D(Collision2D collision)
        {
            if (1 << collision.gameObject.layer == ground_layer.value)
            {
                is_grounded = false;
            }
        }

    }
}