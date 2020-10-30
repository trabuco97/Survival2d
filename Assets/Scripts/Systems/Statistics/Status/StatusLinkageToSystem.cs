using System;
using System.Collections.Generic;

namespace Survival2D.Systems.Statistics.Status
{
    public delegate void OnModifierRemovedMethod();

    public class StatusLinkageToStat
    {
        public StatModifier modifier;
        public Stat stat_linked;
        public List<Action> removal_methods;
    }

    public class StatusLinkageToIncrementalStat
    {
        public IncrementalStatModifier modifier;
        public IncrementalStat stat_linked;
        public List<Action> removal_methods;
    }
}