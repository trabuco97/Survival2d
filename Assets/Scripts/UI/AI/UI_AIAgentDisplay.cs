using UnityEngine;

using Survival2D.Entities.AI;

namespace Survival2D.UI.AI
{
    public class UI_AIAgentDisplay : MonoBehaviour
    {
        [SerializeField] private GameObject action_display_prefab = null;

        private AgentAIBehaviour ai_behaviour;



        private void Awake()
        {
#if UNITY_EDITOR
            if (action_display_prefab == null)
            {
                Debug.LogWarning($"{nameof(action_display_prefab)} not found in behaviour {typeof(UI_AIAgentDisplay)} in object {gameObject.GetFullName()}");
            }
#endif
            ai_behaviour = GetComponentInParent<AgentAIBehaviour>();
        }

        private void Update()
        {
            
        }


        private void InitializeDisplay()
        {
            foreach (var action in ai_behaviour)


        }



    }
}