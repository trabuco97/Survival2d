using System;
using System.Collections.Generic;
using UnityEngine;

using Survival2D.Systems.Statistics;
using Survival2D.Systems.Statistics.Status;

namespace Survival2D.Physics.Movement
{
    public class MovementSystemControllerBehaviour : ISystemBehaviourWithStatus, IOrderedBehaviour
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
        [Header("No Swim Stats")]
        [SerializeField] private float horizontal_grounded_speed_value = 0f;
        [SerializeField] private float horizontal_grounded_acceleration_value = 0f;
        [SerializeField] private float swim_speed_value = 0f;
        [SerializeField] private float horizontal_swim_acceleration_value = 0f;
        [SerializeField] private float jump_potency_value = 0f;

        [Header("References")]
        [SerializeField] private MovementSystemWrapper[] movement_draggable_container = null;
        [SerializeField] private MovementType current_movement_atAwake = MovementType.No_Swimmable;

        private Dictionary<MovementType, MovementSystemWrapper> movement_database = new Dictionary<MovementType, MovementSystemWrapper>();
        private IMovementSystem current_movement = null;

        private SystemStatsCollection stats;

        public override SystemType SystemType => SystemType.Movement;
        public int Order => 1;
        public override SystemStatsCollection Stats => stats;

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
            var stats_array = new Stat[]
            {
                new Stat(horizontal_grounded_speed_value),
                new Stat(horizontal_grounded_acceleration_value),
                new Stat(swim_speed_value),
                new Stat(horizontal_swim_acceleration_value),
                new Stat(jump_potency_value),
            };

            stats = new SystemStatsCollection(stats_array);
            
            InitializeDatabase();
            DisableAllMovement();
            SetCurrentMovement(current_movement_atAwake);
        }

        public void Initialize()
        {
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

        public void SetCurrentMovementRestrictions(bool state)
        {
            current_movement.HasMovementSpecificRestrictions = state;
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

        private void InitializeDatabase()
        {
            foreach (var wrapper in movement_draggable_container)
            {
                wrapper.movement_system.Stats = stats;
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
    }
}