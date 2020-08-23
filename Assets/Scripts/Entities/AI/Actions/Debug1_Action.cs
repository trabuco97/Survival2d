using UnityEngine;
using System.Collections;

namespace Survival2D.Entities.AI.Action
{
    public class Debug1_Action : IAction
    {
        public Debug1_Action(ConsiderationObject[] consideration_objects, float weight) : base(consideration_objects, weight)
        {
        }

        public override void Execute()
        {
            Debug.Log("Executing Debug action");
        }
    }
}