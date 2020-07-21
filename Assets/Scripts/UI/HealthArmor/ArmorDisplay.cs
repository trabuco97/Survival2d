using UnityEngine;
using UnityEngine.UI;

using TMPro;

using Survival2D.Systems.HealthArmor;

namespace Survival2D.UI.HealthArmor
{
    public class ArmorDisplay : MonoBehaviour
    {
        [SerializeField] private HealthArmorSystem health_armor_system = null;
        [SerializeField] private TMP_Text armor_display = null;

        private void Awake()
        {
            if (health_armor_system == null)
            {
                Debug.LogWarning($"{nameof(health_armor_system)} is not assigned to {nameof(ArmorDisplay)} of {name}");
            }

            if (armor_display == null)
            {
                Debug.LogWarning($"{nameof(armor_display)} is not assigned to {nameof(ArmorDisplay)} of {name}");
            }
        }

        private void Start()
        {
            health_armor_system.onArmorAdquired.AddListener(delegate (ArmorAdquiredEventInfo info)
            {
                SetArmorDisplay(info.armor_value);
            });
        }

        public void SetArmorDisplay(float armor_value)
        {
            armor_display.text = armor_value.ToString().PadLeft(3, '0');
        }
    }
}