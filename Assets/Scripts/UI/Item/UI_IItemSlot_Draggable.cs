using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using Survival2D.Systems.Item;

namespace Survival2D.UI.Item
{
    public abstract class UI_IItemSlot_Draggable : UI_IItemSlot, IPointerDownHandler, IBeginDragHandler, IEndDragHandler, IDragHandler, IDropHandler
    {
        public class IDraggableSlotEvent : UnityEvent<UI_IItemSlot> { }

        public UI_ItemDragImage ItemDragDisplay { private get; set; } = null;

        public IDraggableSlotEvent onSlotDragged { get; } = new IDraggableSlotEvent();


        public void OnBeginDrag(PointerEventData eventData)
        {
            if (!is_displaying_slot) return;

            onSlotDragged.Invoke(this);

            ItemDragDisplay.gameObject.SetActive(true);
            ItemDragDisplay.RecieveSlotDragged(this, item_displayed);

            ui_canvas_group.blocksRaycasts = false;
        }

        public void OnDrag(PointerEventData eventData)
        {
            if (!is_displaying_slot) return;

            ItemDragDisplay.MoveDelta(eventData.delta);
        }

        public void OnDrop(PointerEventData eventData)
        {
            var slot_displayed_startDrag = eventData.pointerDrag.GetComponent<UI_IItemSlot_Draggable>();
            if (CanItemToSystemDisplayed(slot_displayed_startDrag.item_displayed) &&
                slot_displayed_startDrag.CanItemToSystemDisplayed(this.item_displayed))
            {
                var item = SendItemToSystem(slot_displayed_startDrag.item_displayed);
                slot_displayed_startDrag.SendItemToSystem(item);

            }
        }

        public void OnEndDrag(PointerEventData eventData)
        {
            ui_canvas_group.blocksRaycasts = true;
            ItemDragDisplay.gameObject.SetActive(false);
        }

        public void OnPointerDown(PointerEventData eventData)
        {
        }
        public abstract bool CanItemToSystemDisplayed(ItemObject item_sentToSystem);
        public abstract ItemObject SendItemToSystem(ItemObject other_slot_item);
    }
}