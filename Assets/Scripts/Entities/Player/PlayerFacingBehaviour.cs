using UnityEngine;
using UnityEngine.InputSystem;

using Survival2D.Input;

namespace Survival2D.Entities.Player
{
    public class PlayerFacingBehaviour : MonoBehaviour, IEntityFacing
    {
        [Header("References")]
        [SerializeField] private InputClient input_client = null;

        private Camera main_camera = null;
        private PlayerGameplayInput gameplay_input = null;
        private Vector2 look_position = new Vector2();

        public Vector2 FacingVector => GetFacingVector();

        public bool IsFacing => gameplay_input.LookDirection.enabled;

        private void Start()
        {
            gameplay_input = input_client.GameplayInput;
            main_camera = Camera.main;
        }

        private void Update()
        {
            look_position = gameplay_input.LookDirection.LookPosition.ReadValue<Vector2>();
        }

        private Vector2 GetFacingVector()
        {
            var viewport_position = main_camera.ScreenToViewportPoint(look_position);
            return (viewport_position - new Vector3(0.5f, 0.5f)).normalized;
        }
    }
}