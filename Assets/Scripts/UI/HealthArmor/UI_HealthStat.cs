using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

using TMPro;

using Survival2D.Systems.HealthArmor;

namespace Survival2D.UI.HealthArmor
{
    public class UI_HealthStat : MonoBehaviour, UI_IHealthArmorStat
    {
        [SerializeField] private Slider bar_display = null;
        [SerializeField] private TMP_Text text_display = null;

        private void Awake()
        {
#if UNITY_EDITOR
            if (bar_display == null)
            {
                Debug.LogWarning($"{nameof(bar_display)} is not assigned to {nameof(UI_HealthStat)} of {name}");
            }

            if (text_display == null)
            {
                Debug.LogWarning($"{nameof(text_display)} is not assigned to {nameof(UI_HealthStat)} of {name}");
            }
#endif
        }


        public void SetHealthDisplay(float actual_health, float total_health)
        {
            bar_display.maxValue = total_health;
            bar_display.value = actual_health;

            text_display.text = actual_health.ToString().PadLeft(3, '0') + "/" + total_health.ToString().PadLeft(3, '0'); 
        }

        public void InicializeDisplay(HealthArmorSystem health_system)
        {
            bar_display.minValue = 0;

            health_system.onHealthModified.AddListener(SetHealthDisplay);
            health_system.onZeroHealth.AddListener(delegate (float total_health)
            {
                SetHealthDisplay(0, total_health);
            });


            SetHealthDisplay(health_system.Health.ActualValue, health_system.Health.Value);
        }
    }
}