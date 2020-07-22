using System;

namespace Survival2D.Systems.Statistics
{
    // all values are positive,
    // the stat system interpret the calculations if the incrementtal stat is increase or decrease

    [Serializable]
    public class IncrementalStatModifier : StatModifier
    {
        public enum IncrementalType { Increase, Decrease, MAX_TYPES }

        public IncrementalType incrementalType = IncrementalType.MAX_TYPES;

    }
}