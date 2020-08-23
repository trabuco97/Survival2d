using System;
using System.Collections.ObjectModel;
using System.Collections.Generic;

namespace Survival2D.Entities.AI
{
    public enum ActionSelectionMode { Highest, Weight_Random }

    public class AgentAI
    {
        private const uint ARBRITARY_VALUE_TODETERMINEHOWMANYACTIONS_TOTAKE = 3;                        // change that pls
        private const float ARBRITARY_VALUE_TODETERMINETHERANGEOFRANDOMWEIGHTADDEDTOTHESCORE = 0.05f;   // also change this pls

        private List<ConsiderationObject> consideration_container = new List<ConsiderationObject>();
        private List<IAction> actions_to_perform = null;

        private ActionSelectionMode mode;

        public ReadOnlyCollection<IAction> ActionsPerformed { get { return actions_to_perform.AsReadOnly(); } }
        public event AIMethods OnActionUpdate;


        public AgentAI(IAction[] actions_toPerform, ActionSelectionMode mode)
        {
            this.mode = mode;
            actions_to_perform = new List<IAction>(actions_toPerform);

            foreach (var action in actions_to_perform)
            {
                foreach (var consideration_object in action.ConsiderationArray)
                {
                    if (!consideration_container.Contains(consideration_object))
                    {
                        consideration_container.Add(consideration_object);
                    }
                }
            }
        }



        public void RecalculateActionToPerform(AIGameContext context)
        {
            foreach (var consideration in consideration_container)
            {
                consideration.IsDirty = true;
            }

            if (mode == ActionSelectionMode.Highest)
            {
                float highest_score = -1f;
                IAction highest = null;
                foreach (var action in actions_to_perform)
                {
                    float value = action.CalculateUtilityValue(context);
                    if (value > highest_score)
                    {
                        highest_score = value;
                        highest = action;
                    }
                }


                highest.Execute();
            }
            else
            {
                float[] highest_score_array = new float[ARBRITARY_VALUE_TODETERMINEHOWMANYACTIONS_TOTAKE];
                IAction[] highest_array = new IAction[ARBRITARY_VALUE_TODETERMINEHOWMANYACTIONS_TOTAKE];
                foreach (var action in actions_to_perform)
                {
                    float value = action.CalculateUtilityValue(context);
                    for (int i = 0; i < ARBRITARY_VALUE_TODETERMINEHOWMANYACTIONS_TOTAKE; i++)
                    {
                        if (value > highest_score_array[i])
                        {
                            highest_score_array[i] = value;
                            highest_array[i] = action;

                            break;
                        }
                    }
                }

                float highest_score = -1f;
                int highest_index = 0;
                for (int i = 0; i < ARBRITARY_VALUE_TODETERMINEHOWMANYACTIONS_TOTAKE; i++)
                {
                    float random_value = UnityEngine.Random.Range(-ARBRITARY_VALUE_TODETERMINETHERANGEOFRANDOMWEIGHTADDEDTOTHESCORE, ARBRITARY_VALUE_TODETERMINETHERANGEOFRANDOMWEIGHTADDEDTOTHESCORE);
                    float old_value = highest_score_array[i];
                    float new_value = old_value + old_value * random_value;

                    if (new_value > highest_score)
                    {
                        highest_score = old_value;
                        highest_index = i;
                    }
                }

                highest_array[highest_index]?.Execute();
            }
        }

    }
}