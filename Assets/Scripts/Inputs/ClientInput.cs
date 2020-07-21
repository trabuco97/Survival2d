using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Survival2D.Input
{
    // Expand upon adding new inputs
    public class ClientInput : MonoBehaviour
    {
        public PlayerGameplayInput GameplayInput { get; private set; }
        public PlayerUIInput UIInput { get; private set; }

        private Dictionary<string, InputActionMap> maps_database = new Dictionary<string, InputActionMap>();

        private void Awake()
        {
            GameplayInput = new PlayerGameplayInput();
            UIInput = new PlayerUIInput();

            InicializeMap();
        }

        private void OnEnable()
        {
            GameplayInput.Enable();
            UIInput.Enable();
        }

        private void OnDisable()
        {
            GameplayInput.Disable();
            UIInput.Disable();
        }

        public void SetActionMapsState(bool state, params string[] action_maps_disabled)
        {
            foreach (var action_map_name in action_maps_disabled)
            {
                if (maps_database.TryGetValue(action_map_name, out var action_map))
                {
                    if (state)
                    {
                        action_map.Enable();
                    }
                    else
                    {
                        action_map.Disable();
                    }
                }
#if UNITY_EDITOR
                else
                {
                    Debug.LogWarning($"{action_map_name} not found in client input");
                }
#endif
            }
        }


        private void InicializeMap()
        {
            maps_database.Add("movement", GameplayInput.Movement.Get());
            maps_database.Add("consolechat", UIInput.ConsoleChat.Get());
            maps_database.Add("inventory", UIInput.Inventory.Get());
        }
    }
}