using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;

using Survival2D.Systems;
using Survival2D.Systems.Item;

namespace Survival2D.UI.Item
{
    public abstract class UI_IItemSlot : MonoBehaviour
    {
        [SerializeField] public Image ui_image = null;
        [SerializeField] public Image ui_background = null;
        [HideInInspector] public CanvasGroup ui_canvas_group = null;

        protected ItemObject item_displayed = null;
        protected bool is_displaying_slot =  false;

        protected virtual void Awake()
        {
#if UNITY_EDITOR
            if (ui_image == null)
            {
                Debug.LogWarning($"{nameof(ui_image)} is not assigned to {nameof(UI_IItemSlot)} of {name}");
            }

            if (ui_background == null)
            {
                Debug.LogWarning($"{nameof(ui_background)} is not assigned to {nameof(UI_IItemSlot)} of {name}");
            }
#endif
        }

        public virtual void InitializeDisplay(IItemContainer slot_toDisplay, Sprite background_image = null)
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