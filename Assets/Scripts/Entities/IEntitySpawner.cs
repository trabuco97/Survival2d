using UnityEngine;
using System.Collections;

namespace Survival2D.Entities
{
    public abstract class IEntitySpawner : MonoBehaviour
    {
        [SerializeField] private EntityTrackerManager tracker_manager = null;

        protected virtual void Awake()
        {
#if UNITY_EDITOR
            if (tracker_manager == null)
            {
                Debug.LogWarning($"{nameof(tracker_manager)} is not assigned to {nameof(IEntitySpawner)} of {gameObject.GetFullName()}");
            }
#endif
        }

        protected abstract GameObject GetEntitySpawner();

        public EntityBehaviour SpawnEntity()
        {
            var entity_spawned = GetEntitySpawner();
            var entity_behaviour = entity_spawned.GetComponent<EntityBehaviour>();

            if (entity_behaviour != null)
            {
                if (!tracker_manager.TryAddEntity(entity_behaviour))
                {
                    entity_behaviour.ForceDespawn();
                }
                else
                {
                    return entity_behaviour;
                }
            }
#if UNITY_EDITOR
            else
            {
                Debug.Log($"Behaviour {this.GetType()} doesnt have its entities spawned implement entitybehavior");
            }
#endif

            return null;
        }
    }
}