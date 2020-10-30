using UnityEngine;

using Survival2D.Systems.Statistics;

namespace Survival2D.Physics.Movement.NoSwimmable
{
    public class NoSwimmable_MovementSystem : IMovementSystem
    {
        [Header("References")]
        [SerializeField] private GroundDetection ground_detector = null;

        [Header("No Swim Stats")]
        [SerializeField] private float min_jump_angle = 70f;

        [SerializeField] private bool awake_ground_restricted = true;
        [SerializeField] private bool awake_swim_restricted = true;


        public bool IsGroundRestricted { get; private set; } = true;
        public bool IsSwimRestricted { get; private set; } = true;
        public override bool HasMovementSpecificRestrictions 
        { 
            set
            {
                IsGroundRestricted = value;
                IsSwimRestricted = value;
            }
        }


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
            IsGroundRestricted = awake_ground_restricted;
            IsSwimRestricted = awake_swim_restricted;
        }


        private void Update()
        {
            rgb2.velocity = CheckForVelocityConstraints(rgb2.velocity);
        }

        public void Jump()
        {
            if (!ground_detector.IsGrounded) return;

            var jump_rad = CalculateJumpAngle() * Mathf.Deg2Rad;
            var jump_vector = new Vector2(Mathf.Cos(jump_rad), Mathf.Sin(jump_rad));
            if (IsLeft(rgb2.velocity.x))
            {
                jump_vector.x *= -1;
            }
            jump_vector *= Stats[(int)MovementStats.Jump].Value;
            rgb2.AddForce(jump_vector, ForceMode2D.Impulse);
        }

        public void HorizontalMove(float value)
        {
            var velocity = rgb2.velocity;
            var delta_toAdd = new Vector2(0, 0);
            if (ground_detector.IsGrounded)
            {
                delta_toAdd.x = value * Stats[(int)MovementStats.H_G_Acceleration].Value;
            }
            else
            {
                delta_toAdd.x = value * Stats[(int)MovementStats.H_S_Acceleration].Value;
            }

            velocity += delta_toAdd;
            rgb2.velocity = velocity;
        }

        private Vector2 CheckForVelocityConstraints(Vector2 velocity)
        {
            if (ground_detector.IsGrounded)
            {
                if (IsGroundRestricted)
                {
                    var horizontal_grounded_limit = Stats[(int)MovementStats.H_G_Speed].Value;
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
            }
            else if (IsSwimRestricted)
            {
                var swim_limit = Stats[(int)MovementStats.S_Speed].Value;
                if (velocity.magnitude > swim_limit)
                {
                    var new_velocity_magnitude = Mathf.Lerp(swim_limit, velocity.magnitude, 0.5f);

                    // to not decrease indefinetly
                    if (new_velocity_magnitude - swim_limit <= 0.01) new_velocity_magnitude = swim_limit;

                    var new_velocity = velocity.normalized * new_velocity_magnitude;
                    velocity = new_velocity;
                }
            }

            return velocity;
        }

        private float CalculateJumpAngle()
        {
            // between the jump straight up to the min angle
            var horizontal_velocity = Mathf.Abs(rgb2.velocity.x);

            var value = Mathf.Lerp(90f, min_jump_angle, horizontal_velocity / Stats[(int)MovementStats.H_G_Speed].Value);
            return value;
        }

        private bool IsLeft(float value)
        {
            return value < 0;
        }

    }
}