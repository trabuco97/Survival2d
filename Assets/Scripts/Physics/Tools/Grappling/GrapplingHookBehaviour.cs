using UnityEngine;
using UnityEngine.Events;
using System.Collections;

namespace Survival2D.Physics.Tools.Grappling
{
    [RequireComponent(typeof(FixedJoint2D))]
    public class GrapplingHookBehaviour : MonoBehaviour
    {
        [Header("References")]
        [SerializeField] private Transform rope_end = null;
        [SerializeField] private PropulsionBehaviour propulsion = null;
        [SerializeField] private Rigidbody2D rgb2 = null;
        [SerializeField] private FixedJoint2D fixed_joint = null;
        [SerializeField] private Collider2D hook_collider = null;

        [SerializeField] private DistanceJoint2D swing_joint = null;

        private Rigidbody2D user_grappling = null;

        public bool IsTravelling { get; private set; } = false;
        public bool IsFixed { get; private set; } = false;
        public Vector3 EndHook { get { return rope_end.position; } }
        public UnityEvent onHookFixed { get; } = new UnityEvent();

        private void Awake()
        {
#if UNITY_EDITOR
            if (propulsion == null)
            {
                Debug.LogWarning($"{nameof(propulsion)} is not assigned to {nameof(GrapplingHookBehaviour)} of {gameObject.GetFullName()}");
            }
            if (rgb2 == null)
            {
                Debug.LogWarning($"{nameof(rgb2)} is not assigned to {nameof(GrapplingHookBehaviour)} of {gameObject.GetFullName()}");
            }
            if (fixed_joint == null)
            {
                Debug.LogWarning($"{nameof(fixed_joint)} is not assigned to {nameof(GrapplingHookBehaviour)} of {gameObject.GetFullName()}");
            }
            if (hook_collider == null)
            {
                Debug.LogWarning($"{nameof(hook_collider)} is not assigned to {nameof(GrapplingHookBehaviour)} of {gameObject.GetFullName()}");
            }
            if (swing_joint == null)
            {
                Debug.LogWarning($"{nameof(swing_joint)} is not assigned to {nameof(GrapplingHookBehaviour)} of {gameObject.GetFullName()}");
            }
#endif
        }

        private void Update()
        {
            if (IsTravelling)
            {
                UpdateCustomRotation();
            }
        }

        public void InicializedHook(Collider2D user_collider)
        {
            user_grappling = user_collider.attachedRigidbody;

            Physics2D.IgnoreCollision(user_collider, hook_collider);
        }

        public void PropulseHook(float potency, Vector2 direction)
        {
            fixed_joint.enabled = false;
            propulsion.IniciatePropulsion(potency, direction);
            IsTravelling = true;
        }

        public void EnableSwing(float min_distance)
        {
            swing_joint.enabled = true;
            swing_joint.connectedBody = user_grappling;

            swing_joint.anchor = swing_joint.connectedAnchor = new Vector2();
            swing_joint.distance = min_distance;

        }
        
        public void PullHook()
        {
            IsTravelling = false;
            IsFixed = false;

            fixed_joint.connectedBody = null;
            fixed_joint.enabled = false;

            swing_joint.connectedBody = null;
            swing_joint.enabled = false;

            gameObject.SetActive(false);
        }

        private void UpdateCustomRotation()
        {
            // Calculate the rotation based in the velocity direction

            float z_rotation = Mathf.Rad2Deg * Mathf.Atan2(rgb2.velocity.y, rgb2.velocity.x);
            transform.rotation = Quaternion.Euler(0, 0, z_rotation);
        }

        private void OnCollisionEnter2D(Collision2D collision)
        {
            if (IsFixed) return;

            fixed_joint.enabled = true;
            fixed_joint.connectedBody = collision.rigidbody;
            IsTravelling = false;
            IsFixed = true;

            onHookFixed.Invoke();
        }
    }
}