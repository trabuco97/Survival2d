using UnityEngine;
using System.Collections;

namespace Survival2D.Entities.Player
{
    // Behaviour in the scene
    public class PlayerSceneBinder : MonoBehaviour
    {
        private GameObject player_binded = null;
        public bool HasPlayerBinded { get { return player_binded != null; } }

        public event EntityMethods OnPlayerBinded;

        public void BindPlayerToScene(GameObject player)
        {
            player_binded = player;
            if (HasPlayerBinded)
            {
                OnPlayerBinded?.Invoke(new EntityEventArgs(player));
            }
        }

        // The player is already in the scene and binded to this component
        public bool RequestBehaviour<T>(out T behaviour) where T : MonoBehaviour
        {
            behaviour = null;

            if (!HasPlayerBinded) return false;
            var behaviour_searched = player_binded.GetComponentInChildren<T>();
            if (behaviour_searched != null)
            {
                behaviour = behaviour_searched;
                return true;
            }

            return false;
        }
    }
}