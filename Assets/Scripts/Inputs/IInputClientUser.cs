using UnityEngine;
using System.Collections;

namespace Survival2D.Input
{
    // Workaround, maybe change how input is accesed
    public abstract class IInputClientUser : MonoBehaviour
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