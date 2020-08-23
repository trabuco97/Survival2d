using System.Collections.Generic;

namespace Survival2D.Systems.Item.Equipment
{
    public class EquipmentGroupType 
    {
        private Stack<EquipmentSlot> type_slots = null;

        private EquipmentSystem equipment = null;
        private int max_slots = 0;

        public int SlotsUnlocked { get { return type_slots.Count; } }

        public ItemType type = ItemType.MAX_TYPES;

        public EquipmentGroupType(EquipmentSystem equipment, ItemType type, int max_slots = 1)
        {
            this.equipment = equipment;
            this.max_slots = max_slots;
            this.type = type;
            type_slots = new Stack<EquipmentSlot>(max_slots);
        }

        public bool EquipItem(ItemObject equipable, out ItemObject last_equipable, out int slot_modified)
        {
            var slots_array = GetSlotArray();
            EquipmentSlot slot_toAccess;

            int slot_number_toAccess = slots_array.Length - 1;
            while (slot_number_toAccess >= 0 && !slots_array[slot_number_toAccess].IsEmpty)
            {
                slot_number_toAccess--;
            }

            slot_toAccess = slots_array[slot_number_toAccess];

            if (slot_toAccess != null)
            {
                last_equipable = slot_toAccess.RemoveItem();
                slot_toAccess.AddItem(equipable);

                slot_modified = slots_array.Length - slot_number_toAccess - 1; 
                return true;
            }

            last_equipable = null;
            slot_modified = -1;
            return false;
        }

        public bool EquipItem(ItemObject equipable, out ItemObject last_equipable, int num_slot)
        {
            EquipmentSlot slot_toAccess = GetSlot(num_slot);

            if (slot_toAccess != null)
            {
                last_equipable = slot_toAccess.RemoveItem();
                slot_toAccess.AddItem(equipable);

                return true;
            }

            last_equipable = null;
            return false;
        }

        public ItemObject GetEquipable(int slot_number = -1)
        {
            var type_slot_array = GetSlotArray();
            if (slot_number != - 1)
            {
                return GetSlot(slot_number).ItemContained;
            }
            else
            {
                for (int i = type_slot_array.Length - 1; i >= 0; i--)
                {
                    var type_slot = type_slot_array[i];
                    if (!type_slot.IsEmpty)
                        return type_slot.ItemContained;
                }

                return null;
            }
        }

        public ItemObject[] GetEquipableArray()
        {
            var slots = type_slots.ToArray();
            var equipables = new List<ItemObject>(slots.Length);

            for (int i = equipables.Count - 1; i >= 0; i--)
            {
                equipables.Add(slots[i].ItemContained);
            }

            return equipables.ToArray();
        }

        public EquipmentSlot GetSlot(int slot_number)
        {
            if (slot_number >= 0 && slot_number < SlotsUnlocked)
            {
                return GetSlotArray()[type_slots.Count - 1 - slot_number];
            }

            return null;
        }

        public EquipmentSlot[] GetSlotArray()
        {
            return type_slots.ToArray();
        }

        public void UnlockSlot()
        {
            if (SlotsUnlocked < max_slots)
            {
                type_slots.Push(new EquipmentSlot(equipment, type));
            }
        }

        public void LockSlot(out ItemObject equipable_slotless)
        {
            equipable_slotless = null;

            if (type_slots.Count > 0)
            {
                EquipmentSlot slot_locked = type_slots.Pop();
                equipable_slotless = slot_locked.ItemContained;
            }
        }
    }
}