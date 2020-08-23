using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

using Survival2D.Systems.Item.Inventory;

using TMPro;
using System;

namespace Survival2D.UI.Item.Inventory.Hotbar
{
    public class UI_HotbarSlot : UI_IItemSlot
    {
        [SerializeField] private TMP_Text stack_display = null;

        protected override void Awake()
        {
            base.Awake();

#if UNITY_EDITOR
            if (stack_display == null)
            {
                Debug.LogWarning($"{nameof(stack_display)} is not assigned to {nameof(UI_HotbarSlot)} of {gameObject.GetFullName()}");
            }
#endif
        }

        internal void InicializedNonSelected()
        {
            ui_background.color = Color.white;
        }

        internal void InitializeSelected()
        {
            ui_background.color = Color.green;
        }
    }
}