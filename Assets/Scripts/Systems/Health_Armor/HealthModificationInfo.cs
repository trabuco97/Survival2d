using UnityEngine;
using System.Collections;

using Survival2D.Systems.Statistics;

namespace Survival2D.Systems.HealthArmor
{
    public class HealthModificationInfo
    {
        public IncrementalStat.AdditiveTemporaryType temporal_delta_type;
        public float health_delta_value;
        public string[] status_applied;
    }
}