using UnityEngine;
using UnityEngine.UI;

using TMPro;

using Survival2D.Systems.Statistics.Status;

namespace Survival2D.UI.Status
{
    public class StatusDisplay : MonoBehaviour
    {
        [SerializeField] private Image status_icon_display = null;
        [SerializeField] private TMP_Text status_time_display = null;

        private StatusSystemDisplay status_system_display = null;
        private EntityStatus status_displaying = null;


        private void Awake()
        {
#if UNITY_EDITOR
            if (status_icon_display == null)
            {
                Debug.LogWarning($"{nameof(status_icon_display)} is not assigned to {typeof(StatusDisplay)} of {name}");
            }

            if (status_time_display == null)
            {
                Debug.LogWarning($"{nameof(status_time_display)} is not assigned to {typeof(StatusDisplay)} of {name}");
            }
#endif
        }

        private void Update()
        {
            UpdateStatusDisplay();
        }

        public void Inicialize(StatusSystemDisplay status_system_display, EntityStatus status)
        {
            this.status_system_display = status_system_display;
            this.status_displaying = status;

            status_icon_display.sprite = status.status_data.ui_icon;
            UpdateStatusDisplay();
        }


        private void UpdateStatusDisplay()
        {
            var time_left = status_displaying.actual_status_duration;
            if (time_left >= 0)
            {
                var seconds_left = time_left % 60;
                status_time_display.text = $"{seconds_left} s";
            }
            else
            {

            }

        }
    }
}