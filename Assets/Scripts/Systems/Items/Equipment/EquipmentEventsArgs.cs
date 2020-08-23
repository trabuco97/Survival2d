using System;

namespace Survival2D.Systems.Item.Equipment
{

    public delegate void EquipmentMethods(EquipmentSlotArgs args);

    public class EquipmentSlotArgs : EventArgs
    {
        public ItemObject LastItemInSlot { get; set; }
        public EquipmentSlot Slot { get; set; }
    }

}
