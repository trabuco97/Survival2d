using UnityEngine;
using System.Collections;

namespace Survival2D.Systems.HealthArmor
{
    public static class HealthSystemCalculator
    {
        public static float GetArmorRatingLoss(float base_armor_rating_loss, float damage_recieved)
        {
            float output = base_armor_rating_loss;

            return output;
        }

        public static float GetDamageDealtReduction(float damage_value, float armor_value)
        {
            float reduction = damage_value * armor_value / 100;
            return damage_value - reduction;
        }

    }
}