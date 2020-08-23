using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

using ConsoleChat.UI;
using Survival2D.Input;

namespace ConsoleChat.Input
{
    public class ConsoleChatInputBehaviour : IInputClientUser
    {
        [SerializeField] private UI_ChatConsoleInputField consoleChat = null;

        private bool is_auto_tab_accepted = false;

        private void Awake()
        {
#if UNITY_EDITOR
            if (consoleChat == null)
            {
                Debug.LogWarning($"{nameof(consoleChat)} is not assigned to {nameof(ConsoleChatInputBehaviour)} of {name}");
            }
#endif
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
            manager.CurrentClient.SetInputTypeState(other_inputs, new InputType[] { InputType.Gameplay, InputType.UI }, CurrentActionMaps.ConsoleChat);
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

        protected override void SetupCallbacks()
        {
            var action_map = manager.CurrentClient.UIInput.ConsoleChat;

            action_map.ToogleChat.started += var => { PerformToogleChat(); };
            action_map.ToogleAutoTab.started += var => { PerformAutoTab(); };
            action_map.AcceptAutoTab.started += var => { PerformAcceptAutotTab(); };
            action_map.AcceptInput.started += var => { PerformAcceptInput(); };
        }
    }
}