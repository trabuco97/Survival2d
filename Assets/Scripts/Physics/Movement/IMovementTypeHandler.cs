using UnityEngine;
using System.Collections;

namespace Survival2D.Physics.Movement
{
    public abstract class IMovementTypeHandler : MonoBehaviour
    {
        private bool active_state = false;
        public bool ActiveState 
        {
            get { return active_state; }
            set
            {
                active_state = value;
                this.enabled = value;
            }
        }
    }
}