using UnityEngine;
using System.Collections;

namespace Survival2D.Entities.Player
{

    // TODO : support for multiple players
    public class PlayerManager : MonoBehaviour
    {
        [SerializeField] private PlayerSpawner spawner_behaviour = null;
        [SerializeField] private PlayerSceneBinder binder_behaviour = null;

        public static PlayerManager Instance { get; private set; }
        public GameObject player_object = null;

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
        }

        private void Start()
        {
            player_object = spawner_behaviour.SpawnPlayer();
            binder_behaviour.BindPlayerToScene(player_object);
        }
    }
}