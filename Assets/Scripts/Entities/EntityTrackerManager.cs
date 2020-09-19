using System.Collections.Generic;
using UnityEngine;

namespace Survival2D.Entities
{
    public class EntityTrackerManager : MonoBehaviour
    {
        [SerializeField] private EntityBehaviour[] custom_entities_atAwake = null;
        [SerializeField] private uint max_entities_spawned = 5;

        private List<EntityBehaviour> entity_behaviour_container = new List<EntityBehaviour>();


        private void Awake()
        {
            entity_behaviour_container = new List<EntityBehaviour>((int)max_entities_spawned);
            if (custom_entities_atAwake != null)
            {
                int i = 0;
                while (i < custom_entities_atAwake.Length && TryAddEntity(custom_entities_atAwake[i]))
                {
                    i++;
                }
            }
        }


        public bool TryAddEntity(EntityBehaviour entity)
        {
            if (entity_behaviour_container.Count == max_entities_spawned) return false;

            entity_behaviour_container.Add(entity);
            return true;
        }
    }
}