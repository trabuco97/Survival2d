﻿using UnityEngine;
using System;
using System.Collections.Generic;

namespace Survival2D.Systems.Statistics.Status
{
    public class StatusDatabaseBehaviour : MonoBehaviour
    {
        [SerializeField] private List<Scriptable_StatusData> status_draggable_holder = new List<Scriptable_StatusData>();

        private Dictionary<string, Scriptable_StatusData> status_container = null;

        private static StatusDatabaseBehaviour instance;
        public static StatusDatabaseBehaviour Instance { get { return instance; } }

        private void Awake()
        {
            InicializeDatabase();
            if (instance == null)
            {
                instance = this;
            }
        }


        private void InicializeDatabase()
        {
            status_container = new Dictionary<string, Scriptable_StatusData>();

            foreach (var status_data in status_draggable_holder)
            {
                status_container.Add(status_data.status_name, status_data);
            }
        }


        public bool TryGetStatus(string name, out StatusObject entity_status)
        {
            bool is_status_found = status_container.TryGetValue(name, out Scriptable_StatusData base_status);
            if (is_status_found)
            {
                entity_status = new StatusObject
                {
                    status_data = base_status,
                    actual_status_duration = base_status.status_duration,
                    linkage_container = new List<StatusLinkageToStat>()
                };
            }
            else
            {
                entity_status = null;
            }

            return is_status_found;
        }

        public string[] GetStatusNames()
        {
            var output = new string[status_draggable_holder.Count];
            int i = 0;
            foreach (var status in status_draggable_holder)
            {
                output[i++] = status.status_name;
            }

            return output;
        }
    }
}