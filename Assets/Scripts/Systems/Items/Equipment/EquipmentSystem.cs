using UnityEngine;
using System.Collections.Generic;

namespace Survival2D.Systems.Item.Equipment
{
    public class EquipmentSystem : IItemSystem
    {
        private Dictionary<ItemType, EquipmentGroupType> equipment_groups = null;
        public Dictionary<ItemType, EquipableSlotEvent> onEquipableReplacedEvents = new Dictionary<ItemType, EquipableSlotEvent>();

        public EquipmentSystem()
        {
            InicializeGroups();
        }

        public bool EquipItem(ItemType type, ItemObject equipable, out ItemObject last_equipable, int slot_number = -1)
        {
            last_equipable = null;
            if (!ItemSystemContainerChecker.ItMatches(type, SystemType.Equipment)) return false;

            equipment_groups.TryGetValue(type, out EquipmentGroupType group_type);
            if (group_type != null)
            {
                bool result;
                int slot_modified;
                if (slot_number == -1)
                {
                    result = group_type.EquipItem(equipable, out var last_equipped, out var slot_modified_out);
                    slot_modified = slot_modified_out;
                    last_equipable = last_equipped;
                }
                else
                {
                    result = group_type.EquipItem(equipable, out var last_equipped, slot_number);
                    slot_modified = slot_number;
                    last_equipable = last_equipped;
                }

                if (result)
                {
                    if (onEquipableReplacedEvents.TryGetValue(type, out EquipableSlotEvent onEvent))
                    {
                        onEvent.Invoke(group_type.GetSlot(slot_modified), last_equipable);
                    }

                    return true;
                }

                return false;
            }
            else
            {
                return false;
            }
        }

        public EquipmentGroupType GetGroup(ItemType type)
        {
            if (equipment_groups.TryGetValue(type, out EquipmentGroupType group_type))
            {
                return group_type;
            }

            return null;
        }

        public ItemObject GetEquipableType(ItemType type, int slot = -1)
        {
            if (equipment_groups.TryGetValue(type, out EquipmentGroupType group_type))
            {
                return group_type.GetEquipable(slot);
            }

            return null;
        }

        public ItemObject[] GetEquipableTypeArray(ItemType type)
        {
            if (equipment_groups.TryGetValue(type, out EquipmentGroupType group_type))
            {
                return group_type.GetEquipableArray();
            }

            return null;
        }

        public bool IsItemUsedInSystem(ItemObject item_toEvaluate)
        {
            bool evaluation = item_toEvaluate == null;
            if (!evaluation)
            {
                var type = item_toEvaluate.type;
                evaluation = type == ItemType.Suit;
            }

            return evaluation;
        }

        // Default equipment types for every entity
        private void InicializeGroups()
        {
            equipment_groups = new Dictionary<ItemType, EquipmentGroupType>();

            var group = new EquipmentGroupType(this, ItemType.Suit);
            group.UnlockSlot();

            equipment_groups.Add(ItemType.Suit, group);


            onEquipableReplacedEvents.Add(ItemType.Suit, new EquipableSlotEvent());
        }



    }
}