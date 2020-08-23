using UnityEngine;
using UnityEngine.Events;
using System.Collections;
using System;

namespace Survival2D.Physics
{
    public class RetractingBehaviour : MonoBehaviour
    {
        [SerializeField] private bool has_custom_values = false;
        [SerializeField] private float custom_retracting_rate = 0.5f;
        [SerializeField] private Rigidbody2D custom_retracting_body = null;

        [Header("References")]
        [SerializeField] private Rigidbody2D body_toGo = null;
        [SerializeField] private bool is_retract_imperative = false;
        [SerializeField] private float min_distance = 2f;


        public Rigidbody2D RetractingBody { private get; set; }
        public UnityEvent onBodyReachDestination { get; } = new UnityEvent();

        public bool EnableRetracting { private get; set; } = false;
        public float RetractingRate { get; private set; } = 0f;
        public float MinimumDistance { get { return min_distance; } }



        private void Awake()
        {
#if UNITY_EDITOR
            if (body_toGo == null)
            {
                Debug.LogWarning($"{nameof(body_toGo)} is not assigned to {nameof(Deprecated_RetractingBehaviour)} of {gameObject.GetFullName()}");
            }
#endif

            if (has_custom_values)
            {
                RetractingBody = custom_retracting_body;
                RetractingRate = custom_retracting_rate;
            }
        }

        private void Update()
        {
            if (EnableRetracting && !CheckRetracting())
            {
                UpdateRetracting();
            }
        }

        public void Retract(Rigidbody2D retracting_body, float retracting_rate)
        {
            RetractingBody = retracting_body;
            RetractingRate = retracting_rate;

            EnableRetracting = true;
        }

        private void UpdateRetracting()
        {
            var raw_vector = (body_toGo.position - RetractingBody.position).normalized;
            var impulse_vector = raw_vector * RetractingRate;

            if (is_retract_imperative)
            {
                RetractingBody.velocity = impulse_vector;
            }
            else
            {

                RetractingBody.AddForce(impulse_vector);
            }
        }

        private bool CheckRetracting()
        {
            var distance = (body_toGo.position - RetractingBody.position).magnitude;

            if (distance <= min_distance)
            {
                EnableRetracting = false;
                onBodyReachDestination.Invoke();

                return true;
            }

            return false;
        }

    }
}