using UnityEngine;
using System.Collections.Generic;

namespace Survival2D.Entities.AI
{
    public class ConsiderationObject
    {
        private IConsideration current;
        private AIDataNode current_data_node;
        private Appraisal current_apprasial = new Appraisal();


        public ConsiderationType Type { get { return current.Type; } }
        public bool IsDirty { get; set; } = true;
        public List<ConsiderationType> consideration_dependencies { get; private set; } = new List<ConsiderationType>();


        public ConsiderationObject(IConsideration current)
        {
            this.current = current;
        }

        public void SetDataNode(AIDataNode new_data_node)
        {
            current_data_node = new_data_node;
            current_data_node.SetMinMaxValues(current.MinValue, current.MaxValue);
        }

        public Appraisal GetCurrentApprasial(AIGameContext context)
        {
            if (IsDirty)
            {
                current_apprasial = current.Evaluate(context, current_data_node);
                IsDirty = false;
            }

            return current_apprasial;
        }
    }
}