using UnityEngine;
using System.Collections;

namespace Survival2D.Entities.AI
{
    public class AgentAIBehaviour : MonoBehaviour
    {
        [SerializeField] private float custom_update_time = 1f;
        [SerializeField] private Scriptable_AnimalAI animal_ai = null;
        [SerializeField] private EntityBehaviour entity = null;

        public AgentAI Agent { get; private set; } = null;
        private float current_update_time = 0f;

        private void Awake()
        {
            Agent = ScriptableAIProcessor.ProcessScriptableAI(animal_ai);
            current_update_time = custom_update_time;
        }


        private void Update()
        {
            current_update_time -= Time.deltaTime;
            if (current_update_time <= 0)
            {
                current_update_time = custom_update_time;
                Agent.RecalculateActionToPerform(new AIGameContext { current_entity = entity });
            }
        }


    }
}