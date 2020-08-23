using UnityEngine.Events;

using System.Collections.Generic;

namespace Survival2D.Systems.Statistics.Status
{
    public class StatusObject   
    {
        public Scriptable_StatusData status_data;
        public float actual_status_duration;
        public List<StatusLinkageToStat> linkage_container;
        public List<StatusLinkageToIncrementalStat> incremental_linkage_container;
    }
}