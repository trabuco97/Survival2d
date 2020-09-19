using UnityEngine;
using System.Collections;

using Survival2D.Systems.Statistics;

namespace Survival2D.Systems.HealthArmor
{
    public class HealthModificationInfo
    {
        public IncrementalStat.AdditiveTemporaryType DeltaValueType { get; private set; }
        public float HealthDeltaValue { get; private set; }
        public string[] StatusApplied { get; private set; }


        public HealthModificationInfo(IncrementalStat.AdditiveTemporaryType delta_type, float delta_value)
        {
            DeltaValueType = delta_type;
            HealthDeltaValue = delta_value;
        }

        public HealthModificationInfo(IncrementalStat.AdditiveTemporaryType delta_type, float delta_value, string[] status_applied) : this(delta_type, delta_value)
        {
            StatusApplied = status_applied;
        }
    }
}