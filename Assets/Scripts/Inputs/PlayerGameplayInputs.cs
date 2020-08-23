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
        },
        {
            ""name"": ""ToolUsage"",
            ""id"": ""5012f7ab-49fb-410e-b006-734803438038"",
            ""actions"": [
                {
                    ""name"": ""Primary"",
                    ""type"": ""Button"",
                    ""id"": ""3d2b36d8-7e38-4fba-8479-2fd4ee248c74"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Secondary"",
                    ""type"": ""Button"",
                    ""id"": ""576f5200-b825-42be-82f2-ca37f25a8ebd"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                }
            ],
            ""bindings"": [
                {
                    ""name"": """",
                    ""id"": ""1ae58b4c-4343-4f98-a529-cd4acf896ec5"",
                    ""path"": ""<Mouse>/leftButton"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard&Mouse"",
                    ""action"": ""Primary"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""a7b32739-e9c0-414a-8069-45079d83b035"",
                    ""path"": ""<Mouse>/rightButton"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard&Mouse"",
                    ""action"": ""Secondary"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        },
        {
            ""name"": ""Hotbar"",
            ""id"": ""e709d77b-d227-4614-8cee-ea9709ce56fc"",
            ""actions"": [
                {
                    ""name"": ""Scroll"",
                    ""type"": ""Value"",
                    ""id"": ""d76163ee-6fe4-4c13-8981-8b9d4f5c5122"",
                    ""expectedControlType"": ""Vector2"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""1"",
                    ""type"": ""Button"",
                    ""id"": ""30fc3e09-e3c3-4e56-ac3e-f5a5757e1d7e"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""2"",
                    ""type"": ""Button"",
                    ""id"": ""0a18aa13-1096-4398-8f99-cad8ca6f805e"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""3"",
                    ""type"": ""Button"",
                    ""id"": ""12b4924e-1bcc-4bca-9244-f92f5443519f"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""4"",
                    ""type"": ""Button"",
                    ""id"": ""7a133dec-aa12-4ac8-b42c-9f8f4ad9ea09"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""5"",
                    ""type"": ""Button"",
                    ""id"": ""0a93e690-2cf9-4b95-9f84-aa28a6e8517b"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""6"",
                    ""type"": ""Button"",
                    ""id"": ""aa56d016-a092-430e-bc9f-07aa59681819"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""7"",
                    ""type"": ""Button"",
                    ""id"": ""cd5cab9b-c82a-4dd7-a88a-4c0f20ddd933"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""8"",
                    ""type"": ""Button"",
                    ""id"": ""d3d7b8c1-4cdf-4a28-93ac-6c1f5ea700ea"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""9"",
                    ""type"": ""Button"",
                    ""id"": ""a8b5ef2d-4a90-4a65-9154-b466c733930b"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""0"",
                    ""type"": ""Button"",
                    ""id"": ""916baee2-3f67-41e9-9bfc-3e3c1ff43266"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                }
            ],
            ""bindings"": [
                {
                    ""name"": """",
                    ""id"": ""b7927082-cfb9-4e18-a66f-66496cab655b"",
                    ""path"": ""<Mouse>/scroll"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Scroll"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""e78f2e7f-c38e-4944-913b-57b16472a092"",
                    ""path"": ""<Keyboard>/1"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""1"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""992544dd-2cc5-4706-88b9-31ae3d46ae16"",
                    ""path"": ""<Keyboard>/2"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""2"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""3dac61c7-2e3d-46ba-a935-2781efe964a1"",
                    ""path"": ""<Keyboard>/3"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""3"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""b4f80e5f-62df-41f1-bf88-c99b0c7d34a2"",
                    ""path"": ""<Keyboard>/4"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""4"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""2e898c07-cc3c-48e4-b50e-d674fbcab212"",
                    ""path"": ""<Keyboard>/5"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""5"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""7fc4e59c-3f4f-42ec-ba3e-c2ce2de6ca9a"",
                    ""path"": ""<Keyboard>/6"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""6"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""328d69e4-4a1d-4c40-a5ad-bf4771ad4b69"",
                    ""path"": ""<Keyboard>/7"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""7"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""a70a8979-5c5f-4c19-b02b-7ebd5eb007d8"",
                    ""path"": ""<Keyboard>/8"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""8"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""70ceef41-fd1d-4e0d-a65c-dfba7974506a"",
                    ""path"": ""<Keyboard>/9"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""9"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""6385586a-9f5f-448e-82c6-61ccf4c22a7b"",
                    ""path"": ""<Keyboard>/0"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""0"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        },
        {
            ""name"": ""LookDirection"",
            ""id"": ""3ad86e44-3f1d-49d4-b744-99646c03c164"",
            ""actions"": [
                {
                    ""name"": ""LookPosition"",
                    ""type"": ""PassThrough"",
                    ""id"": ""40e4d342-8345-4d58-a429-5655ba4100c2"",
                    ""expectedControlType"": ""Vector2"",
                    ""processors"": """",
                    ""interactions"": """"
                }
            ],
            ""bindings"": [
                {
                    ""name"": """",
                    ""id"": ""ff1b296f-e4da-48dd-bd49-c676ae82b212"",
                    ""path"": ""<Mouse>/position"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard&Mouse"",
                    ""action"": ""LookPosition"",
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
            // ToolUsage
            m_ToolUsage = asset.FindActionMap("ToolUsage", throwIfNotFound: true);
            m_ToolUsage_Primary = m_ToolUsage.FindAction("Primary", throwIfNotFound: true);
            m_ToolUsage_Secondary = m_ToolUsage.FindAction("Secondary", throwIfNotFound: true);
            // Hotbar
            m_Hotbar = asset.FindActionMap("Hotbar", throwIfNotFound: true);
            m_Hotbar_Scroll = m_Hotbar.FindAction("Scroll", throwIfNotFound: true);
            m_Hotbar__1 = m_Hotbar.FindAction("1", throwIfNotFound: true);
            m_Hotbar__2 = m_Hotbar.FindAction("2", throwIfNotFound: true);
            m_Hotbar__3 = m_Hotbar.FindAction("3", throwIfNotFound: true);
            m_Hotbar__4 = m_Hotbar.FindAction("4", throwIfNotFound: true);
            m_Hotbar__5 = m_Hotbar.FindAction("5", throwIfNotFound: true);
            m_Hotbar__6 = m_Hotbar.FindAction("6", throwIfNotFound: true);
            m_Hotbar__7 = m_Hotbar.FindAction("7", throwIfNotFound: true);
            m_Hotbar__8 = m_Hotbar.FindAction("8", throwIfNotFound: true);
            m_Hotbar__9 = m_Hotbar.FindAction("9", throwIfNotFound: true);
            m_Hotbar__0 = m_Hotbar.FindAction("0", throwIfNotFound: true);
            // LookDirection
            m_LookDirection = asset.FindActionMap("LookDirection", throwIfNotFound: true);
            m_LookDirection_LookPosition = m_LookDirection.FindAction("LookPosition", throwIfNotFound: true);
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

        // ToolUsage
        private readonly InputActionMap m_ToolUsage;
        private IToolUsageActions m_ToolUsageActionsCallbackInterface;
        private readonly InputAction m_ToolUsage_Primary;
        private readonly InputAction m_ToolUsage_Secondary;
        public struct ToolUsageActions
        {
            private @PlayerGameplayInput m_Wrapper;
            public ToolUsageActions(@PlayerGameplayInput wrapper) { m_Wrapper = wrapper; }
            public InputAction @Primary => m_Wrapper.m_ToolUsage_Primary;
            public InputAction @Secondary => m_Wrapper.m_ToolUsage_Secondary;
            public InputActionMap Get() { return m_Wrapper.m_ToolUsage; }
            public void Enable() { Get().Enable(); }
            public void Disable() { Get().Disable(); }
            public bool enabled => Get().enabled;
            public static implicit operator InputActionMap(ToolUsageActions set) { return set.Get(); }
            public void SetCallbacks(IToolUsageActions instance)
            {
                if (m_Wrapper.m_ToolUsageActionsCallbackInterface != null)
                {
                    @Primary.started -= m_Wrapper.m_ToolUsageActionsCallbackInterface.OnPrimary;
                    @Primary.performed -= m_Wrapper.m_ToolUsageActionsCallbackInterface.OnPrimary;
                    @Primary.canceled -= m_Wrapper.m_ToolUsageActionsCallbackInterface.OnPrimary;
                    @Secondary.started -= m_Wrapper.m_ToolUsageActionsCallbackInterface.OnSecondary;
                    @Secondary.performed -= m_Wrapper.m_ToolUsageActionsCallbackInterface.OnSecondary;
                    @Secondary.canceled -= m_Wrapper.m_ToolUsageActionsCallbackInterface.OnSecondary;
                }
                m_Wrapper.m_ToolUsageActionsCallbackInterface = instance;
                if (instance != null)
                {
                    @Primary.started += instance.OnPrimary;
                    @Primary.performed += instance.OnPrimary;
                    @Primary.canceled += instance.OnPrimary;
                    @Secondary.started += instance.OnSecondary;
                    @Secondary.performed += instance.OnSecondary;
                    @Secondary.canceled += instance.OnSecondary;
                }
            }
        }
        public ToolUsageActions @ToolUsage => new ToolUsageActions(this);

        // Hotbar
        private readonly InputActionMap m_Hotbar;
        private IHotbarActions m_HotbarActionsCallbackInterface;
        private readonly InputAction m_Hotbar_Scroll;
        private readonly InputAction m_Hotbar__1;
        private readonly InputAction m_Hotbar__2;
        private readonly InputAction m_Hotbar__3;
        private readonly InputAction m_Hotbar__4;
        private readonly InputAction m_Hotbar__5;
        private readonly InputAction m_Hotbar__6;
        private readonly InputAction m_Hotbar__7;
        private readonly InputAction m_Hotbar__8;
        private readonly InputAction m_Hotbar__9;
        private readonly InputAction m_Hotbar__0;
        public struct HotbarActions
        {
            private @PlayerGameplayInput m_Wrapper;
            public HotbarActions(@PlayerGameplayInput wrapper) { m_Wrapper = wrapper; }
            public InputAction @Scroll => m_Wrapper.m_Hotbar_Scroll;
            public InputAction @_1 => m_Wrapper.m_Hotbar__1;
            public InputAction @_2 => m_Wrapper.m_Hotbar__2;
            public InputAction @_3 => m_Wrapper.m_Hotbar__3;
            public InputAction @_4 => m_Wrapper.m_Hotbar__4;
            public InputAction @_5 => m_Wrapper.m_Hotbar__5;
            public InputAction @_6 => m_Wrapper.m_Hotbar__6;
            public InputAction @_7 => m_Wrapper.m_Hotbar__7;
            public InputAction @_8 => m_Wrapper.m_Hotbar__8;
            public InputAction @_9 => m_Wrapper.m_Hotbar__9;
            public InputAction @_0 => m_Wrapper.m_Hotbar__0;
            public InputActionMap Get() { return m_Wrapper.m_Hotbar; }
            public void Enable() { Get().Enable(); }
            public void Disable() { Get().Disable(); }
            public bool enabled => Get().enabled;
            public static implicit operator InputActionMap(HotbarActions set) { return set.Get(); }
            public void SetCallbacks(IHotbarActions instance)
            {
                if (m_Wrapper.m_HotbarActionsCallbackInterface != null)
                {
                    @Scroll.started -= m_Wrapper.m_HotbarActionsCallbackInterface.OnScroll;
                    @Scroll.performed -= m_Wrapper.m_HotbarActionsCallbackInterface.OnScroll;
                    @Scroll.canceled -= m_Wrapper.m_HotbarActionsCallbackInterface.OnScroll;
                    @_1.started -= m_Wrapper.m_HotbarActionsCallbackInterface.On_1;
                    @_1.performed -= m_Wrapper.m_HotbarActionsCallbackInterface.On_1;
                    @_1.canceled -= m_Wrapper.m_HotbarActionsCallbackInterface.On_1;
                    @_2.started -= m_Wrapper.m_HotbarActionsCallbackInterface.On_2;
                    @_2.performed -= m_Wrapper.m_HotbarActionsCallbackInterface.On_2;
                    @_2.canceled -= m_Wrapper.m_HotbarActionsCallbackInterface.On_2;
                    @_3.started -= m_Wrapper.m_HotbarActionsCallbackInterface.On_3;
                    @_3.performed -= m_Wrapper.m_HotbarActionsCallbackInterface.On_3;
                    @_3.canceled -= m_Wrapper.m_HotbarActionsCallbackInterface.On_3;
                    @_4.started -= m_Wrapper.m_HotbarActionsCallbackInterface.On_4;
                    @_4.performed -= m_Wrapper.m_HotbarActionsCallbackInterface.On_4;
                    @_4.canceled -= m_Wrapper.m_HotbarActionsCallbackInterface.On_4;
                    @_5.started -= m_Wrapper.m_HotbarActionsCallbackInterface.On_5;
                    @_5.performed -= m_Wrapper.m_HotbarActionsCallbackInterface.On_5;
                    @_5.canceled -= m_Wrapper.m_HotbarActionsCallbackInterface.On_5;
                    @_6.started -= m_Wrapper.m_HotbarActionsCallbackInterface.On_6;
                    @_6.performed -= m_Wrapper.m_HotbarActionsCallbackInterface.On_6;
                    @_6.canceled -= m_Wrapper.m_HotbarActionsCallbackInterface.On_6;
                    @_7.started -= m_Wrapper.m_HotbarActionsCallbackInterface.On_7;
                    @_7.performed -= m_Wrapper.m_HotbarActionsCallbackInterface.On_7;
                    @_7.canceled -= m_Wrapper.m_HotbarActionsCallbackInterface.On_7;
                    @_8.started -= m_Wrapper.m_HotbarActionsCallbackInterface.On_8;
                    @_8.performed -= m_Wrapper.m_HotbarActionsCallbackInterface.On_8;
                    @_8.canceled -= m_Wrapper.m_HotbarActionsCallbackInterface.On_8;
                    @_9.started -= m_Wrapper.m_HotbarActionsCallbackInterface.On_9;
                    @_9.performed -= m_Wrapper.m_HotbarActionsCallbackInterface.On_9;
                    @_9.canceled -= m_Wrapper.m_HotbarActionsCallbackInterface.On_9;
                    @_0.started -= m_Wrapper.m_HotbarActionsCallbackInterface.On_0;
                    @_0.performed -= m_Wrapper.m_HotbarActionsCallbackInterface.On_0;
                    @_0.canceled -= m_Wrapper.m_HotbarActionsCallbackInterface.On_0;
                }
                m_Wrapper.m_HotbarActionsCallbackInterface = instance;
                if (instance != null)
                {
                    @Scroll.started += instance.OnScroll;
                    @Scroll.performed += instance.OnScroll;
                    @Scroll.canceled += instance.OnScroll;
                    @_1.started += instance.On_1;
                    @_1.performed += instance.On_1;
                    @_1.canceled += instance.On_1;
                    @_2.started += instance.On_2;
                    @_2.performed += instance.On_2;
                    @_2.canceled += instance.On_2;
                    @_3.started += instance.On_3;
                    @_3.performed += instance.On_3;
                    @_3.canceled += instance.On_3;
                    @_4.started += instance.On_4;
                    @_4.performed += instance.On_4;
                    @_4.canceled += instance.On_4;
                    @_5.started += instance.On_5;
                    @_5.performed += instance.On_5;
                    @_5.canceled += instance.On_5;
                    @_6.started += instance.On_6;
                    @_6.performed += instance.On_6;
                    @_6.canceled += instance.On_6;
                    @_7.started += instance.On_7;
                    @_7.performed += instance.On_7;
                    @_7.canceled += instance.On_7;
                    @_8.started += instance.On_8;
                    @_8.performed += instance.On_8;
                    @_8.canceled += instance.On_8;
                    @_9.started += instance.On_9;
                    @_9.performed += instance.On_9;
                    @_9.canceled += instance.On_9;
                    @_0.started += instance.On_0;
                    @_0.performed += instance.On_0;
                    @_0.canceled += instance.On_0;
                }
            }
        }
        public HotbarActions @Hotbar => new HotbarActions(this);

        // LookDirection
        private readonly InputActionMap m_LookDirection;
        private ILookDirectionActions m_LookDirectionActionsCallbackInterface;
        private readonly InputAction m_LookDirection_LookPosition;
        public struct LookDirectionActions
        {
            private @PlayerGameplayInput m_Wrapper;
            public LookDirectionActions(@PlayerGameplayInput wrapper) { m_Wrapper = wrapper; }
            public InputAction @LookPosition => m_Wrapper.m_LookDirection_LookPosition;
            public InputActionMap Get() { return m_Wrapper.m_LookDirection; }
            public void Enable() { Get().Enable(); }
            public void Disable() { Get().Disable(); }
            public bool enabled => Get().enabled;
            public static implicit operator InputActionMap(LookDirectionActions set) { return set.Get(); }
            public void SetCallbacks(ILookDirectionActions instance)
            {
                if (m_Wrapper.m_LookDirectionActionsCallbackInterface != null)
                {
                    @LookPosition.started -= m_Wrapper.m_LookDirectionActionsCallbackInterface.OnLookPosition;
                    @LookPosition.performed -= m_Wrapper.m_LookDirectionActionsCallbackInterface.OnLookPosition;
                    @LookPosition.canceled -= m_Wrapper.m_LookDirectionActionsCallbackInterface.OnLookPosition;
                }
                m_Wrapper.m_LookDirectionActionsCallbackInterface = instance;
                if (instance != null)
                {
                    @LookPosition.started += instance.OnLookPosition;
                    @LookPosition.performed += instance.OnLookPosition;
                    @LookPosition.canceled += instance.OnLookPosition;
                }
            }
        }
        public LookDirectionActions @LookDirection => new LookDirectionActions(this);
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
        public interface IToolUsageActions
        {
            void OnPrimary(InputAction.CallbackContext context);
            void OnSecondary(InputAction.CallbackContext context);
        }
        public interface IHotbarActions
        {
            void OnScroll(InputAction.CallbackContext context);
            void On_1(InputAction.CallbackContext context);
            void On_2(InputAction.CallbackContext context);
            void On_3(InputAction.CallbackContext context);
            void On_4(InputAction.CallbackContext context);
            void On_5(InputAction.CallbackContext context);
            void On_6(InputAction.CallbackContext context);
            void On_7(InputAction.CallbackContext context);
            void On_8(InputAction.CallbackContext context);
            void On_9(InputAction.CallbackContext context);
            void On_0(InputAction.CallbackContext context);
        }
        public interface ILookDirectionActions
        {
            void OnLookPosition(InputAction.CallbackContext context);
        }
    }
}
