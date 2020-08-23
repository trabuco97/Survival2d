using System;
#if UNITY_EDITOR
using UnityEngine;
#endif

namespace Survival2D.Systems.Item.Inventory.Hotbar
{
    public class HotbarSystem : IDisposable
    {
        private const int HOTBAR_SIZE = 10;
        private InventorySpace hotbar_space = null;
        private InventorySystem inventory = null;


        public uint SlotIndex { get; private set; } = 0;
        public InventorySlot SlotSelected { get { return hotbar_space.Slots[SlotIndex]; } }
        public InventorySlot[] Slots { get { return hotbar_space.Slots; } }

        //Events
        public event InventoryMethods OnHotbarSlotSelected;
        public event EventHandler OnHotbarSpaceChanged;


        public HotbarSystem(InventorySystem system, InventorySpace space)
        {
            inventory = system;

            if (space.Size == HOTBAR_SIZE)
            {
                hotbar_space = space;
            }
#if UNITY_EDITOR
            else
            {
                Debug.LogError("hotbar space not right size");
            }
#endif


            inventory.OnSlotModified += OnHotbarSlotModified;
        }

        public void ChangeSlotSelected(uint new_slot_selected_index)
        {
            if (new_slot_selected_index >= 0 && new_slot_selected_index < HOTBAR_SIZE)
            {
                var previous_slot_selected = SlotSelected;
                if (SlotIndex != new_slot_selected_index)
                {
                    SlotIndex = new_slot_selected_index;
                    OnHotbarSlotSelected.Invoke(new InventoryEventArgs(SlotSelected, previous_slot_selected));
                }
            }
#if UNITY_EDITOR
            else
            {
                Debug.LogError("slot index outof range in hotbar system");
            }
#endif
        }

        private void OnHotbarSlotModified(InventoryEventArgs args)
        {
            if (hotbar_space.CheckIfContainsSlot(args.SlotModified))
            {
                OnHotbarSpaceChanged.Invoke(this, EventArgs.Empty);
            }
        }

        public void Dispose()
        {
            inventory.OnSlotModified -= OnHotbarSlotModified;
        }
    }
}