using UnityEngine;
using System.Collections;

namespace Survival2D.Entities.Stats
{
    [CreateAssetMenu(fileName = "New Player base stats", menuName = "CustomData/PlayerBaseStats")]
    public class PlayerBaseStats : EntityBaseStats
    {
        [Header("Player Base Stats")]
        public int radiation_base_tolerance;
        public int radiation_death_base;
        public int respiration_decreaserate_base;
        public int respiration_capacity_base;
        public int hunger_decreaserate_base;
        public int hunger_capacity_base;
    }
}