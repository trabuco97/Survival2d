using System;

namespace Survival2D.Systems.Statistics.Status
{
    [Flags]
    public enum StatModified 
    { 
        Stat0 =     1 << 0,
        Stat1 =     1 << 1,
        Stat2 =     1 << 2,
        Stat3 =     1 << 3,
        Stat4 =     1 << 4,
        Stat5 =     1 << 5,
        Stat6 =     1 << 6,
        Stat7 =     1 << 7 
    }

    public abstract class IModifierData
    {
        public SystemType type;
        public StatModified stat_layerMask = StatModified.Stat0;          // layer mask defined by the system
    }

    [Serializable]
    public class StatModifierData : IModifierData
    {
        public StatModifier modifier;
    }

    [Serializable]
    public class IncrementalStatModifierData : IModifierData
    {
        public IncrementalStatModifier modifier;
    }
}