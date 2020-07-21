using System.Collections.Generic;

namespace Survival2D.Systems.Statistics.Status
{
    /// <summary>
    /// The system is responsible to link each stat_modifier to its respective stats, declare the events when the stats has modifiers applied and add callbacks when
    /// the status is removed (subsequenly removing the stat_modifiers)
    /// </summary>
    public interface ISystemWithStatus
    {
        EntityStatus.EntityModifierLinkage LinkModifierToStat(StatModifierData statModifier_data);
    }
}