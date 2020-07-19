using UnityEngine;
using System;
using System.Collections.Generic;

namespace Survival2D.Systems.Statistics.Status
{
    public class StatusSystem : MonoBehaviour
    {
        private List<EntityStatus> status_applied_container = null;

        private void Awake()
        {
            status_applied_container = new List<EntityStatus>();
        }

        private void Update()
        {
            UpdateStatusDuration();
        }

        public void AddStatus(string status_name)
        {
            if (!HasEntityStatus(status_name))
            {
                if (StatusDatabase.Instance.TryGetStatus(status_name, out EntityStatus entity_status))
                {
                    StatusData status_data = entity_status.status_data;
                    foreach (var modifier_data in status_data.modifiers_data)
                    {
                        Type systemType = SystemToTypeConverter.GetSystemType(modifier_data.type);
                        ISystemWithStatus system = transform.parent.GetComponentInChildren(systemType) as ISystemWithStatus;

                        if (system != null)
                        {
                            EntityStatus.EntityModifierLinkage linkage = system.LinkModifierToStat(modifier_data);
                            entity_status.linkage_container.Add(linkage);
                        }
                        else
                        {
                            Debug.LogWarning("dasda");
                        }
                    }

                    status_applied_container.Add(entity_status);
                }
            }
            else
            {
                EntityStatus status = GetEntityStatus(status_name);
                status.actual_status_duration = status.status_data.status_duration;
            }
        }

        private void UpdateStatusDuration()
        {
            for (int i = status_applied_container.Count - 1; i >= 0; i--)
            {
                var entity_status = status_applied_container[i];
                entity_status.actual_status_duration -= Time.deltaTime;
                if (entity_status.actual_status_duration <= 0)
                {
                    DeleteStatus(entity_status, i);
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

        private EntityStatus GetEntityStatus(string status_toFind)
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

        private void DeleteStatus(EntityStatus entity_status, int index)
        {
            foreach (var linkage in entity_status.linkage_container)
            {
                foreach (var stat in linkage.stats_linked)
                {
                    stat.RemoveModifier(linkage.modifier);
                }

                linkage.onModifierRemoval.Invoke();
            }

            status_applied_container.RemoveAt(index);
        }
    }
}