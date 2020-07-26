using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Survival2D.Input
{
    public enum CurrentActionMaps { No_SwimmableMovement, ConsoleChat, Inventory }

    // Expand upon adding new inputs
    public class InputClient : MonoBehaviour
    {

        public PlayerGameplayInput GameplayInput { get; private set; }
        public PlayerUIInput UIInput { get; private set; }


        private Dictionary<CurrentActionMaps, InputActionMap> maps_database = new Dictionary<CurrentActionMaps, InputActionMap>();

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

        public void SetActionMapsState(bool state, params CurrentActionMaps[] action_maps_disabled)
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
            maps_database.Add(CurrentActionMaps.No_SwimmableMovement, GameplayInput.NoSwimmable_Movement.Get());
            maps_database.Add(CurrentActionMaps.ConsoleChat, UIInput.ConsoleChat.Get());
            maps_database.Add(CurrentActionMaps.Inventory, UIInput.Inventory.Get());
        }
    }
}