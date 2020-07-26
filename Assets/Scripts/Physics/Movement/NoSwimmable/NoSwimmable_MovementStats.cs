using UnityEngine;
using System.Collections;

using Survival2D.Systems.Statistics;

namespace Survival2D.Physics.Movement.NoSwimmable
{
    public class NoSwimmable_MovementStats : IMovementStats
    {
        public Stat horizontal_grounded_speed;
        public Stat horizontal_grounded_acceleration;
        public Stat swim_speed;
        public Stat horizontal_swim_acceleration;

        // define the magnitude of the velocity vector when jumping
        public Stat jump_potency;

    }
}