using System;
using UnityEngine;

using Survival2D.Systems.Item.Inventory;
using Survival2D.Systems.Item.Inventory.Hotbar;

namespace Survival2D.Systems.Tools
{
    public class ToolItemBuffer_FromHotbar : MonoBehaviour
    {
        [SerializeField] private HotbarSystemBehaviour hotbar_behaviour = null;
        [SerializeField] private ToolSystemBehaviour tool_behaviour = null;

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

            hotbar_behaviour.OnHotbarInicialized += AddCallbacks;
        }

        private void OnDestroy()
        {
            hotbar_behaviour.OnHotbarInicialized -= AddCallbacks;
            hotbar_behaviour.Hotbar.OnHotbarSlotSelected -= HotbarCallbackToBuffer;
        }

        private void AddCallbacks(object e, EventArgs args)
        {
            hotbar_behaviour.Hotbar.OnHotbarSlotSelected += HotbarCallbackToBuffer;
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
    }
}