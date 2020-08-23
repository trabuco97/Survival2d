using UnityEngine;
using System.Collections;

using Survival2D.Entities;

using Survival2D.Systems.Statistics;
using System;

namespace Survival2D.Physics.Tools.Grappling
{
    public class GrapplingBehaviour : MonoBehaviour
    {
        [SerializeField] private bool has_custom_values = false;
        [SerializeField] private float custom_impulse_potency = 0f;
        [SerializeField] private Collider2D custom_user_collider = null;
        [SerializeField] private IEntityFacingWrapper custom_facing = null;

        [Header("References")]
        [SerializeField] private Transform grappling_start_point_parent = null;
        [SerializeField] private Transform grappling_start_point = null;
        [SerializeField] private GrapplingHookBehaviour grappling_hook = null;
        [SerializeField] private RetractingBehaviour retract_behaviour = null;

        [Header("Grappling line propierties")]
        [SerializeField] private LineRenderer grappling_line = null;
        [SerializeField] private Transform origin_grappling_line = null;

        private IEntityFacing facing_behaviour = null;
        private Vector2 GrappDirection { get { return facing_behaviour.FacingVector; } }

        public Collider2D GrappUser { private get; set; } = null;
        public GrapplingHookBehaviour Hook { get { return grappling_hook; } }
        public RetractingBehaviour RetractBehaviour { get { return retract_behaviour; } }


        public Stat ImpulsePotency { get; private set; } = new Stat();
        public Stat RetractingVelocity { get; private set; } = new Stat();

        private void Awake()
        {
#if UNITY_EDITOR
            if (grappling_start_point_parent == null)
            {
                Debug.LogWarning($"{nameof(grappling_start_point_parent)} is not assigned to {nameof(GrapplingBehaviour)} of {gameObject.GetFullName()}");
            }
            if (grappling_start_point == null)
            {
                Debug.LogWarning($"{nameof(grappling_start_point)} is not assigned to {nameof(GrapplingBehaviour)} of {gameObject.GetFullName()}");
            }
            if (retract_behaviour == null)
            {
                Debug.LogWarning($"{nameof(retract_behaviour)} is not assigned to {nameof(GrapplingBehaviour)} of {gameObject.GetFullName()}");
            }
            if (grappling_hook == null)
            {
                Debug.LogWarning($"{nameof(grappling_hook)} is not assigned to {nameof(GrapplingBehaviour)} of {gameObject.GetFullName()}");
            }
            if (grappling_line == null)
            {
                Debug.LogWarning($"{nameof(grappling_line)} is not assigned to {nameof(GrapplingBehaviour)} of {gameObject.GetFullName()}");
            }
            if (origin_grappling_line == null)
            {
                Debug.LogWarning($"{nameof(origin_grappling_line)} is not assigned to {nameof(GrapplingBehaviour)} of {gameObject.GetFullName()}");
            }

#endif

            if (has_custom_values)
            {
                ImpulsePotency.ChangeBaseValue(custom_impulse_potency);
                facing_behaviour = custom_facing.Result;
            }

            grappling_hook.gameObject.SetActive(false);
        }

        private void Update()
        {
            if (facing_behaviour != null)
            {
                UpdateGrapplingDirection();
            }

            if (grappling_hook.IsTravelling || grappling_hook.IsFixed)
            {
                UpdateGrapplingLine();
            }
        }


        [ContextMenu("inicialize")]
        public void InicializeGrappling()
        {
            grappling_hook.InicializedHook(custom_user_collider);
        }

        public void InicializeGrappling(Collider2D user_collider, IEntityFacing user_facing)
        {
            grappling_hook.InicializedHook(user_collider);
            facing_behaviour = user_facing;
        }


        [ContextMenu("Propulse")]
        public void ShootGrappling()
        {
            grappling_hook.gameObject.SetActive(true);
            grappling_hook.transform.position = grappling_start_point.position;
            grappling_hook.PropulseHook(ImpulsePotency.Value, GrappDirection);

            grappling_line.positionCount = 2;
        }

        [ContextMenu("Retract")]
        public void RetractGrappling()
        {
            // assuming the behaviour has its values assigned in the inspector
            retract_behaviour.Retract(GrappUser.attachedRigidbody, RetractingVelocity.Value);
        }

        public void EnableSwinging()
        {
            var min_distance = retract_behaviour.MinimumDistance;
            grappling_hook.EnableSwing(min_distance);
        }

        [ContextMenu("Pull")]
        public void PullGrappling()
        {
            grappling_hook.PullHook();
            retract_behaviour.EnableRetracting = false;

            grappling_line.positionCount = 0;
        }


        private void UpdateGrapplingLine()
        {
            grappling_line.SetPosition(0, origin_grappling_line.transform.position);
            grappling_line.SetPosition(1, grappling_hook.EndHook + new Vector3(0, -0.1f, 0));
        }

        private void UpdateGrapplingDirection()
        {
            var angle = Mathf.Atan2(GrappDirection.y, GrappDirection.x) * Mathf.Rad2Deg;
            var vector_rotation = grappling_start_point_parent.rotation.eulerAngles;
            vector_rotation.z = angle;

            grappling_start_point_parent.rotation = Quaternion.Euler(vector_rotation);
        }



#if UNITY_EDITOR
        [Header("Gizmos propierties")]
        [SerializeField] private bool is_gizmos_drawing = false;
        [SerializeField] private Color gizmos_direccion_color = Color.white;
        [SerializeField] private float gizmos_dirrection_distance = 5;

        private void OnDrawGizmos()
        {
            if (!is_gizmos_drawing || facing_behaviour == null) return;

            Gizmos.color = gizmos_direccion_color;
            Gizmos.DrawLine(transform.position, transform.position + (Vector3)GrappDirection * gizmos_dirrection_distance);
        }
#endif


    }
}