using UnityEngine.Events;

namespace Survival2D.Systems.Statistics.Status
{
    public class StatusObjectEvent : UnityEvent<StatusObject> { }

    // Passes the status destroyed and the index of the contain which was stored
    public class StatusObjectContainedEvent : UnityEvent<StatusObject, int> { }
}