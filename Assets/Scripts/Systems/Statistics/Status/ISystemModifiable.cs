﻿using UnityEngine.Events;

namespace Survival2D.Systems.Statistics.Status
{
    /// <summary>
    /// The system is responsible to link each stat_modifier to its respective stats, declare the events when the stats has modifiers applied and add callbacks when
    /// the status is removed (subsequenly removing the stat_modifiers)
    /// </summary>
    public interface ISystemWithStatus
    {
        StatusLinkageToStat LinkModifierToStat(StatModifierData statModifier_data);
        StatusLinkageToIncrementalStat LinkIncrementalModifierToStat(IncrementalStatModifierData statModifier_data);
    }

    public interface ISystemWithStatusBehaviour
    {
        UnityEvent OnSystemInicialized { get; }
        ISystemWithStatus System { get; }
    }
}        