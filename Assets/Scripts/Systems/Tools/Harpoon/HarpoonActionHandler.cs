using UnityEngine;
using System.Collections;

namespace Survival2D.Systems.Tools.Harpoon
{
    public class HarpoonActionHandler : IToolActionHandler_ByInput
    {
        protected override void SetupCallbacks()
        {
            var tool_map = manager.CurrentClient.GameplayInput.ToolUsage;
            tool_map.Primary.performed += (ctx) =>
            {
                if (is_handle_active) SendData(0);
            };

            tool_map.Secondary.performed += (ctx) =>
            {
                if (is_handle_active) SendData(1);
            };
        }
    }
}