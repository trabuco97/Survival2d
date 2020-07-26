using UnityEngine;
using UnityEngine.Events;

namespace Survival2D.Entities
{
    public class EntityBehaviour : MonoBehaviour
    {
        public EntityEvent onDespawn { get; } = new EntityEvent();

        private void OnDisable()
        {
            onDespawn.Invoke(this.gameObject);
        }
    }
}