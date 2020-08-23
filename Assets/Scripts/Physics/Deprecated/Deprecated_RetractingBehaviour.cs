using UnityEngine;
using UnityEngine.Events;

namespace Survival2D.Physics
{
    [RequireComponent(typeof(DistanceJoint2D))]
    public class Deprecated_RetractingBehaviour : MonoBehaviour
    {
        [SerializeField] private float custom_retracting_rate = 0.5f;
        [SerializeField] private Rigidbody2D custom_retracting_body = null;

        [SerializeField] private DistanceJoint2D joint_toGo = null;

        [SerializeField] private float min_distance = 2f;


        private bool enable_retracting = false;


        public Rigidbody2D RetractingBody { private get; set; }
        public DistanceJoint2D JointToGo { private get; set; }
        public UnityEvent onBodyReachDestination { get; } = new UnityEvent();
        public bool EnableRetracting
        {
            set
            {
                if (!value)
                {
                    joint_toGo.connectedBody = null;
                }

                JointToGo.enabled = value;
                enable_retracting = value;
            }
        }
        public float RetractingRate { get; private set; } = 0f;

        private void Awake()
        {
#if UNITY_EDITOR
            if (joint_toGo == null)
            {
                Debug.LogWarning($"{nameof(joint_toGo)} is not assigned to {nameof(Deprecated_RetractingBehaviour)} of {gameObject.GetFullName()}");
            }
#endif
            JointToGo = joint_toGo;


            RetractingBody = custom_retracting_body;
            if (JointToGo != null)
            {
                JointToGo.enabled = false;
            }

            RetractingRate = custom_retracting_rate;
        }


        private void Update()
        {
            if (enable_retracting)
            {
                UpdateRetracting();
            }
        }


        public void Retract()
        {
            JointToGo.connectedBody = RetractingBody;

            JointToGo.anchor = new Vector2();
            JointToGo.connectedAnchor = new Vector2();

            JointToGo.distance = (JointToGo.attachedRigidbody.position - RetractingBody.position).magnitude;

            EnableRetracting = true;
        }


        public void Retract(Rigidbody2D retracting_body, float retracting_rate)
        {
            RetractingBody = retracting_body;
            RetractingRate = retracting_rate;

            Retract();
        }

        private void UpdateRetracting()
        {
            JointToGo.distance -= RetractingRate;
            if (JointToGo.distance < min_distance)
            {
                JointToGo.distance = min_distance;
                EnableRetracting = false;
                onBodyReachDestination.Invoke();
            }
        }

#if UNITY_EDITOR
        [ContextMenu("Enable retract")]
        private void sada()
        {
            Retract();
        }
#endif

    }
}