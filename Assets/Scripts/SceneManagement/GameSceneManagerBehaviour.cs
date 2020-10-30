using System.Collections.Generic;

using UnityEngine;
using UnityEngine.SceneManagement;

using Survival2D.Saving;

namespace Survival2D.SceneManagement
{
    public class GameSceneManagerBehaviour : IRootComponentSaved
    {
        #region DEBUG_Code
#if UNITY_EDITOR

        [ContextMenu("Load Example000")]
        private void DEBUG1()
        {
            LoadLevel("Example000");
        }

        [ContextMenu("Load Example001")]
        private void DEBUG2()
        {
            LoadLevel(MenuType.Example001);
        }

        [ContextMenu("LoadSubScene 0")]
        private void DEBUG3()
        {
            LoadSpecificSubScene(0);
        }

        [ContextMenu("Restart Level")]
        private void DEBUG4()
        {
            RestartLevel();
        }
        
        [ContextMenu("Load Level Saved")]
        private void DEBUG5()
        {
            LoadData();
        }
#endif
        #endregion
        [SerializeField] private Scriptable_GameSceneDatabase gameScene_database = null;

        // Format of all gameplay levels
        private const string SCENE_NAME_ROOT = "{0}_root";
        private const string SCENE_NAME_SPECIFIC_SUB = "{0}_sub_{1}";
        private const string GENERAL_SUB = "{0}_{1}";

        private List<int> specific_subscenes_loaded = new List<int>();
        private List<int> general_subscenes_loaded = new List<int>();
        private static bool has_subload_queued = false;
        private static int[] specific_subscenes_toLoad = null;
        private static int[] general_subscenes_toLoad = null;


        public Scriptable_IGameScene CurrentSceneData { get { return gameScene_database.CurrentScene; } }
        public override string Component_ID => "GameScene";
        public override object DataToManage { set => SetComponentData(value as GameplaySceneData); }
        public override bool CanBeSaved => true;

        public event SceneMethod OnSceneStart;

        protected override void Awake()
        {
            base.Awake();

#if UNITY_EDITOR
            if (gameScene_database == null)
            {
                Debug.LogWarning($"{nameof(gameScene_database)} is not assigned to {nameof(GameSceneManagerBehaviour)} of {gameObject.GetFullName()}");
            }
#endif
            gameScene_database.IntializeDatabase();
            IntializeFromCurrentScene();
            if (has_subload_queued)
            {
                LoadQueuedSubScenes();
            }
        }

        private void Start()
        {
            OnSceneStart?.Invoke(new SceneEventArgs(gameScene_database.CurrentScene));
        }

        public void LoadLevel(string scene_name)
        {
            SceneManager.LoadSceneAsync(string.Format(SCENE_NAME_ROOT, scene_name), LoadSceneMode.Single);
            specific_subscenes_loaded = new List<int>();
        }

        public void LoadLevel(MenuType type)
        {
            if (gameScene_database.MenuSceneDatabase.TryGetValue(type, out var sceneData))
            {
                SceneManager.LoadSceneAsync(string.Format(SCENE_NAME_ROOT, sceneData.scene_name), LoadSceneMode.Single);
                specific_subscenes_loaded = new List<int>();
            }
        }

        public void LoadSpecificSubScene(int index)
        {
            SceneManager.LoadSceneAsync(string.Format(SCENE_NAME_SPECIFIC_SUB, gameScene_database.CurrentSceneName, index.ToString().PadLeft(2, '0')), LoadSceneMode.Additive);
            specific_subscenes_loaded.Add(index);
        }

        public void LoadGeneralSubscene(int index)
        {
            SceneManager.LoadSceneAsync(string.Format(SCENE_NAME_SPECIFIC_SUB, gameScene_database.CurrentSceneName, index.ToString().PadLeft(2, '0')), LoadSceneMode.Additive);
            specific_subscenes_loaded.Add(index);
        }

        public void RestartLevel()
        {
            LoadLevel(gameScene_database.CurrentSceneName);
        }

        public override object GetComponentData()
        {
            var data = new GameplaySceneData();

            data.scene_name = gameScene_database.CurrentSceneName;
            data.general_subscenes = general_subscenes_loaded.ToArray();
            data.specific_subscenes = specific_subscenes_loaded.ToArray();

            return data;
        }

        // When loading the scene, changes to the scene setted
        private void SetComponentData(GameplaySceneData data)
        {
            if (data != null)
            {
                has_subload_queued = true;

                general_subscenes_toLoad = data.general_subscenes;
                specific_subscenes_toLoad = data.specific_subscenes;

                LoadLevel(data.scene_name);
            }
        }
        
        // To do
        private void LoadQueuedSubScenes()
        {

        }

        private void IntializeFromCurrentScene()
        {
            var current_scene_name = SceneManager.GetActiveScene().name;
            var index = current_scene_name.IndexOf("_root");

            if (index != -1)
            {
                current_scene_name = current_scene_name.Remove(index, 5);
                gameScene_database.CurrentSceneName = current_scene_name;
            }
            else
            {
#if UNITY_EDITOR
                Debug.LogError("Error, name of current scene is invalid");
#endif
                gameScene_database.CurrentSceneName = string.Empty;
            }
        }




    }
}