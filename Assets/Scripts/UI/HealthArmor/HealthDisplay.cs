using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

using TMPro;

using Survival2D.Systems.HealthArmor;

namespace Survival2D.UI.HealthArmor
{
    public class HealthDisplay : MonoBehaviour
    {
        [SerializeField] private HealthArmorSystem health_armor_system = null;
        [SerializeField] private Slider bar_display = null;
        [SerializeField] private TMP_Text text_display = null;


        private void Awake()
        {
            if (health_armor_system == null)
            {
                Debug.LogWarning($"{nameof(health_armor_system)} is not assigned to {nameof(HealthDisplay)} of {name}");
            }

            if (bar_display == null)
            {
                Debug.LogWarning($"{nameof(bar_display)} is not assigned to {nameof(HealthDisplay)} of {name}");
            }

            if (text_display == null)
            {
                Debug.LogWarning($"{nameof(text_display)} is not assigned to {nameof(HealthDisplay)} of {name}");
            }
        }

        private void Start()
        {
            bar_display.minValue = 0;

            health_armor_system.onHealthModified.AddListener(SetHealthDisplay);
            health_armor_system.onZeroHealth.AddListener(delegate (float total_health, GameObject dead_entity)
            {
                SetHealthDisplay(0, total_health);
            });


            SetHealthDisplay(health_armor_system.Health.ActualValue, health_armor_system.Health.Value);
        }


        public void SetHealthDisplay(float actual_health, float total_health)
        {
            bar_display.maxValue = total_health;
            bar_display.value = actual_health;

            text_display.text = actual_health.ToString().PadLeft(3, '0') + "/" + total_health.ToString().PadLeft(3, '0'); 
        }

    }
}