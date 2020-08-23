using UnityEngine;

using TMPro;

using Survival2D.Systems.HealthArmor;

namespace Survival2D.UI.HealthArmor
{
    public class UI_ArmorStat : MonoBehaviour, UI_IHealthArmorStat
    {
        [SerializeField] private TMP_Text armor_display = null;

        private HealthArmorSystem current;

        private void Awake()
        {
#if UNITY_EDITOR
            if (armor_display == null)
            {
                Debug.LogWarning($"{nameof(armor_display)} is not assigned to {nameof(UI_ArmorStat)} of {name}");
            }
#endif
        }

        private void OnDestroy()
        {
            if (current != null) TerminateCallbacks(current);
        }

        public void SetArmorDisplay(float armor_value)
        {
            armor_display.text = armor_value.ToString().PadLeft(3, '0');
        }

        public void InitializeDisplay(HealthArmorSystem health_system)
        {
            if (current != null) TerminateCallbacks(current);

            this.current = health_system;
            health_system.OnArmorEquipped += CallbackArmorEquipped;
        }

        private void TerminateCallbacks(HealthArmorSystem health_system)
        {
            health_system.OnArmorEquipped -= CallbackArmorEquipped;
        }

        private void CallbackArmorEquipped(ArmorEventArgs args)
        {
            SetArmorDisplay(args.ArmorValue);
        }
    }
}