﻿using UnityEngine;
using UnityEngine.UI;

using TMPro;

using Survival2D.Systems.HealthArmor;

namespace Survival2D.UI.HealthArmor
{
    public class UI_ArmorRatingStat : MonoBehaviour, UI_IHealthArmorStat
    {
        [SerializeField] private Slider bar_display = null;
        [SerializeField] private TMP_Text rating_display = null;

        private HealthArmorSystem current;

        private void Awake()
        {
#if UNITY_EDITOR
            if (bar_display == null)
            {
                Debug.LogWarning($"{nameof(bar_display)} is not assigned to {nameof(UI_ArmorRatingStat)} of {name}");
            }

            if (rating_display == null)
            {
                Debug.LogWarning($"{nameof(rating_display)} is not assigned to {nameof(UI_ArmorRatingStat)} of {name}");
            }
#endif
        }

        private void OnDestroy()
        {
            if (current != null) TerminateCallbacks(current);
        }


        public void SetArmorRatingDisplay(float actual_rating, float total_rating)
        {
            rating_display.text = actual_rating.ToString().PadLeft(3, '0') + "/" + total_rating.ToString().PadLeft(3, '0');
            bar_display.maxValue = total_rating;
            bar_display.value = actual_rating;
        }

        public void InitializeDisplay(HealthArmorSystem health_system)
        {
            bar_display.minValue = 0;

            if (current != null) TerminateCallbacks(current);
            current = health_system;

            health_system.OnArmorEquipped += CallbackArmorRatingUpdate;
            health_system.OnArmorRatingModified += CallbackArmorRatingUpdate;
        }

        private void TerminateCallbacks(HealthArmorSystem health_system)
        {
            health_system.OnArmorEquipped -= CallbackArmorRatingUpdate;
            health_system.OnArmorRatingModified -= CallbackArmorRatingUpdate;
        }

        private void CallbackArmorRatingUpdate(ArmorEventArgs args)
        {
            SetArmorRatingDisplay(args.ActualArmorRating, args.TotalArmorRating);
        }
    }
}