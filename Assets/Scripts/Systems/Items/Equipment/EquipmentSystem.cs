#if UNITY_EDITOR
using UnityEngine;
#endif
using System.Collections.Generic;

using Survival2D.Systems.Statistics.Status;

namespace Survival2D.Systems.Item.Equipment
{
    public class EquipmentSystem : IItemSystem
    {
        private StatusSystem status_system;

        private Dictionary<ItemType, EquipmentGroupType> equipment_groups = null;
        private Dictionary<ItemType, List<EquipmentMethods>> onEquipableReplacedEvents = new Dictionary<ItemType, List<EquipmentMethods>>();

        public EquipmentSystem(StatusSystem status_system)
        {
            this.status_system = status_system;
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
                    if (onEquipableReplacedEvents.TryGetValue(type, out List<EquipmentMethods> OnEvents))
                    {
                        var args = new EquipmentSlotArgs { LastItemInSlot = last_equipable, Slot = group_type.GetSlot(slot_modified) };

                        // Execute all delegates assigned to the type
                        foreach (var current_event in OnEvents)
                        {
                            current_event(args);
                        }
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

        public bool TryAddMethodToEquipType(ItemType type, EquipmentMethods method)
        {
            if (onEquipableReplacedEvents.TryGetValue(type, out var methods_list))
            {
                methods_list.Add(method);
                return true;
            }

            return false;
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
                var type = item_toEvaluate.Type;
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
            onEquipableReplacedEvents.Add(ItemType.Suit, new List<EquipmentMethods>());
            TryAddMethodToEquipType(ItemType.Suit, Handler_ManageEquipableStatus);

        }

        private void Handler_ManageEquipableStatus(EquipmentSlotArgs args)
        {
            var last_equipable = args.LastItemInSlot;
            var equipable = args.Slot.ItemContained;

            var last_status_eqp = last_equipable as IEquipableObjWithStatus;
            var status_eqp = equipable as IEquipableObjWithStatus;

            if (last_status_eqp != null)
            {
                foreach (var status_name in last_status_eqp.StatusNames)
                {
                    status_system.TryRemoveStatus(status_name);
                }
            }

            if (status_eqp != null)
            {


                foreach (var status_name in status_eqp.StatusNames)
                {
                    status_system.AddStatus(status_name);
                }
            }
        }

    }
}