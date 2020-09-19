using UnityEngine;

using Survival2D.Saving;

namespace Survival2D.Entities.Player
{

    // TODO : support for multiple players
    public class PlayerManager : MonoBehaviour
    {
        [SerializeField] private bool spawn_at_Start = false;
        [SerializeField] private EntityBehaviour custom_player_object = null;       // TODO: if using it, change how dependencies between systems works 

        [Header("References")]
        [SerializeField] private PlayerSpawner spawner_behaviour = null;
        [SerializeField] private PlayerSceneBinder binder_behaviour = null;

        public static PlayerManager Instance { get; private set; }
        public EntityBehaviour PlayerEntity { get; private set; } = null;

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
            if (custom_player_object != null)
            {
                PlayerEntity = custom_player_object;
            }

        }

        private void SpawnPlayer()
        {
            if (custom_player_object == null)
            {
                PlayerEntity = spawner_behaviour.SpawnEntity();
            }
            else
            {
                PlayerEntity = custom_player_object;
            }

        }

        private void InitializePlayer()
        {
            PlayerEntity.OrderBehaviour.InitializeObject();
            binder_behaviour.BindPlayerToScene(PlayerEntity);
        }

        private void Start()
        {
            if (spawn_at_Start)
            {
                SpawnPlayer();
                InitializePlayer();
            }
        }
    }
}