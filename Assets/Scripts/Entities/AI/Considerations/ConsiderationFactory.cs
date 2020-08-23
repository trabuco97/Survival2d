using UnityEngine;
using System.Collections.Generic;

using Survival2D.Entities.AI.Consideration;

namespace Survival2D.Entities.AI
{
    public static class ConsiderationFactory
    {
        public static Dictionary<ConsiderationType, IConsideration> consideration_database = new Dictionary<ConsiderationType, IConsideration>();

        public static IConsideration GetConsiderationType(ConsiderationType type)
        {
            if (consideration_database.TryGetValue(type, out var consideration))
            {
                return consideration;
            }
            else
            {
                IConsideration output = null;
                switch (type)
                {
                    case ConsiderationType.DEBUG1:
                        output = new Debug1_Consideration();
                        break;
                }



                consideration_database.Add(type, output);
                return output;
            }
        }


    }
}