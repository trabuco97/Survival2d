using UnityEngine;
using System.Collections;

using Survival2D.Systems.HealthArmor;

namespace Survival2D.Entities.AI.Consideration
{
    public class Debug1_Consideration : IConsideration
    {
        public ConsiderationType Type => ConsiderationType.DEBUG1;

        public float MinValue => 0;

        public float MaxValue => 1;

        public Appraisal Evaluate(AIGameContext context, AIDataNode data_node)
        {
            var health_system_behaviour = context.current_entity.GetComponentInChildren<HealthArmorSystemBehaviour>();
            var current_health = health_system_behaviour.HealthSystem.Health.ActualValue;
            var total_health = health_system_behaviour.HealthSystem.Health.ActualValue;

            var input = current_health / total_health;

            var appraisal_value = CurveResponseCalculator.GetValueFromEquation(data_node, input);
            return new Appraisal { base_value = appraisal_value, veto = false };
        }

    }
}