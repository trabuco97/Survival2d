using System;
using UnityEngine;
using System.Collections.Generic;

namespace Survival2D.Systems.Statistics.Status
{
    public class StatusSystem
    {
        public delegate ISystemWithStatus SystemWithStatusReciever(SystemType type);


        private List<StatusObject> status_applied_container = null;
        private SystemWithStatusReciever system_reciever;

        public StatusObjectEvent onStatusInicialized { get; } = new StatusObjectEvent();
        public StatusObjectContainedEvent onStatusUpdate { get; } = new StatusObjectContainedEvent();
        public StatusObjectContainedEvent onStatusRemoved { get; } = new StatusObjectContainedEvent();

        public StatusSystem(SystemWithStatusReciever system_getter)
        {
            status_applied_container = new List<StatusObject>();
            system_reciever = system_getter;
        }

        public void AddStatus(string status_name)
        {
            if (!HasEntityStatus(status_name))
            {
                if (StatusDatabaseBehaviour.Instance.TryGetStatus(status_name, out StatusObject status_object))
                {
                    StatusData status_data = status_object.status_data;
                    foreach (var modifier_data in status_data.modifiers_data)
                    {
                        // this is workaround, improve if necessary
                        // the system is obtain asumming the system are in their own gameobject, childs of the entity object

                        var system = system_reciever(modifier_data.type);

                        if (system != null)
                        {
                            StatusLinkageToStat linkage = system.LinkModifierToStat(modifier_data);
                            status_object.linkage_container.Add(linkage);

                        }
#if UNITY_EDITOR
                        else
                        {
                            Debug.LogError("error trying to get system to apply status");
                        }
#endif
                    }

                    foreach (var incremental_modifier_data in status_data.incremental_modifiers_data)
                    {
                        var system = system_reciever(incremental_modifier_data.type);

                        if (system != null)
                        {
                            StatusLinkageToIncrementalStat incremental_linkage = system.LinkIncrementalModifierToStat(incremental_modifier_data);
                            status_object.incremental_linkage_container.Add(incremental_linkage);

                        }
#if UNITY_EDITOR
                        else
                        {
                            Debug.LogError("error trying to get system to apply status");
                        }
#endif
                    }

                    status_applied_container.Add(status_object);
                    onStatusInicialized.Invoke(status_object);
                }
            }
            else
            {
                StatusObject status_object = GetEntityStatus(status_name);
                status_object.actual_status_duration = status_object.status_data.status_duration;

                onStatusInicialized.Invoke(status_object);
            }
        }

        public void UpdateStatusDuration()
        {
            for (int i = status_applied_container.Count - 1; i >= 0; i--)
            {
                var status_object = status_applied_container[i];
                status_object.actual_status_duration -= Time.deltaTime;


                if (status_object.actual_status_duration <= 0)
                {
                    DeleteStatus(status_object, i);
                }
                else
                {
                    onStatusUpdate.Invoke(status_object, i);
                }
            }
        }

        private bool HasEntityStatus(string status_toCheck)
        {
            foreach (var entity_status in status_applied_container)
            {
                if (entity_status.status_data.status_name == status_toCheck)
                {
                    return true;
                }
            }

            return false;
        }

        private StatusObject GetEntityStatus(string status_toFind)
        {
            foreach (var entity_status in status_applied_container)
            {
                if (entity_status.status_data.status_name == status_toFind)
                {
                    return entity_status;
                }
            }

            return null;
        }

        private void DeleteStatus(StatusObject status_object, int index)
        {
            if (status_object.linkage_container != null)
            {
                foreach (var linkage in status_object.linkage_container)
                {
                    foreach (var stat in linkage.stats_linked)
                    {
                        stat.RemoveModifier(linkage.modifier);
                    }

                    linkage.onModifierRemoval.Invoke();
                }
            }

            if (status_object.incremental_linkage_container != null)
            {
                foreach (var incremental_linkage in status_object.incremental_linkage_container)
                {
                    foreach (var incremental_stat in incremental_linkage.stats_linked)
                    {
                        incremental_stat.RemoveIncrementalModifier(incremental_linkage.modifier);
                    }

                    incremental_linkage.onModifierRemoval.Invoke();
                }
            }

            onStatusRemoved.Invoke(status_object, index);
            status_applied_container.RemoveAt(index);
        }
    }
}