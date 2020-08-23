using UnityEngine;
using System.Collections;

namespace Survival2D.Entities
{
    public class AwarenessBehaviour : MonoBehaviour
    {
        [SerializeField] private bool has_custom_values = false;
        [SerializeField] private EntityBehaviour[] custom_entities_aware = null;

        public EntityBehaviour[] EntitiesAware { get { return custom_entities_aware; } }

        // Use this for initialization
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {

        }
    }
}