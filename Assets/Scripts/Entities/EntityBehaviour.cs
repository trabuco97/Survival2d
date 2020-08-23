using UnityEngine;
using UnityEngine.Events;

namespace Survival2D.Entities
{
    public class EntityBehaviour : MonoBehaviour
    {
        public event EntityMethods OnDespawn;


        private void OnDisable()
        {
            OnDespawn?.Invoke(new EntityEventArgs(this.gameObject));
        }
    }
}