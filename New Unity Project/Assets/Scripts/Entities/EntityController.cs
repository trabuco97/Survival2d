using UnityEngine;
using System.Collections;

using Survival2D.Entities.Stats;

namespace Survival2D.Entities
{
    public class EntityController : MonoBehaviour
    {
        [SerializeField] private EntityBaseStats base_stats = null;
        public EntityBaseStats BaseStats { get { return base_stats; } }


        private void Awake()
        {
            if (base_stats = null)
            {
                Debug.LogWarning($"{nameof(base_stats)} is not assigned to {nameof(EntityController)} of {name}");
            }
        }

        private void Start()
        {
            
        }
    }
}