using System;
using UnityEngine;
using System.Collections.Generic;

namespace Survival2D.Systems.Statistics.Status
{
    public class StatusSystem
    {
        public delegate ISystemWithStatus SystemWithStatusReciever(SystemType type);
        public delegate void StatusObjectEvent(StatusSystemArgs args);

        private SystemWithStatusReciever system_reciever;

        private List<StatusObject> status_applied_container = null;

        public event StatusObjectEvent OnNewStatusAdded;
        public event StatusObjectEvent OnStatusUpdated;
        public event StatusObjectEvent OnStatusRemoved;

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
                    Scriptable_StatusData status_data = status_object.status_data;
                    foreach (var modifier_data in status_data.modifiers_data)
                    {
                        // this is workaround, improve if necessary
                        // the system is obtain asumming the system are in their own gameobject, childs of the entity object

                        var system = system_reciever(modifier_data.type);

                        if (system != null)
                        {
                            StatusLinkageToStat linkage = system.Stats.AddModifier(modifier_data);
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
                            StatusLinkageToIncrementalStat incremental_linkage = system.Stats.AddIncrementalModifier(incremental_modifier_data);
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
                    OnNewStatusAdded.Invoke(new StatusSystemArgs { StatusObject = status_object });
                }
            }
            else
            {
                StatusObject status_object = GetEntityStatus(status_name);
                status_object.actual_status_duration = status_object.status_data.status_duration;

                OnNewStatusAdded.Invoke(new StatusSystemArgs { StatusObject = status_object });
            }
        }

        // TODO: maybe improve efficiency of searching the status
        public bool TryRemoveStatus(string name)
        {
            var status_array = status_applied_container.ToArray();
            for (int i = 0; i < status_array.Length; i++)
            {
                var status_obj = status_array[i];
                if (status_obj.status_data.status_name == name)
                {
                    DeleteStatus(status_obj, i);
                    return true;
                }
            }

            return false;
        }

        public void UpdateStatusDuration(float delta_time)
        {
            for (int i = status_applied_container.Count - 1; i >= 0; i--)
            {
                var status_object = status_applied_container[i];
                status_object.actual_status_duration -= delta_time;

                if (status_object.status_data.has_duration && status_object.actual_status_duration <= 0)
                {
                    DeleteStatus(status_object, i);
                }
                else
                {
                    OnStatusUpdated.Invoke(new StatusSystemArgs { StatusObject = status_object, SlotContained = i});
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
                    linkage.stat_linked.RemoveModifier(linkage.modifier);
                    foreach (var method in linkage.removal_methods)
                    {
                        method();
                    }

                }
            }

            if (status_object.incremental_linkage_container != null)
            {
                foreach (var incremental_linkage in status_object.incremental_linkage_container)
                {

                    incremental_linkage.stat_linked.RemoveIncrementalModifier(incremental_linkage.modifier);
                    foreach (var method in incremental_linkage.removal_methods)
                    {
                        method();
                    }
                }
            }
            OnStatusRemoved.Invoke(new StatusSystemArgs { StatusObject = status_object, SlotContained = index });
            status_applied_container.RemoveAt(index);
        }
    }
}