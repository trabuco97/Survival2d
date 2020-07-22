using System.Collections.Generic;

using UnityEngine;
using UnityEngine.Events;

namespace Survival2D.Systems.Statistics.Status
{
    // Pre: has the same parent as status system behaviour
    public class SystemStatusHolderBehaviour : MonoBehaviour
    {
        private ISystemWithStatusBehaviour[] system_behaviour_container = null;
        private Dictionary<SystemType, ISystemWithStatus> systemWithStatus_database = null;

        public UnityEvent onSystemStatusInicialized { get; } = new UnityEvent();

        private void Awake()
        {
            system_behaviour_container = transform.parent.GetComponentsInChildren<ISystemWithStatusBehaviour>();
            systemWithStatus_database = new Dictionary<SystemType, ISystemWithStatus>();
        }

        public void Start()
        {
            // add callback when the systems are inicialized
            foreach (var system_behaviour in system_behaviour_container)
            {
                system_behaviour.OnSystemInicialized.AddListener(delegate
                {
                    var type = SystemTypeConverter.GetSystemFromType(system_behaviour.System.GetType());
                    if (type != SystemType.None)
                    {
                        systemWithStatus_database.Add(type, system_behaviour.System);
                    }
#if UNITY_EDITOR
                    else
                    {
                        Debug.LogError("error trying to get type from SystemTypeConverter");
                    }
#endif
                });
            }

            onSystemStatusInicialized.Invoke();
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
    }
}