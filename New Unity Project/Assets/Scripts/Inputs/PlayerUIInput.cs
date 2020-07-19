// GENERATED AUTOMATICALLY FROM 'Assets/Resources/ActionsMaps/PlayerUIInput.inputactions'

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Utilities;

namespace Survival2D.Input
{
    public class @PlayerUIInput : IInputActionCollection, IDisposable
    {
        public InputActionAsset asset { get; }
        public @PlayerUIInput()
        {
            asset = InputActionAsset.FromJson(@"{
    ""name"": ""PlayerUIInput"",
    ""maps"": [
        {
            ""name"": ""ConsoleChat"",
            ""id"": ""a37e8e3a-e3a9-4890-948b-33c79d383887"",
            ""actions"": [
                {
                    ""name"": ""ToogleChat"",
                    ""type"": ""Button"",
                    ""id"": ""59c60c18-bf84-4ebf-a1ad-3b11b69ead59"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""ToogleAutoTab"",
                    ""type"": ""Button"",
                    ""id"": ""d1a6ccec-7d67-4338-a6fc-859eed882402"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""AcceptAutoTab"",
                    ""type"": ""Button"",
                    ""id"": ""e016517d-39c9-4ef7-b49d-6d3e7c264715"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""AcceptInput"",
                    ""type"": ""Button"",
                    ""id"": ""e371021d-7934-4dd5-9e55-3e52a0b8691e"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                }
            ],
            ""bindings"": [
                {
                    ""name"": """",
                    ""id"": ""7869eca0-8e08-430a-b708-ded17b487cef"",
                    ""path"": ""<Keyboard>/comma"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""ToogleChat"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""24bdefe5-1a0d-43cc-8586-31dcd4d22470"",
                    ""path"": ""<Keyboard>/tab"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""ToogleAutoTab"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""84b06c61-1843-4033-bd23-e0dac0e59ad4"",
                    ""path"": ""<Keyboard>/enter"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""AcceptAutoTab"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""21ae4b08-ac5c-41d9-9461-3eb2150bae41"",
                    ""path"": ""<Keyboard>/enter"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""AcceptInput"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        },
        {
            ""name"": ""Inventory"",
            ""id"": ""c5cc5bd5-53a5-4694-a703-0aa67a10defb"",
            ""actions"": [
                {
                    ""name"": ""ToogleView"",
                    ""type"": ""Button"",
                    ""id"": ""e81e788c-f528-408f-9665-6d691ec71878"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                }
            ],
            ""bindings"": [
                {
                    ""name"": """",
                    ""id"": ""eca0fdc2-ebad-4dc3-a71c-e22d97898679"",
                    ""path"": ""<Keyboard>/e"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""ToogleView"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        }
    ],
    ""controlSchemes"": []
}");
            // ConsoleChat
            m_ConsoleChat = asset.FindActionMap("ConsoleChat", throwIfNotFound: true);
            m_ConsoleChat_ToogleChat = m_ConsoleChat.FindAction("ToogleChat", throwIfNotFound: true);
            m_ConsoleChat_ToogleAutoTab = m_ConsoleChat.FindAction("ToogleAutoTab", throwIfNotFound: true);
            m_ConsoleChat_AcceptAutoTab = m_ConsoleChat.FindAction("AcceptAutoTab", throwIfNotFound: true);
            m_ConsoleChat_AcceptInput = m_ConsoleChat.FindAction("AcceptInput", throwIfNotFound: true);
            // Inventory
            m_Inventory = asset.FindActionMap("Inventory", throwIfNotFound: true);
            m_Inventory_ToogleView = m_Inventory.FindAction("ToogleView", throwIfNotFound: true);
        }

        public void Dispose()
        {
            UnityEngine.Object.Destroy(asset);
        }

        public InputBinding? bindingMask
        {
            get => asset.bindingMask;
            set => asset.bindingMask = value;
        }

        public ReadOnlyArray<InputDevice>? devices
        {
            get => asset.devices;
            set => asset.devices = value;
        }

        public ReadOnlyArray<InputControlScheme> controlSchemes => asset.controlSchemes;

        public bool Contains(InputAction action)
        {
            return asset.Contains(action);
        }

        public IEnumerator<InputAction> GetEnumerator()
        {
            return asset.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public void Enable()
        {
            asset.Enable();
        }

        public void Disable()
        {
            asset.Disable();
        }

        // ConsoleChat
        private readonly InputActionMap m_ConsoleChat;
        private IConsoleChatActions m_ConsoleChatActionsCallbackInterface;
        private readonly InputAction m_ConsoleChat_ToogleChat;
        private readonly InputAction m_ConsoleChat_ToogleAutoTab;
        private readonly InputAction m_ConsoleChat_AcceptAutoTab;
        private readonly InputAction m_ConsoleChat_AcceptInput;
        public struct ConsoleChatActions
        {
            private @PlayerUIInput m_Wrapper;
            public ConsoleChatActions(@PlayerUIInput wrapper) { m_Wrapper = wrapper; }
            public InputAction @ToogleChat => m_Wrapper.m_ConsoleChat_ToogleChat;
            public InputAction @ToogleAutoTab => m_Wrapper.m_ConsoleChat_ToogleAutoTab;
            public InputAction @AcceptAutoTab => m_Wrapper.m_ConsoleChat_AcceptAutoTab;
            public InputAction @AcceptInput => m_Wrapper.m_ConsoleChat_AcceptInput;
            public InputActionMap Get() { return m_Wrapper.m_ConsoleChat; }
            public void Enable() { Get().Enable(); }
            public void Disable() { Get().Disable(); }
            public bool enabled => Get().enabled;
            public static implicit operator InputActionMap(ConsoleChatActions set) { return set.Get(); }
            public void SetCallbacks(IConsoleChatActions instance)
            {
                if (m_Wrapper.m_ConsoleChatActionsCallbackInterface != null)
                {
                    @ToogleChat.started -= m_Wrapper.m_ConsoleChatActionsCallbackInterface.OnToogleChat;
                    @ToogleChat.performed -= m_Wrapper.m_ConsoleChatActionsCallbackInterface.OnToogleChat;
                    @ToogleChat.canceled -= m_Wrapper.m_ConsoleChatActionsCallbackInterface.OnToogleChat;
                    @ToogleAutoTab.started -= m_Wrapper.m_ConsoleChatActionsCallbackInterface.OnToogleAutoTab;
                    @ToogleAutoTab.performed -= m_Wrapper.m_ConsoleChatActionsCallbackInterface.OnToogleAutoTab;
                    @ToogleAutoTab.canceled -= m_Wrapper.m_ConsoleChatActionsCallbackInterface.OnToogleAutoTab;
                    @AcceptAutoTab.started -= m_Wrapper.m_ConsoleChatActionsCallbackInterface.OnAcceptAutoTab;
                    @AcceptAutoTab.performed -= m_Wrapper.m_ConsoleChatActionsCallbackInterface.OnAcceptAutoTab;
                    @AcceptAutoTab.canceled -= m_Wrapper.m_ConsoleChatActionsCallbackInterface.OnAcceptAutoTab;
                    @AcceptInput.started -= m_Wrapper.m_ConsoleChatActionsCallbackInterface.OnAcceptInput;
                    @AcceptInput.performed -= m_Wrapper.m_ConsoleChatActionsCallbackInterface.OnAcceptInput;
                    @AcceptInput.canceled -= m_Wrapper.m_ConsoleChatActionsCallbackInterface.OnAcceptInput;
                }
                m_Wrapper.m_ConsoleChatActionsCallbackInterface = instance;
                if (instance != null)
                {
                    @ToogleChat.started += instance.OnToogleChat;
                    @ToogleChat.performed += instance.OnToogleChat;
                    @ToogleChat.canceled += instance.OnToogleChat;
                    @ToogleAutoTab.started += instance.OnToogleAutoTab;
                    @ToogleAutoTab.performed += instance.OnToogleAutoTab;
                    @ToogleAutoTab.canceled += instance.OnToogleAutoTab;
                    @AcceptAutoTab.started += instance.OnAcceptAutoTab;
                    @AcceptAutoTab.performed += instance.OnAcceptAutoTab;
                    @AcceptAutoTab.canceled += instance.OnAcceptAutoTab;
                    @AcceptInput.started += instance.OnAcceptInput;
                    @AcceptInput.performed += instance.OnAcceptInput;
                    @AcceptInput.canceled += instance.OnAcceptInput;
                }
            }
        }
        public ConsoleChatActions @ConsoleChat => new ConsoleChatActions(this);

        // Inventory
        private readonly InputActionMap m_Inventory;
        private IInventoryActions m_InventoryActionsCallbackInterface;
        private readonly InputAction m_Inventory_ToogleView;
        public struct InventoryActions
        {
            private @PlayerUIInput m_Wrapper;
            public InventoryActions(@PlayerUIInput wrapper) { m_Wrapper = wrapper; }
            public InputAction @ToogleView => m_Wrapper.m_Inventory_ToogleView;
            public InputActionMap Get() { return m_Wrapper.m_Inventory; }
            public void Enable() { Get().Enable(); }
            public void Disable() { Get().Disable(); }
            public bool enabled => Get().enabled;
            public static implicit operator InputActionMap(InventoryActions set) { return set.Get(); }
            public void SetCallbacks(IInventoryActions instance)
            {
                if (m_Wrapper.m_InventoryActionsCallbackInterface != null)
                {
                    @ToogleView.started -= m_Wrapper.m_InventoryActionsCallbackInterface.OnToogleView;
                    @ToogleView.performed -= m_Wrapper.m_InventoryActionsCallbackInterface.OnToogleView;
                    @ToogleView.canceled -= m_Wrapper.m_InventoryActionsCallbackInterface.OnToogleView;
                }
                m_Wrapper.m_InventoryActionsCallbackInterface = instance;
                if (instance != null)
                {
                    @ToogleView.started += instance.OnToogleView;
                    @ToogleView.performed += instance.OnToogleView;
                    @ToogleView.canceled += instance.OnToogleView;
                }
            }
        }
        public InventoryActions @Inventory => new InventoryActions(this);
        public interface IConsoleChatActions
        {
            void OnToogleChat(InputAction.CallbackContext context);
            void OnToogleAutoTab(InputAction.CallbackContext context);
            void OnAcceptAutoTab(InputAction.CallbackContext context);
            void OnAcceptInput(InputAction.CallbackContext context);
        }
        public interface IInventoryActions
        {
            void OnToogleView(InputAction.CallbackContext context);
        }
    }
}
