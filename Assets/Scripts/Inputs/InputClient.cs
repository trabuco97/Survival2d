using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Survival2D.Input
{
    public enum InputType { Gameplay, UI }

    public enum CurrentActionMaps 
    { 
        No_SwimmableMovement, 
        ConsoleChat, 
        Inventory, 
        Hotbar,
        ToolUsage,
        LookDirection
    }

    // Expand upon adding new inputs
    public class InputClient : MonoBehaviour
    {
        #region DATA_SPECIFIC
        private class InputActionMapData
        {
            public InputActionMap action_map;
            public InputType action_type;
        }
        #endregion

        public PlayerGameplayInput GameplayInput { get; private set; }
        public PlayerUIInput UIInput { get; private set; }

        private Dictionary<CurrentActionMaps, InputActionMapData> maps_database = new Dictionary<CurrentActionMaps, InputActionMapData>();

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
                if (maps_database.TryGetValue(action_map_name, out var data))
                {
                    SetMapState(data.action_map, state);
                }
#if UNITY_EDITOR
                else
                {
                    Debug.LogWarning($"{action_map_name} not found in client input");
                }
#endif
            }
        }


        public void SetInputTypeState(bool state, InputType[] type_array, params CurrentActionMaps[] exceptions)
        {
            foreach (var wrapper in maps_database)
            {
                foreach (var type in type_array)
                {
                    if (ContainsSameType(exceptions, wrapper.Key)) continue;


                    if (wrapper.Value.action_type == type)
                    {
                        SetMapState(wrapper.Value.action_map, state);
                    }
                }


            }
        }

        private void InicializeMap()
        {
            AddToDatabase(CurrentActionMaps.No_SwimmableMovement, GameplayInput.NoSwimmable_Movement.Get(), InputType.Gameplay);
            AddToDatabase(CurrentActionMaps.ConsoleChat, UIInput.ConsoleChat.Get(), InputType.UI);
            AddToDatabase(CurrentActionMaps.Inventory, UIInput.Inventory.Get(), InputType.UI);
            AddToDatabase(CurrentActionMaps.Hotbar, GameplayInput.Hotbar.Get(), InputType.Gameplay);
            AddToDatabase(CurrentActionMaps.ToolUsage, GameplayInput.ToolUsage.Get(), InputType.Gameplay);
            AddToDatabase(CurrentActionMaps.LookDirection, GameplayInput.LookDirection.Get(), InputType.Gameplay);
        }

        private void AddToDatabase(CurrentActionMaps action_map_type, InputActionMap map, InputType input_type)
        {
            maps_database.Add(action_map_type, new InputActionMapData { action_map = map, action_type = input_type });
        }

        private void SetMapState(InputActionMap map, bool state)
        {
            if (state)
            {
                map.Enable();
            }
            else
            {
                map.Disable();
            }
        }

        private bool ContainsSameType(CurrentActionMaps[] type_array, CurrentActionMaps current_type)
        {
            foreach (var type in type_array)
            {
                if (current_type == type) return true;
            }

            return false;
        }
    }
}