using UnityEngine;
using UnityEngine.SceneManagement;

namespace Survival2D.SceneManagement
{
    public class GameSceneManager : MonoBehaviour
    {
        [SerializeField] private Scriptable_GameSceneDatabase gameScene_database = null;

        // Format of all gameplay levels
        private const string SCENE_NAME_ROOT = "{0}_root";
        private const string SCENE_NAME_SUB = "{0}_sub_{1}";

        public Scriptable_IGameScene CurrentSceneData { get { return gameScene_database.CurrentScene; } }
        public event SceneMethod OnSceneStart;

        private void Awake()
        {
#if UNITY_EDITOR
            if (gameScene_database == null)
            {
                Debug.LogWarning($"{nameof(gameScene_database)} is not assigned to {nameof(GameSceneManager)} of {gameObject.GetFullName()}");
            }
#endif
            gameScene_database.IntializeDatabase();
            IntializeFromCurrentScene();
        }

        private void Start()
        {
            OnSceneStart?.Invoke(new SceneEventArgs(gameScene_database.CurrentScene));
        }

        public void LoadLevel(string scene_name)
        {
            if (gameScene_database.GameplaySceneDatabase.TryGetValue(scene_name, out var sceneData))
            {
                SceneManager.LoadSceneAsync(string.Format(SCENE_NAME_ROOT, scene_name), LoadSceneMode.Single);
            }
        }

        public void LoadLevel(MenuType type)
        {
            if (gameScene_database.MenuSceneDatabase.TryGetValue(type, out var sceneData))
            {
                SceneManager.LoadSceneAsync(string.Format(SCENE_NAME_ROOT, sceneData.scene_name), LoadSceneMode.Single);
            }
        }

        public void LoadSubScene(int index)
        {
            SceneManager.LoadSceneAsync(string.Format(SCENE_NAME_SUB, gameScene_database.CurrentSceneName, index.ToString().PadLeft(2, '0')), LoadSceneMode.Additive);
        }

        public void RestartLevel()
        {
            LoadLevel(gameScene_database.CurrentSceneName);
        }

        private void IntializeFromCurrentScene()
        {
            var current_scene_name = SceneManager.GetActiveScene().name;
            var index = current_scene_name.IndexOf("_root");
            current_scene_name = current_scene_name.Remove(index, 5);
            gameScene_database.CurrentSceneName = current_scene_name;
        }

#if UNITY_EDITOR

        [ContextMenu("Load House000")]
        private void DEBUG1()
        {
            LoadLevel("House000");
        }

        [ContextMenu("Load MainMenu")]
        private void DEBUG2()
        {
            LoadLevel(MenuType.MainMenu);
        }

        [ContextMenu("Restart Level")]
        private void DEBUG3()
        {
            RestartLevel();
        }
#endif

    }
}