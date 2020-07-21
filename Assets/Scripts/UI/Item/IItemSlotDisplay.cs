using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;

using Survival2D.Systems;
using Survival2D.Systems.Item;

namespace Survival2D.UI.Item
{
    public abstract class IItemSlotDisplay : MonoBehaviour, IPointerDownHandler, IBeginDragHandler, IEndDragHandler, IDragHandler, IDropHandler
    {
        public class IDraggableSlotEvent : UnityEvent<IItemSlotDisplay> { }

        [SerializeField] public Image ui_image = null;
        [SerializeField] public Image ui_background = null;
        [HideInInspector] public CanvasGroup ui_canvas_group = null;

        [HideInInspector] public ItemDragDisplay item_drag_display = null;

        [HideInInspector] public IDraggableSlotEvent onSlotDragged = new IDraggableSlotEvent();

        private ItemObject item_displayed = null;
        private bool is_displaying_slot =  false;

        protected virtual void Awake()
        {
            if (ui_image == null)
            {
                Debug.LogWarning($"{nameof(ui_image)} is not assigned to {nameof(IItemSlotDisplay)} of {name}");
            }

            if (ui_background == null)
            {
                Debug.LogWarning($"{nameof(ui_background)} is not assigned to {nameof(IItemSlotDisplay)} of {name}");
            }
        }

        public virtual void InicializeDisplay(IItemContainer slot_toDisplay, Sprite background_image = null)
        {
            is_displaying_slot = !slot_toDisplay.IsEmpty;
            if (is_displaying_slot)
            {
                OnObjectInicialized(slot_toDisplay.ItemContained);
            }
            else
            {
                OnObjectNonInicialized(slot_toDisplay.ItemContained);

            }

            ui_background.sprite = background_image;
        }

        public void OnBeginDrag(PointerEventData eventData)
        {
            if (!is_displaying_slot) return;

            onSlotDragged.Invoke(this);

            item_drag_display.gameObject.SetActive(true);
            item_drag_display.RecieveSlotDragged(this, item_displayed);

            ui_canvas_group.blocksRaycasts = false;
        }

        public void OnDrag(PointerEventData eventData)
        {
            if (!is_displaying_slot) return;

            item_drag_display.MoveDelta(eventData.delta);
        }

        public void OnDrop(PointerEventData eventData)
        {
            var slot_displayed_startDrag = eventData.pointerDrag.GetComponent<IItemSlotDisplay>();
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
            item_drag_display.gameObject.SetActive(false);
        }

        public void OnPointerDown(PointerEventData eventData)
        {
        }

        public abstract bool CanItemToSystemDisplayed(ItemObject item_sentToSystem);
        public abstract ItemObject SendItemToSystem(ItemObject other_slot_item);


        protected virtual void OnObjectInicialized(ItemObject item_object)
        {
            item_displayed = item_object;
            ui_image.sprite = item_object.ItemData.ui_display;
        }

        protected virtual void OnObjectNonInicialized(ItemObject item_object)
        {
            ui_image.sprite = null;
        }

    }
}