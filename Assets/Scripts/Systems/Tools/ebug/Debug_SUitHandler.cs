//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;

//namespace Survival2D.Systems.Tools
//{
//    public class Debug_SUitHandler : IToolActionHandler_ByInput
//    {
//        protected override void SetupCallbacks()
//        {
//            var tool_map = manager.CurrentClient.GameplayInput.ToolUsage;
//            current_toolActionMap = tool_map;

//            tool_map.Primary.performed += (ctx) => { if (is_handle_active) SendData(0); };
//            tool_map.Secondary.performed += (ctx) => { if (is_handle_active) SendData(1); };
//        }
//    }
//}