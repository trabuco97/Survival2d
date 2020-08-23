#if UNITY_EDITOR
using UnityEngine;
#endif
using System.Collections.Generic;



namespace Survival2D.Entities.AI
{
    public static class ScriptableAIProcessor
    {
        public static AgentAI ProcessScriptableAI(Scriptable_AnimalAI scriptable)
        {
            var action_list = new List<IAction>();
            List<ConsiderationObject> consideration_obj_list;

            foreach (var scp_actions in scriptable.actions)
            {
                consideration_obj_list = new List<ConsiderationObject>();

                foreach (var scp_consideration in scp_actions.consideration_container)
                {
                    var cons = ConsiderationFactory.GetConsiderationType(scp_consideration.type);
                    var cons_object = new ConsiderationObject(cons);
                    AIDataNode data_node;

                    if (scp_consideration.mode == ParameterMode.CustomCurve)
                    {
                        data_node = new AIDataNode(scp_consideration.mode, scp_consideration.utility_curve);
                    }
                    else
                    {
                        data_node = new AIDataNode(scp_consideration.mode, scp_consideration.aditional_values);
                    }

                    cons_object.SetDataNode(data_node);
                    consideration_obj_list.Add(cons_object);
                }


                var new_action = ActionFactory.GetActionType(scp_actions.type, consideration_obj_list.ToArray(), scp_actions.weight);
                action_list.Add(new_action);
            }


            var agent = new AgentAI(action_list.ToArray(), ActionSelectionMode.Highest);
            return agent;
        }
    }
}