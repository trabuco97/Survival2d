using UnityEngine;
using UnityEditor;

namespace Survival2D.Entities.Stats
{
    public abstract class EntityBaseStats : ScriptableObject
    {
        [Header("Entity Base Stats")]
        public int horizontal_velocity;
        public int swim_velocity;
        public int upward_velocity;
        public int downward_velocity;
        public int mass;
        public int health_base;

    }
}