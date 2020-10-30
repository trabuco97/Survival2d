using System;

using MyBox;

namespace Survival2D.Systems.Statistics.Status
{
    public enum DefaultStatModified 
    { 
        Stat0   ,
        Stat1   ,
        Stat2   ,
        Stat3   ,
    }

    public abstract class IModifierData
    {
        public SystemType type;
        [ConditionalField("type", true, SystemType.MAX_SYSTEMS)] public DefaultStatModified defaultStat;
        [ConditionalField("type", false, SystemType.Health)] public HealthArmorStats healthArmorStat;
        [ConditionalField("type", false, SystemType.Movement)] public MovementStats movementStat;

        public int stat_index;

        /// <summary>
        /// Always called before using the class
        /// </summary>
        public void InitIndex()
        {
            switch (type)
            {
                case SystemType.Health:
                    stat_index = (int)healthArmorStat;
                    break;
                case SystemType.Movement:
                    stat_index = (int)movementStat;
                    break;
                default:
                    stat_index = (int)defaultStat;
                    break;
            }
        }
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