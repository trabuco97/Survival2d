using UnityEngine;
using System.Collections;

using Survival2D.SceneManagement;

namespace DEBUG.SceneManagement
{
    public class DEBUG_GameplaySceneLoadedScript : MonoBehaviour
    {
        [SerializeField] private GameSceneManager scene_manager = null;

        private void OnEnable()
        {
            scene_manager.OnSceneStart += Handler_Action;
        }

        private void OnDisable()
        {
            scene_manager.OnSceneStart -= Handler_Action;
        }

        private void Handler_Action(SceneEventArgs args)
        {
            scene_manager.LoadSubScene(0);
        }


    }
}