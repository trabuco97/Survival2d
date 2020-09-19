using System;
using UnityEngine;

using Survival2D.Entities.Player;
using Survival2D.Systems.HealthArmor;

namespace Survival2D.UI.HealthArmor
{
    public class UI_HealthSystem : IPlayerBehaviourListener<HealthArmorSystemBehaviour>
    {
        [SerializeField] private UI_HealthStat health_display = null;
        [SerializeField] private UI_ArmorStat armor_display = null;
        [SerializeField] private UI_ArmorRatingStat armorRating_display = null;


        protected override void Awake()
        {
            base.Awake();

#if UNITY_EDITOR
            if (health_display == null)
            {
                Debug.LogWarning($"{nameof(health_display)} is not assigned to {nameof(UI_HealthSystem)} of {name}");
            }
            if (armor_display == null)
            {
                Debug.LogWarning($"{nameof(armor_display)} is not assigned to {nameof(UI_HealthSystem)} of {name}");
            }
            if (armorRating_display == null)
            {
                Debug.LogWarning($"{nameof(armorRating_display)} is not assigned to {nameof(UI_HealthSystem)} of {name}");
            }
#endif

        }

        protected override void OnDestroy()
        {
            base.OnDestroy();
        }

        protected override void InitializeBehaviour()
        {
            var health_system = Behaviour.HealthSystem;

            health_display.InitializeDisplay(health_system);
            armor_display.InitializeDisplay(health_system);
            armorRating_display.InitializeDisplay(health_system);
        }
    }
}