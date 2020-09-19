using UnityEngine;
using System.Collections;

namespace Survival2D.Systems.HealthArmor
{
    public class HealthArmorSystemData
    {
        public float Health { get; private set; }

        public HealthArmorSystemData(float health)
        {
            Health = health;
        }
    }
}