using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

using Survival2D.Entities.Player;
using Survival2D.Systems.Item.Inventory.Hotbar;

namespace Survival2D.UI.Item.Inventory.Hotbar
{
    public class UI_Hotbar : IPlayerBehaviourListener<HotbarSystemBehaviour>
    {
        [SerializeField] private GridLayoutGroup ui_grid_layout = null; 
        [SerializeField] private GameObject ui_hotbarslot_prefab = null;

        private List<UI_HotbarSlot> slots_container = null;
        private UI_HotbarSlot selected = null;

        private HotbarSystem Hotbar { get { return Behaviour.Hotbar; } }

        protected override void OnDestroy()
        {
            base.OnDestroy();

            Behaviour.OnHotbarInicialized -= InitializedHotbarCallback;
            Behaviour.Hotbar.OnHotbarSpaceChanged -= CallbackUpdateSlotDisplay;
        }

        protected override void InitializeBehaviour()
        {
            Behaviour.OnHotbarInicialized += InitializedHotbarCallback;
        }

        public void InitializeDisplay()
        {
            slots_container = new List<UI_HotbarSlot>();

            for (int i = 0; i < Hotbar.Slots.Length; i++)
            {
                var instance = Instantiate(ui_hotbarslot_prefab, ui_grid_layout.transform);
                if (instance.TryGetComponent(out UI_HotbarSlot slot_display))
                {
                    slots_container.Add(slot_display);
                }
            }

            selected = slots_container[0];
            selected.InitializeSelected();
            UpdateSlotDisplay();
        }

        public void UpdateSlotDisplay()
        {
            for (int i = 0; i < Hotbar.Slots.Length; i++)
            {
                slots_container[i].InitializeDisplay(Hotbar.Slots[i]);
            }
        }


        public void MoveLeftSelected()
        {
            var new_slot_index = Hotbar.SlotIndex - 1;
            if (new_slot_index > Hotbar.Slots.Length) new_slot_index = 0;   // Check of overflow of uint
            Hotbar.ChangeSlotSelected(new_slot_index);
            ChangeSlotDisplayed(new_slot_index);
        }

        public void MoveRightSelected()
        {
            var new_slot_index = Hotbar.SlotIndex + 1;
            if (new_slot_index == Hotbar.Slots.Length) new_slot_index = (uint)Hotbar.Slots.Length - 1;
            Hotbar.ChangeSlotSelected(new_slot_index);
            ChangeSlotDisplayed(new_slot_index);
        }

        public void MoveSelected(uint index)
        {
            Hotbar.ChangeSlotSelected(index);
            ChangeSlotDisplayed(index);
        }

        private void ChangeSlotDisplayed(uint new_index)
        {
            selected.InicializedNonSelected();
            selected = slots_container[(int)new_index];
            selected.InitializeSelected();
        }


        private void CallbackUpdateSlotDisplay(object e, EventArgs args)
        {
            UpdateSlotDisplay();
        }


        private void InitializedHotbarCallback(object e, EventArgs args)
        {
            InitializeDisplay();
            Behaviour.Hotbar.OnHotbarSpaceChanged += CallbackUpdateSlotDisplay;
        }

    }
}