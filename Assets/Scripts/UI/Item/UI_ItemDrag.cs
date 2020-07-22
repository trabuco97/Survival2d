using UnityEngine;
using UnityEngine.UI;

using Survival2D.Systems.Item;

namespace Survival2D.UI.Item
{
    public class UI_ItemDrag : MonoBehaviour
    {
        [SerializeField] private Canvas ui_canvas = null;
        [SerializeField] private Image ui_image = null;

        private RectTransform rect_transform = null;

        public UI_IItemSlot SlotDragged { get; private set; }

        private void Awake()
        {
#if UNITY_EDITOR
            if (ui_canvas == null)
            {
                Debug.LogWarning($"{nameof(ui_canvas)} is not assigned to {nameof(UI_ItemDrag)} of {name}");
            }

            if (ui_image == null)
            {
                Debug.LogWarning($"{nameof(ui_image)} is not assigned to {nameof(UI_ItemDrag)} of {name}");
            }
#endif

            rect_transform = GetComponent<RectTransform>();
            gameObject.SetActive(false);
        }

        public void RecieveSlotDragged(UI_IItemSlot slot_display, ItemObject item_object)
        {
            SlotDragged = slot_display;

            rect_transform.position = slot_display.transform.position;

            ui_image.sprite = item_object.ItemData.ui_display;
        }

        public void MoveDelta(Vector2 displacement)
        {
            rect_transform.anchoredPosition += displacement / ui_canvas.scaleFactor;
        }
    }
}