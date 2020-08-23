using System;
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

        protected override void OnDestroy()
        {
            base.OnDestroy();

            Behaviour.StatusSystem.OnNewStatusAdded -= AddNewStatus;
            Behaviour.StatusSystem.OnStatusUpdated -= UpdateSpecificStatus;
            Behaviour.StatusSystem.OnStatusRemoved -= RemoveSpecificStatus;

        }

        private void AddNewStatus(StatusSystemArgs args)
        {
            var instance = Instantiate(status_display_prefab, status_holder_transform);
            var status_display = instance.GetComponent<UI_StatusObject>();

            status_display.Initialize(args.StatusObject);
            status_display_container.Add(status_display);
        }

        private void UpdateSpecificStatus(StatusSystemArgs args)
        {
            status_display_container[args.SlotContained].UpdateStatusDisplay(args.StatusObject);
        }

        private void RemoveSpecificStatus(StatusSystemArgs args)
        {
            // Improve, maybe use a object pool

            var status_display = status_display_container[args.SlotContained];
            status_display_container.RemoveAt(args.SlotContained);
            Destroy(status_display.gameObject);
        }

        // Add callbacks to status system
        protected override void InitializeBehaviour()
        {
            EventHandler handler = null;
            handler = delegate
            {
                var status_system = Behaviour.StatusSystem;

                status_system.OnNewStatusAdded += AddNewStatus;
                status_system.OnStatusUpdated += UpdateSpecificStatus;
                status_system.OnStatusRemoved += RemoveSpecificStatus;


                Behaviour.OnSystemInitialized -= handler;
            };

            Behaviour.OnSystemInitialized += handler;
        }
    }
}