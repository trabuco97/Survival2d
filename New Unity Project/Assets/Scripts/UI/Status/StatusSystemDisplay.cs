using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Survival2D.Systems.Statistics.Status;

namespace Survival2D.UI.Status
{
    public class StatusSystemDisplay : MonoBehaviour
    {
        [SerializeField] private Transform status_holder_transform = null;
        [SerializeField] private GameObject status_display_prefab = null;

        private List<StatusDisplay> status_display_container = new List<StatusDisplay>();

        private void Awake()
        {
#if UNITY_EDITOR
            if (status_display_prefab == null)
            {
                Debug.LogWarning($"{nameof(status_display_prefab)} is not assigned to {typeof(StatusSystemDisplay)} of {name}");
            }

            if (status_holder_transform == null)
            {
                Debug.LogWarning($"{nameof(status_holder_transform)} is not assigned to {typeof(StatusSystemDisplay)} of {name}");
            }
#endif
        }

        public void AddStatus(EntityStatus status_toAdd)
        {
            var instance = Instantiate(status_display_prefab, status_holder_transform);
            var status_display = instance.GetComponent<StatusDisplay>();

            status_display.Inicialize(this, status_toAdd);
        }


    }
}