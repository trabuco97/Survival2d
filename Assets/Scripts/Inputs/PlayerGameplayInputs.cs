// GENERATED AUTOMATICALLY FROM 'Assets/Resources/ActionsMaps/PlayerGameplayInput.inputactions'

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Utilities;

namespace Survival2D.Input
{
    public class @PlayerGameplayInput : IInputActionCollection, IDisposable
    {
        public InputActionAsset asset { get; }
        public @PlayerGameplayInput()
        {
            asset = InputActionAsset.FromJson(@"{
    ""name"": ""PlayerGameplayInput"",
    ""maps"": [
        {
            ""name"": ""NoSwimmable_Movement"",
            ""id"": ""9f516d21-13c5-442b-b5bd-c7a95fbc0b73"",
            ""actions"": [
                {
                    ""name"": ""Horizontal_Movement"",
                    ""type"": ""Value"",
                    ""id"": ""832d3940-c37e-4257-a19e-dd904d051c7c"",
                    ""expectedControlType"": ""Axis"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Jump"",
                    ""type"": ""Button"",
                    ""id"": ""5f866fb2-97ee-4e7c-a008-be70fed5ac5b"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                }
            ],
            ""bindings"": [
                {
                    ""name"": ""1D Axis"",
                    ""id"": ""4494459c-4dce-46c0-ada2-cfca2c848b54"",
                    ""path"": ""1DAxis"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Horizontal_Movement"",
                    ""isComposite"": true,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""negative"",
                    ""id"": ""f34bbd87-05ff-4d87-977d-e900d2a6090f"",
                    ""path"": ""<Keyboard>/a"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard&Mouse"",
                    ""action"": ""Horizontal_Movement"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""positive"",
                    ""id"": ""1b76c697-6b3f-4c65-9232-4014d99a5956"",
                    ""path"": ""<Keyboard>/d"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard&Mouse"",
                    ""action"": ""Horizontal_Movement"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": """",
                    ""id"": ""9362c578-1495-48bb-b560-5c9f2d7fcd34"",
                    ""path"": ""<Keyboard>/space"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard&Mouse"",
                    ""action"": ""Jump"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        }
    ],
    ""controlSchemes"": [
        {
            ""name"": ""Keyboard&Mouse"",
            ""bindingGroup"": ""Keyboard&Mouse"",
            ""devices"": [
                {
                    ""devicePath"": ""<Keyboard>"",
                    ""isOptional"": false,
                    ""isOR"": false
                },
                {
                    ""devicePath"": ""<Mouse>"",
                    ""isOptional"": false,
                    ""isOR"": false
                }
            ]
        },
        {
            ""name"": ""Gamepad"",
            ""bindingGroup"": ""Gamepad"",
            ""devices"": [
                {
                    ""devicePath"": ""<Gamepad>"",
                    ""isOptional"": false,
                    ""isOR"": false
                }
            ]
        },
        {
            ""name"": ""Touch"",
            ""bindingGroup"": ""Touch"",
            ""devices"": [
                {
                    ""devicePath"": ""<Touchscreen>"",
                    ""isOptional"": false,
                    ""isOR"": false
                }
            ]
        },
        {
            ""name"": ""Joystick"",
            ""bindingGroup"": ""Joystick"",
            ""devices"": [
                {
                    ""devicePath"": ""<Joystick>"",
                    ""isOptional"": false,
                    ""isOR"": false
                }
            ]
        },
        {
            ""name"": ""XR"",
            ""bindingGroup"": ""XR"",
            ""devices"": [
                {
                    ""devicePath"": ""<XRController>"",
                    ""isOptional"": false,
                    ""isOR"": false
                }
            ]
        }
    ]
}");
            // NoSwimmable_Movement
            m_NoSwimmable_Movement = asset.FindActionMap("NoSwimmable_Movement", throwIfNotFound: true);
            m_NoSwimmable_Movement_Horizontal_Movement = m_NoSwimmable_Movement.FindAction("Horizontal_Movement", throwIfNotFound: true);
            m_NoSwimmable_Movement_Jump = m_NoSwimmable_Movement.FindAction("Jump", throwIfNotFound: true);
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

        // NoSwimmable_Movement
        private readonly InputActionMap m_NoSwimmable_Movement;
        private INoSwimmable_MovementActions m_NoSwimmable_MovementActionsCallbackInterface;
        private readonly InputAction m_NoSwimmable_Movement_Horizontal_Movement;
        private readonly InputAction m_NoSwimmable_Movement_Jump;
        public struct NoSwimmable_MovementActions
        {
            private @PlayerGameplayInput m_Wrapper;
            public NoSwimmable_MovementActions(@PlayerGameplayInput wrapper) { m_Wrapper = wrapper; }
            public InputAction @Horizontal_Movement => m_Wrapper.m_NoSwimmable_Movement_Horizontal_Movement;
            public InputAction @Jump => m_Wrapper.m_NoSwimmable_Movement_Jump;
            public InputActionMap Get() { return m_Wrapper.m_NoSwimmable_Movement; }
            public void Enable() { Get().Enable(); }
            public void Disable() { Get().Disable(); }
            public bool enabled => Get().enabled;
            public static implicit operator InputActionMap(NoSwimmable_MovementActions set) { return set.Get(); }
            public void SetCallbacks(INoSwimmable_MovementActions instance)
            {
                if (m_Wrapper.m_NoSwimmable_MovementActionsCallbackInterface != null)
                {
                    @Horizontal_Movement.started -= m_Wrapper.m_NoSwimmable_MovementActionsCallbackInterface.OnHorizontal_Movement;
                    @Horizontal_Movement.performed -= m_Wrapper.m_NoSwimmable_MovementActionsCallbackInterface.OnHorizontal_Movement;
                    @Horizontal_Movement.canceled -= m_Wrapper.m_NoSwimmable_MovementActionsCallbackInterface.OnHorizontal_Movement;
                    @Jump.started -= m_Wrapper.m_NoSwimmable_MovementActionsCallbackInterface.OnJump;
                    @Jump.performed -= m_Wrapper.m_NoSwimmable_MovementActionsCallbackInterface.OnJump;
                    @Jump.canceled -= m_Wrapper.m_NoSwimmable_MovementActionsCallbackInterface.OnJump;
                }
                m_Wrapper.m_NoSwimmable_MovementActionsCallbackInterface = instance;
                if (instance != null)
                {
                    @Horizontal_Movement.started += instance.OnHorizontal_Movement;
                    @Horizontal_Movement.performed += instance.OnHorizontal_Movement;
                    @Horizontal_Movement.canceled += instance.OnHorizontal_Movement;
                    @Jump.started += instance.OnJump;
                    @Jump.performed += instance.OnJump;
                    @Jump.canceled += instance.OnJump;
                }
            }
        }
        public NoSwimmable_MovementActions @NoSwimmable_Movement => new NoSwimmable_MovementActions(this);
        private int m_KeyboardMouseSchemeIndex = -1;
        public InputControlScheme KeyboardMouseScheme
        {
            get
            {
                if (m_KeyboardMouseSchemeIndex == -1) m_KeyboardMouseSchemeIndex = asset.FindControlSchemeIndex("Keyboard&Mouse");
                return asset.controlSchemes[m_KeyboardMouseSchemeIndex];
            }
        }
        private int m_GamepadSchemeIndex = -1;
        public InputControlScheme GamepadScheme
        {
            get
            {
                if (m_GamepadSchemeIndex == -1) m_GamepadSchemeIndex = asset.FindControlSchemeIndex("Gamepad");
                return asset.controlSchemes[m_GamepadSchemeIndex];
            }
        }
        private int m_TouchSchemeIndex = -1;
        public InputControlScheme TouchScheme
        {
            get
            {
                if (m_TouchSchemeIndex == -1) m_TouchSchemeIndex = asset.FindControlSchemeIndex("Touch");
                return asset.controlSchemes[m_TouchSchemeIndex];
            }
        }
        private int m_JoystickSchemeIndex = -1;
        public InputControlScheme JoystickScheme
        {
            get
            {
                if (m_JoystickSchemeIndex == -1) m_JoystickSchemeIndex = asset.FindControlSchemeIndex("Joystick");
                return asset.controlSchemes[m_JoystickSchemeIndex];
            }
        }
        private int m_XRSchemeIndex = -1;
        public InputControlScheme XRScheme
        {
            get
            {
                if (m_XRSchemeIndex == -1) m_XRSchemeIndex = asset.FindControlSchemeIndex("XR");
                return asset.controlSchemes[m_XRSchemeIndex];
            }
        }
        public interface INoSwimmable_MovementActions
        {
            void OnHorizontal_Movement(InputAction.CallbackContext context);
            void OnJump(InputAction.CallbackContext context);
        }
    }
}
