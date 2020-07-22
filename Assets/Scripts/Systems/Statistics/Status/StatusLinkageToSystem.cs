using UnityEngine.Events;
using System.Collections.Generic;

namespace Survival2D.Systems.Statistics.Status
{
    public class StatusLinkageToStat
    {
        public StatModifier modifier;
        public List<Stat> stats_linked;
        public UnityEvent onModifierRemoval;
    }

    public class StatusLinkageToIncrementalStat
    {
        public IncrementalStatModifier modifier;
        public List<IncrementalStat> stats_linked;
        public UnityEvent onModifierRemoval;
    }
}