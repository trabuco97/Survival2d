using System;
using UnityEngine;

namespace Survival2D.Entities
{
    public delegate void EntityMethods(EntityEventArgs args);

    public class EntityEventArgs : EventArgs
    {
        public GameObject EntityObject { get; private set; }

        public EntityEventArgs(GameObject entity_object)
        {
            EntityObject = entity_object;
        }
    }

}