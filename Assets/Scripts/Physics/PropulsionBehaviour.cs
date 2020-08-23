using UnityEngine;
using UnityEngine.Events;
using System.Collections;

namespace Survival2D.Physics
{
    public class PropulsionBehaviour : MonoBehaviour
    {
        [SerializeField] private float custom_propulsion_potency = 0f;
        [SerializeField] private Vector2 custom_propulsion_dirrection = Vector2.left;

        [SerializeField] private Rigidbody2D rgb2 = null;

        private float awake_rotation = 0f;

        public UnityEvent onPropulsionEvent { get; } = new UnityEvent();

        private void Awake()
        {
#if UNITY_EDITOR
            if (rgb2 == null)
            {
                Debug.LogWarning($"{nameof(rgb2)} is not assigned to {nameof(PropulsionBehaviour)} of {gameObject.GetFullName()}");
            }
#endif

            awake_rotation = transform.rotation.eulerAngles.z;
        }

        public void IniciatePropulsion(float potency, Vector2 direction, bool has_awake_rotation = true)
        {
            var jump_vector = potency * direction;
            rgb2.AddForce(jump_vector, ForceMode2D.Impulse);

            if (has_awake_rotation)
            {
                var euler_angles = transform.rotation.eulerAngles;
                euler_angles.z = awake_rotation;

                transform.rotation = Quaternion.Euler(euler_angles);
            }

            onPropulsionEvent.Invoke();
        }

        [ContextMenu("propulse")]
        public void IniciatePropulsion()
        {
            IniciatePropulsion(custom_propulsion_potency, custom_propulsion_dirrection);
        }
    }
}