using UnityEngine;
using System.Collections.Generic;

namespace Survival2D.Entities.Player
{
    public class PlayerSpawner : MonoBehaviour
    {
        [SerializeField] private GameObject player_prefab = null;
        [SerializeField] private int max_players = 1;

        [SerializeField] private Transform player_default_spawn = null;

        private List<GameObject> player_tracker_container = new List<GameObject>(); 

        private void Awake()
        {
#if UNITY_EDITOR
            if (player_prefab == null)
            {
                Debug.LogWarning($"{nameof(player_prefab)} is not assigned to {nameof(PlayerSpawner)} of {gameObject.GetFullName()}");
            }
#endif

            if (player_default_spawn == null)
            {
                player_default_spawn = this.transform;
            }
        }

        public GameObject SpawnPlayer(Transform transform = null)
        {
            if (player_tracker_container.Count >= max_players) return null;

            if (transform == null)
            {
                transform = player_default_spawn;
            }

            var instance = Instantiate(player_prefab, transform.position, Quaternion.identity);
            var player = instance.GetComponent<EntityBehaviour>();
            if (player != null)
            {
                player.onDespaawn.AddListener(DestroyPlayer);
            }
#if UNITY_EDITOR
            else
            {
                Debug.LogError("player prefab doesnt contain entity behaviour");
            }
#endif

            return instance;
        }



        private void DestroyPlayer(GameObject player)
        {
            player_tracker_container.Remove(player);
        }

    }
}