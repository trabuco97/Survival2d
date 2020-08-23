using UnityEngine;
using UnityEngine.EventSystems;

using Survival2D.Systems.Item.Equipment;
using Survival2D.Systems.Item;

namespace Survival2D.UI.Item.Equipment
{
    public class UI_EquipmentSlot : UI_IItemSlot_Draggable
    {
        public ItemType Slot_Type { private get; set; }
        public EquipmentSystem Equipment { private get; set; }
        public int SlotNumberDisplayed { private get; set; }

        public override bool CanItemToSystemDisplayed(ItemObject item_sentToSystem)
        {
            return Equipment.IsItemUsedInSystem(item_sentToSystem);
        }

        public override ItemObject SendItemToSystem(ItemObject other_slot_item)
        {
            if (Equipment.EquipItem(Slot_Type, other_slot_item, out ItemObject last_equipable, SlotNumberDisplayed))
            {
                return last_equipable;
            }

            return null;
        }
    }
}