using UnityEngine;
using System.Collections;

namespace Survival2D.Physics
{
    public class GroundDetection : MonoBehaviour
    {
        [SerializeField] private BoxCollider2D box_collider = null;
        [SerializeField] private float delta_ground_detection_value = 0f;
        [SerializeField] private LayerMask ground_layer = 0;

        public bool IsGrounded { get; private set; }

        private void Awake()
        {
#if UNITY_EDITOR
            if (box_collider == null)
            {
                Debug.LogWarning($"{nameof(box_collider)} is not assigned to {nameof(GroundDetection)} of {gameObject.GetFullName()}");
            }
#endif
        }


        private void Update()
        {
            IsGrounded = CheckGroundState();
        }


        private bool CheckGroundState()
        {
            RaycastHit2D hit2D = Physics2D.BoxCast(box_collider.bounds.center, box_collider.bounds.size, 0f, Vector2.down, delta_ground_detection_value, ground_layer);
#if UNITY_EDITOR
            if (IsGrounded)
            {
                state_color = Color.green;
            }
            else
            {
                state_color = Color.red;
            }
#endif

            return hit2D.collider != null;
        }


#if UNITY_EDITOR
        private Color state_color = Color.green;

        private void OnDrawGizmos()
        {
            Gizmos.color = state_color;
            Gizmos.DrawWireCube(box_collider.bounds.center + (Vector3)(Vector2.down * delta_ground_detection_value), box_collider.bounds.size);
        }

#endif
    }
}