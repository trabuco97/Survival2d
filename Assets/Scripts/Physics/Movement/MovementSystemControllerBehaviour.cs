using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

using Survival2D.Systems.Statistics.Status;

namespace Survival2D.Physics.Movement
{
    public class MovementSystemControllerBehaviour : ISystemBehaviourWithStatus
    {
        #region Wrappers
        [Serializable]
        private class MovementSystemWrapper 
        {
            public MovementType type = MovementType.MAX_TYPES;
            public IMovementSystem movement_system = null;
            public IMovementTypeHandler handler = null;

        }
        #endregion
        [SerializeField] private MovementSystemWrapper[] movement_draggable_container = null;
        [SerializeField] private MovementType current_movement_atAwake = MovementType.No_Swimmable;

        private Dictionary<MovementType, MovementSystemWrapper> movement_database = new Dictionary<MovementType, MovementSystemWrapper>();
        private IMovementSystem current_movement = null;

        public override UnityEvent OnSystemInicialized => new UnityEvent();
        public override ISystemWithStatus System => this;

        private void Awake()
        {
#if UNITY_EDITOR
            // check if the types are correct
            foreach (var wrapper in movement_draggable_container)
            {
                bool is_matching_types = true;
                switch (wrapper.type)
                {
                    case MovementType.MAX_TYPES:
                        is_matching_types = false;
                        break;
                }

                if (!is_matching_types)
                {
                    Debug.LogWarning($"error, movement type not assigned correctly in {typeof(MovementSystemControllerBehaviour)} of {gameObject.GetFullName()}");
                }
            }
#endif
            InicializeDatabase();
            DisableAllMovement();
            SetCurrentMovement(current_movement_atAwake);
        }

        private void Start()
        {
            OnSystemInicialized.Invoke();
        }

        public void SetCurrentMovement(MovementType type)
        {
            if (movement_database.TryGetValue(type, out var wrapper))
            {
                current_movement = wrapper.movement_system;
                wrapper.handler.ActiveState = true;
                current_movement.ActiveState = true;
            }
#if UNITY_EDITOR
            else
            {
                Debug.LogWarning($"error, movement type not found in movement controller, probably not found in draggable_container in {gameObject.GetFullName()}");
            }
#endif
        }

        public T GetCurrentMovement<T>() where T : IMovementSystem
        {
            return current_movement as T;
        }

        public T GetMovement<T>(MovementType type) where T : IMovementSystem
        {
            if (movement_database.TryGetValue(type, out var movement))
            {
                return movement as T;
            }

            return null;
        }

        private void InicializeDatabase()
        {
            foreach (var wrapper in movement_draggable_container)
            {
                movement_database.Add(wrapper.type, wrapper);
            }
        }

        private void DisableAllMovement()
        {
            foreach (var wrapper in movement_database.Values)
            {
                wrapper.movement_system.ActiveState = false;
                wrapper.handler.ActiveState = false;
            }
        }

        public override StatusLinkageToIncrementalStat LinkIncrementalModifierToStat(IncrementalStatModifierData statModifier_data)
        {
            throw new System.NotImplementedException();
        }

        public override StatusLinkageToStat LinkModifierToStat(StatModifierData statModifier_data)
        {
            throw new System.NotImplementedException();
        }
    }
}