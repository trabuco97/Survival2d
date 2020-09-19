using UnityEngine;

using Survival2D.SceneManagement;

namespace DEBUG.SceneManagement
{
    public class DEBUG_MenuSceneLoadedScript : MonoBehaviour
    {
        [SerializeField] private GameSceneManager scene_manager = null;

        private void Awake()
        {
        }

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
            var menu_data = args.SceneData as Scriptable_MenuScene;
            Debug.Log(menu_data?.type);
        }


    }
}