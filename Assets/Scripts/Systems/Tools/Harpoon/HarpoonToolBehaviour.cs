using System.Reflection;
using UnityEngine;

using Survival2D.Entities;

using Survival2D.Physics.Tools.Grappling;

using Survival2D.Systems.Item.Harpoon;

using Survival2D.Systems.Statistics.Status;

namespace Survival2D.Systems.Tools.Harpoon
{
    public class HarpoonToolBehaviour : MonoBehaviour, IToolBehaviour
    {
        private enum HarpoonState { None, Thrown, Grounded, Pulled, Swung }

        [Header("References")]
        [SerializeField] private GrapplingBehaviour grapp_behaviour = null; // Maybe change in the future
        [SerializeField] private Collider2D current_user_collider = null;
        [SerializeField] private IEntityFacingWrapper current_user_facing = null;

        private HarpoonState state = HarpoonState.None;

        public bool IsInicialized { get; private set; } = false;
        public SystemStatsCollection Stats => throw new System.NotImplementedException();

        private void Awake()
        {
#if UNITY_EDITOR
            if (grapp_behaviour == null)
            {
                Debug.LogWarning($"{nameof(grapp_behaviour)} is not assigned to {nameof(HarpoonToolBehaviour)} of {gameObject.GetFullName()}");
            }
            if (current_user_collider == null)
            {
                Debug.LogWarning($"{nameof(current_user_collider)} is not assigned to {nameof(HarpoonToolBehaviour)} of {gameObject.GetFullName()}");
            }
            if (current_user_facing == null)
            {
                Debug.LogWarning($"{nameof(current_user_facing)} is not assigned to {nameof(HarpoonToolBehaviour)} of {gameObject.GetFullName()}");
            }
#endif
        }


        public void OnToolDeactivated()
        {
            state = HarpoonState.None;
            grapp_behaviour.PullGrappling();

            grapp_behaviour.gameObject.SetActive(false);
        }

        public void OnToolActivated(IToolItemObject tool_object)
        {
            HarpoonObject harpoon = tool_object as HarpoonObject;
            if (harpoon != null)
            {
                var data = harpoon.HarpoonData;
                grapp_behaviour.ImpulsePotency.ChangeBaseValue(data.base_harpoon_impulse);
                grapp_behaviour.RetractingVelocity.ChangeBaseValue(data.base_harpoon_retract);

            }
#if UNITY_EDITOR
            else
            {
                Debug.LogError("Error gettin harpoon data in HarpoonToolBehaviour");
            }
#endif

            grapp_behaviour.gameObject.SetActive(true);
        }

        public void ExecuteAction(int action_index)
        {
            switch (action_index)
            {
                case 0:
                    if (state == HarpoonState.None)
                    {
                        grapp_behaviour.ShootGrappling();
                        state = HarpoonState.Thrown;
                    }

                    break;
                case 1:
                    if (state == HarpoonState.Grounded)
                    {
                        grapp_behaviour.RetractGrappling();
                        state = HarpoonState.Pulled;
                    }
                    else if (state == HarpoonState.Pulled)
                    {
                        grapp_behaviour.RetractBehaviour.EnableRetracting = false;
                        state = HarpoonState.Grounded;
                    }

                    break;
            }


        }

        public StatusLinkageToStat LinkModifierToStat(StatModifierData statModifier_data)
        {
            return null;
        }

        public StatusLinkageToIncrementalStat LinkIncrementalModifierToStat(IncrementalStatModifierData statModifier_data)
        {
            return null;
        }

        public void InicializeBehaviour()
        {
            grapp_behaviour.InicializeGrappling(current_user_collider, current_user_facing.Result);

            grapp_behaviour.Hook.onHookFixed.AddListener(() => { state = HarpoonState.Grounded; });
            grapp_behaviour.RetractBehaviour.onBodyReachDestination.AddListener(() => 
            { 
                state = HarpoonState.Swung;
                grapp_behaviour.EnableSwinging();
            });

            grapp_behaviour.GrappUser = current_user_collider;

            IsInicialized = true;
        }
    }
}