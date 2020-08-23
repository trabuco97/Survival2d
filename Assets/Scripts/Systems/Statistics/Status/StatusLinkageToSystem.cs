using System;
using System.Collections.Generic;

namespace Survival2D.Systems.Statistics.Status
{
    public delegate void OnModifierRemovedMethod();

    public class StatusLinkageToStat
    {
        public StatModifier modifier;
        public List<Stat> stats_linked = new List<Stat>();
        public List<OnModifierRemovedMethod> removal_methods = new List<OnModifierRemovedMethod>();
    }

    public class StatusLinkageToIncrementalStat
    {
        public IncrementalStatModifier modifier;
        public List<IncrementalStat> stats_linked;
        public List<OnModifierRemovedMethod> removal_methods = new List<OnModifierRemovedMethod>();
    }
}