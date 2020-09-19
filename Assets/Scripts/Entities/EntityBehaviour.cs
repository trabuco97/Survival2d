using UnityEngine;
using UnityEngine.Events;

namespace Survival2D.Entities
{
    public class EntityBehaviour : MonoBehaviour
    {
        [SerializeField] private OrderBehaviour order_container = null;

        public OrderBehaviour OrderBehaviour { get { return order_container; } }

        public event EntityMethods OnDespawn;

        public void ForceDespawn()
        {
            OnDespawn?.Invoke(new EntityEventArgs(this.gameObject));
        }

        private void OnDisable()
        {
        }
    }
}