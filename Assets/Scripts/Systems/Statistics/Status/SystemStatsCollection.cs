using System;
using System.Collections.Generic;
using UnityEngine;

namespace Survival2D.Systems.Statistics.Status
{

    public class SystemStatsCollection
    {
        private Stat[] stats_collection;
        private List<Action>[] delegate_onModifier_collection;

        public SystemStatsCollection(Stat[] stat_array)
        {
            stats_collection = stat_array;
            delegate_onModifier_collection = new List<Action>[stats_collection.Length];

            for (int i = 0; i < delegate_onModifier_collection.Length; i++)
            {
                delegate_onModifier_collection[i] = new List<Action>();
            }
        }


        public Stat this[int stat_index]
        {
            get 
            { 
                return stats_collection[stat_index]; 
            }
        }

        public void AddDelegateOnStatModifier(int stat_index, Action statModifierDelegate)
        {
            if (stat_index >= 0 && stat_index < stats_collection.Length)
            {
                delegate_onModifier_collection[stat_index].Add(statModifierDelegate);
            }
            else
            {
                Debug.LogError("ERROR: asigning delegate to an nonexistent stat in the system");
            }

        }

        public void RemoveDelegateOnStatModifier(int stat_index, Action statModifierDelegate)
        {
            if (stat_index >= 0 && stat_index < stats_collection.Length)
            {
                delegate_onModifier_collection[stat_index].Remove(statModifierDelegate);
            }
            else
            {
                Debug.LogError("ERROR: asigning delegate to an nonexistent stat in the system");
            }

        }

        public StatusLinkageToStat AddModifier(StatModifierData statModifier_data)
        {
            var output = new StatusLinkageToStat();
            output.modifier = statModifier_data.modifier;

            int i = statModifier_data.stat_index;
            if (i >= 0 && i < stats_collection.Length)
            {
                stats_collection[i].AddModifier(statModifier_data.modifier);
                output.stat_linked = stats_collection[i];
                output.removal_methods = delegate_onModifier_collection[i];

                foreach (var action in delegate_onModifier_collection[i])
                {
                    action();
                }
            }
            else
            {
                Debug.LogError("ERROR: asigning statmodifier to an nonexistent stat in the system");
            }

            return output;
        }

        public StatusLinkageToIncrementalStat AddIncrementalModifier(IncrementalStatModifierData statModifier_data)
        {
            var output = new StatusLinkageToIncrementalStat();
            output.modifier = statModifier_data.modifier;

            int i = statModifier_data.stat_index;
            if (i >= 0 && i < stats_collection.Length && stats_collection[i] is IncrementalStat)
            {
                var incremental_stat = stats_collection[i] as IncrementalStat;

                incremental_stat.AddIncrementalModifier(statModifier_data.modifier);
                output.stat_linked = incremental_stat;
                output.removal_methods = delegate_onModifier_collection[i];
            }
            else
            {
                Debug.LogError("ERROR: asigning statmodifier to an nonexistent stat in the system");
            }


            return output;
        }

    }
}