using System;
using UnityEngine;

using Survival2D.Systems.Statistics;
using Survival2D.Systems.Statistics.Status;

namespace Survival2D.Physics.Movement.NoSwimmable
{
    [Serializable]
    public class NoSwimmable_MovementSystem : IMovementSystem
    {
        [Header("References")]
        [SerializeField] private GroundDetection ground_detector = null;

        [Header("No Swim Stats")]
        [SerializeField] private float horizontal_grounded_speed_value = 0f;
        [SerializeField] private float horizontal_grounded_acceleration_value = 0f;
        [SerializeField] private float swim_speed_value = 0f;
        [SerializeField] private float horizontal_swim_acceleration_value = 0f;
        [SerializeField] private float jump_potency_value = 0f;

        [SerializeField] private bool can_swim_surpass_limits = false;

        [SerializeField] private float min_jump_angle = 70f;

        public NoSwimmable_MovementStats stats;

        // negative value - left
        // positive value - right

        protected override void Awake()
        {
            base.Awake();
#if UNITY_EDITOR
            if (ground_detector == null)
            {
                Debug.LogWarning($"{nameof(ground_detector)} is not assigned to {nameof(NoSwimmable_MovementSystem)} of {gameObject.GetFullName()}");
            }
#endif
            stats = new NoSwimmable_MovementStats();
            stats.horizontal_grounded_speed = new Stat(horizontal_grounded_speed_value);
            stats.horizontal_grounded_acceleration = new Stat(horizontal_grounded_acceleration_value);
            stats.swim_speed = new Stat(swim_speed_value);
            stats.horizontal_swim_acceleration = new Stat(horizontal_swim_acceleration_value);
            stats.jump_potency = new Stat(jump_potency_value);
        }


        private void Update()
        {
            rgb2.velocity = CheckForVelocityConstraints(rgb2.velocity);
        }


        public override StatusLinkageToIncrementalStat LinkIncrementalModifierToStat(IncrementalStatModifierData statModifier_data)
        {
            return null;
        }

        public override StatusLinkageToStat LinkModifierToStat(StatModifierData statModifier_data)
        {
            throw new NotImplementedException();
        }

        public void Jump()
        {
            if (!ground_detector.IsGrounded) return;

            var jump_rad = CalculateJumpAngle() * Mathf.Deg2Rad;
            var jump_vector = new Vector2(Mathf.Cos(jump_rad), Mathf.Sin(jump_rad));
            Debug.Log(jump_vector);
            if (IsLeft(rgb2.velocity.x))
            {
                jump_vector.x *= -1;
            }

            jump_vector *= jump_potency_value;
            rgb2.AddForce(jump_vector, ForceMode2D.Impulse);
        }

        public void HorizontalMove(float value)
        {
            var velocity = rgb2.velocity;
            var delta_toAdd = new Vector2(0, 0);
            if (ground_detector.IsGrounded)
            {
                delta_toAdd.x = value * stats.horizontal_grounded_acceleration.Value;
            }
            else
            {
                delta_toAdd.x = value * stats.horizontal_swim_acceleration.Value;
            }

            velocity += delta_toAdd;
            rgb2.velocity = velocity;
        }

        private Vector2 CheckForVelocityConstraints(Vector2 velocity)
        {
            if (ground_detector.IsGrounded)
            {
                var horizontal_grounded_limit = stats.horizontal_grounded_speed.Value;
                if (Mathf.Abs(velocity.x) > horizontal_grounded_limit)
                {
                    var new_velocity_x = horizontal_grounded_limit;
                    if (IsLeft(velocity.x))
                    {
                        new_velocity_x *= -1;
                    }

                    velocity.x = new_velocity_x;
                }
            }
            else if (!can_swim_surpass_limits)
            {
                var swim_limit = stats.swim_speed.Value;
                if (velocity.magnitude > swim_limit)
                {
                    var new_velocity_magnitude = Mathf.Lerp(swim_limit, velocity.magnitude, 0.5f);

                    // to not decrease indefinetly
                    if (new_velocity_magnitude - swim_limit <= 0.01) new_velocity_magnitude = swim_limit;

                    var new_velocity = velocity.normalized * new_velocity_magnitude;
                    if (IsLeft(velocity.x))
                    {
                        new_velocity.x *= -1;
                    }

                    velocity = new_velocity;
                }
            }

            return velocity;
        }

        private float CalculateJumpAngle()
        {
            // between the jump straight up to the min angle
            var horizontal_velocity = Mathf.Abs(rgb2.velocity.x);

            var value = Mathf.Lerp(90f, min_jump_angle, horizontal_velocity / stats.horizontal_grounded_speed.Value);
            return value;
        }

        private bool IsLeft(float value)
        {
            return value < 0;
        }

    }
}