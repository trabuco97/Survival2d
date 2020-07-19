﻿using UnityEngine;
using UnityEngine.UI;

using Survival2D.Systems.Item;

namespace Survival2D.UI.Item
{
    public class ItemDragDisplay : MonoBehaviour
    {
        [SerializeField] private Canvas ui_canvas = null;
        [SerializeField] private Image ui_image = null;

        private RectTransform rect_transform = null;

        public IItemSlotDisplay SlotDragged { get; private set; }

        private void Awake()
        {
            if (ui_canvas == null)
            {
                Debug.LogWarning($"{nameof(ui_canvas)} is not assigned to {nameof(ItemDragDisplay)} of {name}");
            }

            if (ui_image == null)
            {
                Debug.LogWarning($"{nameof(ui_image)} is not assigned to {nameof(ItemDragDisplay)} of {name}");
            }

            rect_transform = GetComponent<RectTransform>();
            gameObject.SetActive(false);
        }

        public void RecieveSlotDragged(IItemSlotDisplay slot_display, ItemObject item_object)
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