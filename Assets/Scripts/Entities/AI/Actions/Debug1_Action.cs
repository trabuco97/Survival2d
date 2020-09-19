using UnityEngine;
using System.Collections;

namespace Survival2D.Entities.AI.Action
{
    public class Debug1_Action : IAction
    {
        public Debug1_Action(ConsiderationObject[] consideration_objects, float weight, ActionType type) : base(consideration_objects, weight, type)
        {
        }

        public override void Execute()
        {
            Debug.Log("Executing Debug action");
        }
    }
}