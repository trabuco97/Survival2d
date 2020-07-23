using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Survival2D.Entities.Player;
using Survival2D.Systems.Statistics.Status;

namespace Survival2D.UI.Status
{
    public class UI_StatusSystem : IPlayerBehaviourListener<StatusSystemBehaviour>
    {
        [SerializeField] private Transform status_holder_transform = null;
        [SerializeField] private GameObject status_display_prefab = null;

        private List<UI_StatusObject> status_display_container = new List<UI_StatusObject>();

        protected override void Awake()
        {
            base.Awake();

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

            status_display.Inicialize(status_toAdd);
            status_display_container.Add(status_display);
        }

        // Add callbacks to status system
        protected override void InicializeBehaviour()
        {
            Behaviour.onSystemInicialized.AddListener(delegate
            {
                var status_system = Behaviour.StatusSystem;

                status_system.onStatusInicialized.AddListener(AddStatus);
                status_system.onStatusUpdate.AddListener(delegate (StatusObject status_object, int index)
                {
                    status_display_container[index].UpdateStatusDisplay(status_object);

                });
                status_system.onStatusRemoved.AddListener(delegate (StatusObject status_object, int index)
                {
                    // Improve, maybe use a object pool

                    var status_display = status_display_container[index];
                    status_display_container.RemoveAt(index);
                    Destroy(status_display.gameObject);
                });
            });
        }
    }
}