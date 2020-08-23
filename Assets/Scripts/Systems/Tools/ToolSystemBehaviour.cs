using System;
using System.Collections.Generic;
using UnityEngine;

using IUnifiedContainerBase;
using Survival2D.Systems.Item;

using Survival2D.Systems.Statistics.Status;
using UnityEngine.Events;

namespace Survival2D.Systems.Tools
{
    // TODO: maybe make that a same item type can have different handlers and behaviour depenending on certain data of the item

    public class ToolSystemBehaviour : ISystemBehaviourWithStatus
    {
        #region WRAPPERS
        [Serializable]
        private class DatabaseWrapper
        {
            public ItemType tool_type = ItemType.MAX_TYPES;
            public List<ToolBehaviourWrapper> tool_wrapper_list = null; // it is used as a list because it might handle tool of the same type differently (at the moment SIZE = 1)
        }

        [Serializable]
        public class IToolContainerWrapper : IUnifiedContainer<IToolBehaviour> { }

        [Serializable]
        private class ToolBehaviourWrapper
        {
            public IToolActionHandler toolaction_handler = null;
            public IToolContainerWrapper tool_behaviour = null;
        }
        #endregion

        [SerializeField] private DatabaseWrapper[] tool_behaviour_draggable = null;

        private Dictionary<ItemType, List<ToolBehaviourWrapper>> tool_behaviour_database = null;
        private ToolBehaviourWrapper current_wrapper = null;


        public override SystemType SystemType => SystemType.Tool;

        public override event EventHandler OnSystemInicialized;

        private void Start()
        {
            InicializedSystem();
            OnSystemInicialized.Invoke(this, EventArgs.Empty);
        }

        public void ActivateWrapper(IToolItemObject tool_object)
        {
            if (tool_behaviour_database.TryGetValue(tool_object.ToolType, out var wrapper_list))
            {
                wrapper_list[0].tool_behaviour.Result.OnToolActivated(tool_object);

                current_wrapper = wrapper_list[0];   // TODO, rework this to support above said
                current_wrapper.toolaction_handler.ActivateHandler(tool_object);
            }
        }

        public void DeactivateWrapper()
        {
            if (current_wrapper != null)
            {
                current_wrapper.toolaction_handler.DeactivateHandler();
                current_wrapper.tool_behaviour.Result.OnToolDeactivated();
            }

            current_wrapper = null;
        }

        public void ExecuteAction(ToolActionData action_data)
        {
            current_wrapper.tool_behaviour.Result.ExecuteAction(action_data.action_index);
        }

        private void InicializedSystem()
        {
            tool_behaviour_database = new Dictionary<ItemType, List<ToolBehaviourWrapper>>();
            foreach (var wrapper in tool_behaviour_draggable)
            {
                tool_behaviour_database.Add(wrapper.tool_type, wrapper.tool_wrapper_list);

                foreach (var wrapper_tool in wrapper.tool_wrapper_list)
                {
                    var tool_behaviour = wrapper_tool.tool_behaviour.Result;
                    if (!tool_behaviour.IsInicialized)
                    {
                        tool_behaviour.InicializeBehaviour();
                    }
                }
            }
        }

        public override StatusLinkageToStat LinkModifierToStat(StatModifierData statModifier_data)
        {
            // TODO: 
            return null;
        }

        public override StatusLinkageToIncrementalStat LinkIncrementalModifierToStat(IncrementalStatModifierData statModifier_data)
        {
            return null;
        }
    }
}