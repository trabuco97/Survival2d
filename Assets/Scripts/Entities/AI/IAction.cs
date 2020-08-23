#if UNITY_EDITOR
using UnityEngine;
#endif
using System.Collections.Generic;

namespace Survival2D.Entities.AI
{
    public abstract class IAction 
    {
        private class ConsiderationObjectWrapper
        {
            public ConsiderationObject Object;
            public List<ConsiderationType> DiscardTypeList;
        }

        public float weight = 0f;
        private Queue<ConsiderationObject> consideration_queue_calculation = null;


        public ConsiderationObject[] ConsiderationArray 
        { 
            get 
            { 
                ConsiderationObject[] output = consideration_queue_calculation.ToArray();
                return output;
            } 
        }
        public ActionType Type { get; private set; }

        public IAction(ConsiderationObject[] consideration_objects, float weight, ActionType type)
        {
            this.weight = weight;
            this.Type = type;
            consideration_queue_calculation = GetLayeredConsiderationStack(consideration_objects);
        }


        public float CalculateUtilityValue(AIGameContext context)
        {
            float initial_value = 1;


            foreach (var consideration in consideration_queue_calculation)
            {

                var appaisal = consideration.GetCurrentApprasial(context);
                if (appaisal.veto) return 0.0f;

                initial_value *= appaisal.base_value;
            }

            return initial_value * weight;
        }

        public abstract void Execute();


        private static Queue<ConsiderationObject> GetLayeredConsiderationStack(ConsiderationObject[] consideration_objects)
        {
            var output = new Queue<ConsiderationObject>();
            var consideration_list = new List<ConsiderationObjectWrapper>(consideration_objects.Length);

            // Generate the list of wrapper
            for (int i = 0; i < consideration_objects.Length; i++)
            {
                consideration_list.Add(new ConsiderationObjectWrapper { Object = consideration_objects[i], DiscardTypeList = new List<ConsiderationType>(consideration_objects[i].consideration_dependencies) });
            }

            List<ConsiderationObjectWrapper> previous_layer = new List<ConsiderationObjectWrapper>();
            List<ConsiderationObjectWrapper> aux;

            while (consideration_list.Count > 0)
            {
                bool has_any_dependency = false;
                aux = new List<ConsiderationObjectWrapper>();

                int i = 0;
                while (i < consideration_list.Count)
                {
                    // first, eliminate all the types that were in the previous layer
                    foreach (var previous_cons in previous_layer)
                    {
                        has_any_dependency = has_any_dependency || consideration_list[i].DiscardTypeList.Remove(previous_cons.Object.Type);
                    }


                    // evaluates the discardtypelist
                    // if empty:    - can be included in the current layer and discarded from the list
                    // if no empty: - stays in the list

                    if (consideration_list[i].DiscardTypeList.Count == 0)
                    {
                        aux.Add(consideration_list[i]);
                        consideration_list.RemoveAt(i);
                    }
                    else
                    {

                        i++;
                    }

                    // add the current layer to the queue
                    foreach (var current_cons in aux)
                    {
                        output.Enqueue(current_cons.Object);
                    }

                    // Check if that all consideration has its dependencies assigned
                    // ERROR:   return null
                    if (!has_any_dependency && consideration_list.Count > 0)
                        return null;

                    previous_layer = aux;
                }
            }


            return output;
        }
    }
}