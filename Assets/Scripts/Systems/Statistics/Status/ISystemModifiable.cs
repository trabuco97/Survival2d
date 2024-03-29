﻿using System;
using UnityEngine;

namespace Survival2D.Systems.Statistics.Status
{
    /// <summary>
    /// The system is responsible to link each stat_modifier to its respective stats, declare the events when the stats has modifiers applied and add callbacks when
    /// the status is removed (subsequenly removing the stat_modifiers)
    /// </summary>
    public interface ISystemWithStatus
    {
        SystemStatsCollection Stats { get; }

    }

    public interface ISystemWithStatusBehaviour : ISystemBehaviour
    {
        ISystemWithStatus System { get; }
    }

    public abstract class ISystemBehaviourWithStatus : MonoBehaviour, ISystemWithStatusBehaviour, ISystemWithStatus
    {
        public ISystemWithStatus System => this;
        public abstract SystemType SystemType { get; }
        public abstract SystemStatsCollection Stats { get; }
    }
}        