using UnityEngine;
using System.Collections;

namespace Survival2D.Systems.Item.Equipment
{
    public class EquipmentSlot : IItemContainer
    {
        public EquipmentSystem equipment = null;
        public ItemType slot_type = ItemType.MAX_TYPES;

        public EquipmentSlot(EquipmentSystem equipment, ItemType slot_type)
        {
            this.equipment = equipment;
            this.slot_type = slot_type;
        }
    }
}