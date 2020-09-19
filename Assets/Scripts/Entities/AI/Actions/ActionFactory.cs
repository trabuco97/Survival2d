using UnityEngine;
using System.Collections;

using Survival2D.Entities.AI.Action;

namespace Survival2D.Entities.AI
{
    public static class ActionFactory
    {
        public static IAction GetActionType(ActionType type, ConsiderationObject[] consideration_list, float weight)
        {
            IAction output = null;
            switch (type)
            {
                case ActionType.DEBUG1:
                    output = new Debug1_Action(consideration_list, weight, type);
                    break;

            }

            return output;
        }
    }
}