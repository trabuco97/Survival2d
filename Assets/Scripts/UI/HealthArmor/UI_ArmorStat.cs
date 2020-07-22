using UnityEngine;

using TMPro;

using Survival2D.Systems.HealthArmor;

namespace Survival2D.UI.HealthArmor
{
    public class UI_ArmorStat : MonoBehaviour, UI_IHealthArmorStat
    {
        [SerializeField] private TMP_Text armor_display = null;

        private void Awake()
        {
#if UNITY_EDITOR
            if (armor_display == null)
            {
                Debug.LogWarning($"{nameof(armor_display)} is not assigned to {nameof(UI_ArmorStat)} of {name}");
            }
#endif
        }

        public void SetArmorDisplay(float armor_value)
        {
            armor_display.text = armor_value.ToString().PadLeft(3, '0');
        }

        public void InicializeDisplay(HealthArmorSystem health_system)
        {
            health_system.onArmorAdquired.AddListener(delegate (ArmorAdquiredEventInfo info)
            {
                SetArmorDisplay(info.armor_value);
            });
        }
    }
}