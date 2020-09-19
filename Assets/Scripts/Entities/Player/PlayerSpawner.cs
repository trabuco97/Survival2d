using UnityEngine;
using System.Collections.Generic;

namespace Survival2D.Entities.Player
{
    /// <summary>
    /// TODO : something something
    /// </summary>
    public class PlayerSpawner : IEntitySpawner
    {
        [SerializeField] private GameObject player_prefab = null;
        [SerializeField] private uint max_players = 1;

        [SerializeField] private Transform player_default_spawn = null;

        private uint current_players = 0;

        protected override void Awake()
        {
            base.Awake();

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

        protected override GameObject GetEntitySpawner()
        {
            if (current_players >= max_players) return null;

            if (transform == null)
            {
                player_default_spawn = this.transform;
            }

            var instance = Instantiate(player_prefab, player_default_spawn.position, Quaternion.identity);
            var player = instance.GetComponent<EntityBehaviour>();
            if (player != null)
            {
                EntityMethods handler = null;

                handler = (args) =>
                {
                    current_players--;
                    player.OnDespawn -= handler;
                };

                player.OnDespawn += handler;
            }
#if UNITY_EDITOR
            else
            {
                Debug.LogError("player prefab doesnt contain entity behaviour");
            }
#endif

            current_players++;
            return instance;
        }
    }
}