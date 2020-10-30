using UnityEngine;
using System.Collections;

using Survival2D.Systems.Statistics.Status;

namespace Survival2D.Physics.Movement
{
    public abstract class IMovementSystem : MonoBehaviour 
    {
        [SerializeField] protected Rigidbody2D rgb2 = null;

        private bool active_state = false;
        public bool ActiveState
        {
            get { return active_state; }
            set
            {
                active_state = value;
                this.enabled = value;
            }
        }

        public SystemStatsCollection Stats { protected get; set; }
        public abstract bool HasMovementSpecificRestrictions { set; }


        protected virtual void Awake()
        {
#if UNITY_EDITOR
            if (rgb2 == null)
            {
                Debug.LogWarning($"{nameof(rgb2)} is not assigned to {nameof(IMovementSystem)} of {gameObject.GetFullName()}");
            }
#endif
        }
    }
}