using System;
using System.Collections.Generic;

using UnityEngine;

namespace Survival2D.Systems.Statistics.Status
{
    // Pre: has the same parent as status system behaviour
    // Initialized when all systems initialized
    public class SystemStatusHolderBehaviour : MonoBehaviour, IOrderedBehaviour
    {
        private ISystemWithStatusBehaviour[] system_behaviour_container = null;
        private Dictionary<SystemType, ISystemWithStatus> systemWithStatus_database = new Dictionary<SystemType, ISystemWithStatus>();

        public int Order => 1;

        private void Awake()
        {
            system_behaviour_container = transform.parent.GetComponentsInChildren<ISystemWithStatusBehaviour>();
        }

        public bool TryGetSystem(SystemType type, out ISystemWithStatus systemWithStatus)
        {
            if (systemWithStatus_database.TryGetValue(type, out var system))
            {
                systemWithStatus = system;
                return true;
            }

            systemWithStatus = null;
            return false;
        }

        public void Initialize()
        {
            foreach (var system_behaviour in system_behaviour_container)
            {
                var type = system_behaviour.SystemType;
                if (type != SystemType.MAX_SYSTEMS)
                {
                    systemWithStatus_database.Add(type, system_behaviour.System);
                }
#if UNITY_EDITOR
                else
                {
                    Debug.LogError("error trying to get type from SystemTypeConverter");
                }
#endif

            }
        }
    }
}