using UnityEngine.InputSystem;

using Survival2D.Input;

namespace Survival2D.Systems.Tools
{
    public abstract class IToolActionHandler_ByInput : IToolActionHandler
    {
        protected InputClientManager manager = null;
        protected InputActionMap current_toolActionMap = null;

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