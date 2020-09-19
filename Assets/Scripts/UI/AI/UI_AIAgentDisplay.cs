using System.Collections.Generic;
using UnityEngine;

using Survival2D.Entities.AI;

namespace Survival2D.UI.AI
{
    public class UI_AIAgentDisplay : MonoBehaviour
    {
        [SerializeField] private GameObject action_display_prefab = null;

        [Header("References")]
        [SerializeField] private Transform action_display_sceneContainer = null;

        private AgentAIBehaviour ai_behaviour;
        private Dictionary<ActionType, UI_AIActionDisplay> ui_action_display_database = new Dictionary<ActionType, UI_AIActionDisplay>();



        private void Awake()
        {
#if UNITY_EDITOR
            if (action_display_prefab == null)
            {
                Debug.LogWarning($"{nameof(action_display_prefab)} not found in behaviour {typeof(UI_AIAgentDisplay)} in object {gameObject.GetFullName()}");
            }
            if (action_display_sceneContainer == null)
            {
                Debug.LogWarning($"{nameof(action_display_sceneContainer)} not found in behaviour {typeof(UI_AIAgentDisplay)} in object {gameObject.GetFullName()}");
            }
#endif
            ai_behaviour = GetComponentInParent<AgentAIBehaviour>();
        }

        private void Start()
        {
            InitializeDisplay();
        }

        private void OnDestroy()
        {
            ai_behaviour.Agent.OnActionUpdate -= Handler_OnActionValueUpdate;
        }


        private void InitializeDisplay()
        {
            var action_collection = ai_behaviour.Agent.ActionsPerformed;
            foreach (var action in action_collection)
            {
                var instance = Instantiate(action_display_prefab, action_display_sceneContainer);
                var ui_action_display = instance.GetComponent<UI_AIActionDisplay>();

                ui_action_display_database.Add(action.Type, ui_action_display);
            }

            ai_behaviour.Agent.OnActionUpdate += Handler_OnActionValueUpdate;
        }


        private void Handler_OnActionValueUpdate(AIEventArgs args)
        {
            if (ui_action_display_database.TryGetValue(args.ActionType, out var action_display))
            {
                action_display.UpdateActionDisplay(args.ActionType, args.ActionValue);
            }
        }


    }
}