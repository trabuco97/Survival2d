using UnityEngine;
using UnityEngine.Events;

namespace Survival2D.Entities
{
    public class EntityBehaviour : MonoBehaviour
    {
        public EntityEvent onDespaawn { get; } = new EntityEvent();


        private void OnDisable()
        {
            onDespaawn.Invoke(this.gameObject);
        }
    }
}