using UnityEngine;
using System.Collections;

namespace Survival2D.Entities.Player
{

    // TODO : support for multiple players
    public class PlayerManager : MonoBehaviour
    {
        [SerializeField] private GameObject player_object_AtAwake = null;       // TODO: if using it, change how dependencies between systems works 

        [SerializeField] private PlayerSpawner spawner_behaviour = null;
        [SerializeField] private PlayerSceneBinder binder_behaviour = null;

        public static PlayerManager Instance { get; private set; }
        public GameObject PlayerObject { get; private set; } = null;

        private void Awake()
        {
#if UNITY_EDITOR
            if (spawner_behaviour == null)
            {
                Debug.LogWarning($"{nameof(spawner_behaviour)} is not assigned to {nameof(PlayerManager)} of {gameObject.GetFullName()}");
            }

            if (binder_behaviour == null)
            {
                Debug.LogWarning($"{nameof(binder_behaviour)} is not assigned to {nameof(PlayerManager)} of {gameObject.GetFullName()}");
            }
#endif

            Instance = this;
            if (player_object_AtAwake != null)
            {
                PlayerObject = player_object_AtAwake;
            }

        }

        private void Start()
        {
            if (player_object_AtAwake == null)
            {
                PlayerObject = spawner_behaviour.SpawnPlayer();
            }
            else
            {
                PlayerObject = player_object_AtAwake;
            }

            binder_behaviour.BindPlayerToScene(PlayerObject);

        }
    }
}