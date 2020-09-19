using System;
using UnityEngine;

using Survival2D.Systems.Item.Inventory;
using Survival2D.Systems.Item.Inventory.Hotbar;

namespace Survival2D.Systems.Tools
{
    public class ToolItemBuffer_FromHotbar : MonoBehaviour, IOrderedBehaviour
    {
        [SerializeField] private HotbarSystemBehaviour hotbar_behaviour = null;
        [SerializeField] private ToolSystemBehaviour tool_behaviour = null;

        private bool is_initialized = false;

        public int Order => 4;

        private void Awake()
        {
#if UNITY_EDITOR
            if (hotbar_behaviour == null)
            {
                Debug.LogWarning($"{nameof(hotbar_behaviour)} is not assigned to {nameof(ToolItemBuffer_FromHotbar)} of {gameObject.GetFullName()}");
            }

            if (tool_behaviour == null)
            {
                Debug.LogWarning($"{nameof(tool_behaviour)} is not assigned to {nameof(ToolItemBuffer_FromHotbar)} of {gameObject.GetFullName()}");
            }
#endif

        }

        private void OnDisable()
        {
            hotbar_behaviour.Hotbar.OnHotbarSlotSelected -= HotbarCallbackToBuffer;
        }

        private void OnEnable()
        {
            if (is_initialized)
            {
                hotbar_behaviour.Hotbar.OnHotbarSlotSelected += HotbarCallbackToBuffer;
            }
        }

        private void HotbarCallbackToBuffer(InventoryEventArgs args)
        {
            tool_behaviour.DeactivateWrapper();
            IToolItemObject tool_object = args.SlotModified.ItemContained as IToolItemObject;

            if (tool_object != null)
            {
                tool_behaviour.ActivateWrapper(tool_object);
            }
        }

        public void Initialize()
        {
            hotbar_behaviour.Hotbar.OnHotbarSlotSelected += HotbarCallbackToBuffer;
            is_initialized = true;
        }
    }
}