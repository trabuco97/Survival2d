using UnityEngine;
using UnityEngine.EventSystems;

using Survival2D.Systems.Item.Equipment;
using Survival2D.Systems.Item;

namespace Survival2D.UI.Item.Equipment
{
    public class EquipmentSlotDisplay : IItemSlotDisplay
    {
        public ItemType Slot_Type { private get; set; }
        public EquipmentDisplay EquipmentDisplay { private get; set; }
        public int SlotNumberDisplayed { private get; set; }

        public override bool CanItemToSystemDisplayed(ItemObject item_sentToSystem)
        {
            return EquipmentDisplay.equipment_system.IsItemUsedInSystem(item_sentToSystem);
        }

        public override ItemObject SendItemToSystem(ItemObject other_slot_item)
        {
            if (EquipmentDisplay.equipment_system.EquipItem(Slot_Type, other_slot_item, out ItemObject last_equipable, SlotNumberDisplayed))
            {
                return last_equipable;
            }

            return null;
        }
    }
}