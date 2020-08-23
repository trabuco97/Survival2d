using UnityEngine;
using System.Collections;

using Survival2D.Systems.Item;

namespace Survival2D.Systems.Tools
{
    public class ToolActionData
    {
        public IToolItemObject tool_object;
        public int action_index;
    }

    // the implemented handler knows how to handle its type tool or different kinds of the same type
    public abstract class IToolActionHandler : MonoBehaviour
    {
        [SerializeField] private ToolSystemBehaviour tool_system = null;
        [SerializeField] private ItemType handler_type = ItemType.MAX_TYPES;
        private IToolItemObject tool_toHandle;

        protected bool is_handle_active = false;

        public ItemType HandlerType { get { return handler_type; } }

        protected virtual void Awake()
        {
#if UNITY_EDITOR
            if (tool_system == null)
            {
                Debug.LogWarning($"{nameof(tool_system)} is not assigned to {nameof(IToolActionHandler)} of {gameObject.GetFullName()}");
            }
#endif
        }

        public virtual void ActivateHandler(IToolItemObject tool_object)
        {
            tool_toHandle = tool_object;
            is_handle_active = true;
        }
        public virtual void DeactivateHandler()
        {
            is_handle_active = false;
        }


        protected void SendData(int action_number)
        {
            var info = new ToolActionData { tool_object = tool_toHandle, action_index = action_number };
            tool_system.ExecuteAction(info);
        }
    }
}