using System;
using System.Collections.Generic;

using UnityEngine;

namespace Survival2D.Systems.Statistics.Status
{
    // Pre: has the same parent as status system behaviour
    public class SystemStatusHolderBehaviour : MonoBehaviour
    {
        private ISystemWithStatusBehaviour[] system_behaviour_container = null;
        private Dictionary<SystemType, ISystemWithStatus> systemWithStatus_database = new Dictionary<SystemType, ISystemWithStatus>();

        public event EventHandler OnSystemStatusInicialized;

        private void Awake()
        {
            system_behaviour_container = transform.parent.GetComponentsInChildren<ISystemWithStatusBehaviour>();

            // add callback when the systems are inicialized
            foreach (var system_behaviour in system_behaviour_container)
            {
                EventHandler handler = null;
                handler = delegate 
                {
                    var type = system_behaviour.SystemType;
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

                    system_behaviour.OnSystemInicialized -= handler;
                };



                system_behaviour.OnSystemInicialized += handler;
            }
        }

        public void Start()
        {
            OnSystemStatusInicialized.Invoke(this, new EventArgs());
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