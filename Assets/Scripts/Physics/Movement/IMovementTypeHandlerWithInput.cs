using UnityEngine;
using System.Collections;

using Survival2D.Input;

namespace Survival2D.Physics.Movement
{
    public abstract class IMovementTypeHandlerWithInput : IMovementTypeHandler
    {
        protected InputClientManager manager = null;

        protected virtual void Start()
        {
            manager = InputClientManager.Instance;
            if (manager.IsClientInicialized)
            {
                SetupCallbacks();
            }
        }

        protected abstract void SetupCallbacks();
    }
}