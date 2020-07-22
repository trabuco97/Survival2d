using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Survival2D.Systems.HealthArmor;

namespace Survival2D.UI.HealthArmor
{
    public class UI_HealthSystem : MonoBehaviour
    {
        [SerializeField] private HealthArmorSystemBehaviour health_armor_system_behaviour = null;

        [SerializeField] private UI_HealthStat health_display = null;
        [SerializeField] private UI_ArmorStat armor_display = null;
        [SerializeField] private UI_ArmorRatingStat armorRating_display = null;


        private void Awake()
        {
#if UNITY_EDITOR
            if (health_armor_system_behaviour == null)
            {
                Debug.LogWarning($"{nameof(health_armor_system_behaviour)} is not assigned to {nameof(UI_HealthSystem)} of {name}");
            }
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

            health_armor_system_behaviour.OnSystemInicialized.AddListener(InicializedDisplay);
        }

        private void InicializedDisplay()
        {
            var health_system = health_armor_system_behaviour.HealthSystem;
            health_display.InicializeDisplay(health_system);
            armor_display.InicializeDisplay(health_system);
            armorRating_display.InicializeDisplay(health_system);
        }
    }
}