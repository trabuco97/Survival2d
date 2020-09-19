using System;
using UnityEngine;

namespace Survival2D.Entities
{
    public delegate void EntityMethods(EntityEventArgs args);

    public class EntityEventArgs : EventArgs
    {
        public GameObject EntityObject { get; private set; }
        public EntityBehaviour Entity { get; private set; }

        public EntityEventArgs(GameObject entity_object)
        {
            EntityObject = entity_object;
        }

        public EntityEventArgs(EntityBehaviour entity_behaviour)
        {
            Entity = entity_behaviour;
        }
    }

}