using UnityEngine.Events;

using System.Collections.Generic;

namespace Survival2D.Systems.Statistics.Status
{
    public class EntityStatus   
    {
        public struct EntityModifierLinkage
        {
            public StatModifier modifier;
            public List<Stat> stats_linked;
            public UnityEvent onModifierRemoval;
        }

        public StatusData status_data;
        public float actual_status_duration;
        public List<EntityModifierLinkage> linkage_container;
    }
}