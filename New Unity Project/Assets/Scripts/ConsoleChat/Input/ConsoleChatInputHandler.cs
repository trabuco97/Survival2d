using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

using ConsoleChat.UI;
using Survival2D.Input;

namespace ConsoleChat.Survival2D
{
    public class ConsoleChatInputHandler : MonoBehaviour
    {
        [SerializeField] private ChatConsoleDisplay consoleChat = null;
        private InputClientManager input_manager = null;

        private bool is_auto_tab_accepted = false;

        private void Awake()
        {
            if (consoleChat == null)
            {
                Debug.LogWarning($"{nameof(consoleChat)} is not assigned to {nameof(ConsoleChatInputHandler)} of {name}");
            }
        }

        private void Start()
        {
            input_manager = InputClientManager.Instance;
            SetupCallbacks();
        }

        private void Update()
        {
            if (is_auto_tab_accepted)
            {
                is_auto_tab_accepted = false;
            }
        }


        public void PerformToogleChat()
        {
            consoleChat.ToogleDisplay();

            bool other_inputs = !consoleChat.IsToogled;
            input_manager.CurrentClient.SetActionMapsState(other_inputs, "movement", "inventory");
        }

        public void PerformAutoTab()
        {
            if (!consoleChat.IsToogled) return;
            consoleChat.ToggleAutoTab();
        }

        public void PerformAcceptAutotTab()
        {
            if (!consoleChat.IsToogled) return;


            if (consoleChat.IsAutoToogled)
            {
                consoleChat.AcceptAutoTab();
                is_auto_tab_accepted = true;
            }
        }

        public void PerformAcceptInput()
        {
            if (!consoleChat.IsToogled) return;

            if (!consoleChat.IsAutoToogled && !is_auto_tab_accepted)
            {
                consoleChat.ProcessInput();
            }

        }

        private void SetupCallbacks()
        {
            var action_map = input_manager.CurrentClient.UIInput.ConsoleChat;

            action_map.ToogleChat.started += var => { PerformToogleChat(); };
            action_map.ToogleAutoTab.started += var => { PerformAutoTab(); };
            action_map.AcceptAutoTab.started += var => { PerformAcceptAutotTab(); };
            action_map.AcceptInput.started += var => { PerformAcceptInput(); };
        }
    }
}