using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Survival2D.Systems.Statistics.Status;

namespace Survival2D.UI.Status
{
    public class UI_StatusSystem : MonoBehaviour
    {
        [SerializeField] private Transform status_holder_transform = null;
        [SerializeField] private GameObject status_display_prefab = null;

        private List<UI_StatusObject> status_display_container = new List<UI_StatusObject>();

        private void Awake()
        {
#if UNITY_EDITOR
            if (status_display_prefab == null)
            {
                Debug.LogWarning($"{nameof(status_display_prefab)} is not assigned to {typeof(UI_StatusSystem)} of {name}");
            }

            if (status_holder_transform == null)
            {
                Debug.LogWarning($"{nameof(status_holder_transform)} is not assigned to {typeof(UI_StatusSystem)} of {name}");
            }
#endif
        }

        public void AddStatus(StatusObject status_toAdd)
        {
            var instance = Instantiate(status_display_prefab, status_holder_transform);
            var status_display = instance.GetComponent<UI_StatusObject>();

            status_display.Inicialize(this, status_toAdd);
        }


    }
}