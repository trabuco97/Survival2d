using UnityEngine;
using UnityEngine.UI;

using TMPro;

using Survival2D.Systems.HealthArmor;

namespace Survival2D.UI.HealthArmor
{
    public class ArmorRatingDisplay : MonoBehaviour
    {
        [SerializeField] private HealthArmorSystem health_armor_system = null;
        [SerializeField] private Slider bar_display = null;
        [SerializeField] private TMP_Text rating_display = null;

        private void Awake()
        {
            if (health_armor_system == null)
            {
                Debug.LogWarning($"{nameof(health_armor_system)} is not assigned to {nameof(ArmorRatingDisplay)} of {name}");
            }

            if (bar_display == null)
            {
                Debug.LogWarning($"{nameof(bar_display)} is not assigned to {nameof(ArmorRatingDisplay)} of {name}");
            }

            if (rating_display == null)
            {
                Debug.LogWarning($"{nameof(rating_display)} is not assigned to {nameof(ArmorRatingDisplay)} of {name}");
            }
        }

        private void Start()
        {
            bar_display.minValue = 0;

            health_armor_system.onArmorAdquired.AddListener(delegate (ArmorAdquiredEventInfo info) 
            {
                SetArmorRatingDisplay(info.armor_rating_temp_value, info.armot_rating_total_value);
            });

            health_armor_system.onLossArmor.AddListener(SetArmorRatingDisplay);
        }

        public void SetArmorRatingDisplay(float actual_rating, float total_rating)
        {
            rating_display.text = actual_rating.ToString().PadLeft(3, '0') + "/" + total_rating.ToString().PadLeft(3, '0');
            bar_display.maxValue = total_rating;
            bar_display.value = actual_rating;
        }
    }
}