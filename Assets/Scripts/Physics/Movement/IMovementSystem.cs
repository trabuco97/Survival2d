using UnityEngine;
using System.Collections;

using Survival2D.Systems.Statistics.Status;

namespace Survival2D.Physics.Movement
{
    public abstract class IMovementSystem : MonoBehaviour, ISystemWithStatus 
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


        protected virtual void Awake()
        {
#if UNITY_EDITOR
            if (rgb2 == null)
            {
                Debug.LogWarning($"{nameof(rgb2)} is not assigned to {nameof(IMovementSystem)} of {gameObject.GetFullName()}");
            }
#endif
        }

        public abstract StatusLinkageToIncrementalStat LinkIncrementalModifierToStat(IncrementalStatModifierData statModifier_data);
        public abstract StatusLinkageToStat LinkModifierToStat(StatModifierData statModifier_data);
    }
}