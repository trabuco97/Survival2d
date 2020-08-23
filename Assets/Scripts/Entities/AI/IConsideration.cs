using UnityEngine;
using System.Collections;

namespace Survival2D.Entities.AI
{
    public interface IConsideration
    {
        float MinValue { get; }
        float MaxValue { get; }
        ConsiderationType Type { get; }

        Appraisal Evaluate(AIGameContext context, AIDataNode data); 
    }
}