using UnityEngine;
using UnityEngine.UI;

using TMPro;

using Survival2D.Systems.HealthArmor;
using Survival2D.Systems.Statistics;

namespace Survival2D.UI.HealthArmor
{
    public class UI_HealthStat : MonoBehaviour, UI_IHealthArmorStat
    {
        [SerializeField] private Slider bar_display = null;
        [SerializeField] private TMP_Text text_display = null;

        private HealthArmorSystem current;

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

        private void OnDestroy()
        {
            if (current != null) TerminateCallbacks(current);
        }

        public void SetHealthDisplay(float actual_health, float total_health)
        {
            bar_display.maxValue = total_health;
            bar_display.value = actual_health;

            text_display.text = actual_health.ToString().PadLeft(3, '0') + "/" + total_health.ToString().PadLeft(3, '0'); 
        }

        public void InitializeDisplay(HealthArmorSystem health_system)
        {
            bar_display.minValue = 0;

            if (current != null) TerminateCallbacks(current);
            current = health_system;

            health_system.OnHealthModified += CallbackHealthModified;
            health_system.OnZeroHealth += CallbackHealthModified;

            var health_stat = health_system.Stats[(int)HealthArmorStats.Health] as IncrementalStat;
            SetHealthDisplay(health_stat.ActualValue, health_stat.Value);
        }

        private void TerminateCallbacks(HealthArmorSystem health_system)
        {
            health_system.OnHealthModified -= CallbackHealthModified;
            health_system.OnZeroHealth -= CallbackHealthModified;
        }

        private void CallbackHealthModified(HealthEventArgs args)
        {
            SetHealthDisplay(args.ActualHealth, args.TotalHealth);
        }

    }
}