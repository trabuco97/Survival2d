using UnityEngine;
using UnityEngine.UI;

using TMPro;

using Survival2D.Systems.Statistics.Status;

namespace Survival2D.UI.Status
{
    public class UI_StatusObject : MonoBehaviour
    {
        [SerializeField] private Image status_icon_display = null;
        [SerializeField] private TMP_Text status_time_display = null;

        private bool can_be_updated = false;

        private void Awake()
        {
#if UNITY_EDITOR
            if (status_icon_display == null)
            {
                Debug.LogWarning($"{nameof(status_icon_display)} is not assigned to {typeof(UI_StatusObject)} of {name}");
            }

            if (status_time_display == null)
            {
                Debug.LogWarning($"{nameof(status_time_display)} is not assigned to {typeof(UI_StatusObject)} of {name}");
            }
#endif
        }

        public void Initialize(StatusObject status_object)
        {
            status_icon_display.sprite = status_object.status_data.ui_icon;
            can_be_updated = status_object.status_data.has_duration;

            if (!can_be_updated) status_time_display.text = string.Empty;

            UpdateStatusDisplay(status_object);
        }


        public void UpdateStatusDisplay(StatusObject status_object)
        {
            if (!can_be_updated) return;

            var time_left = status_object.actual_status_duration;
            if (time_left >= 0)
            {
                var seconds_left = time_left % 60;
                status_time_display.text = $"{seconds_left.ToString("0.0")} s";
            }
            else
            {

            }

        }
    }
}