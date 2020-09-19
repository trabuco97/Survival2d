using UnityEngine;

using Survival2D.SceneManagement;

namespace DEBUG.SceneManagement
{
    public class DEBUG_GaameplaySubSceneLoadedScript : MonoBehaviour
    {
        private GameSceneManager scene_manager = null;

        private void Awake()
        {
            var instance = GameObject.FindGameObjectWithTag("SceneManager");
            if (instance != null)
            {
                scene_manager = instance.GetComponent<GameSceneManager>();
            }
            else
            {
                Debug.LogError("asdasd");
            }
        }

        private void Start()
        {
            var gameplay_data = scene_manager.CurrentSceneData as Scriptable_GameplayScene;
            Debug.Log(scene_manager.CurrentSceneData?.scene_name);
            Debug.Log(gameplay_data?.scene_name);
        }
    }
}