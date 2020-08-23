using System;

namespace Survival2D.Systems.Statistics.Status
{
    public class StatusSystemArgs : EventArgs 
    {
        public StatusObject StatusObject { get; set; }
        public int SlotContained { get; set; }
    }

}