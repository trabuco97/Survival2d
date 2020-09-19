using UnityEngine;
using System.Collections.Generic;

namespace Survival2D.SceneManagement
{
    [CreateAssetMenu(fileName = "GameSceneDatabase", menuName = "Custom/Scene/Database")]
    public class Scriptable_GameSceneDatabase : ScriptableObject, ISerializationCallbackReceiver
    {
        // Expand for each type of gamescene
        [SerializeField] private Scriptable_GameplayScene[] gameplayScene_draggable = null;
        [SerializeField] private Scriptable_MenuScene[] menuScene_draggable = null;


        private bool is_current_scene_dirty = true;
        private string current_scene_name = string.Empty;
        private Scriptable_IGameScene current_scene = null;

        public bool IsInitialized { get; private set; } = false;

        public Dictionary<MenuType, Scriptable_MenuScene> MenuSceneDatabase { get; private set; } = null;
        public Dictionary<string, Scriptable_GameplayScene> GameplaySceneDatabase { get; private set; } = null;
        public string CurrentSceneName
        {
            get
            {
                return current_scene_name;
            }

            set
            {
                is_current_scene_dirty = true;
                current_scene_name = value;
            }
        }

        public Scriptable_IGameScene CurrentScene 
        { 
            get
            {
                if (is_current_scene_dirty)
                {
                    current_scene = FindCurrentScene();
                    is_current_scene_dirty = false;
                }

                return current_scene;
            }
        }

        public void IntializeDatabase()
        {
            if (IsInitialized) return;
            MenuSceneDatabase = new Dictionary<MenuType, Scriptable_MenuScene>();
            GameplaySceneDatabase = new Dictionary<string, Scriptable_GameplayScene>();

            foreach (var gameplayScene in gameplayScene_draggable)
            {
                GameplaySceneDatabase.Add(gameplayScene.scene_name, gameplayScene);
            }

            foreach (var menuScene in menuScene_draggable)
            {
                MenuSceneDatabase.Add(menuScene.type, menuScene);
            }

            IsInitialized = true;
        }

        public void OnAfterDeserialize()
        {
        }

        public void OnBeforeSerialize()
        {
            IsInitialized = false;
        }

        // Without _root prefix
        private Scriptable_IGameScene FindCurrentScene()
        {
            if (GameplaySceneDatabase.TryGetValue(current_scene_name, out var gameplayScene))
            {
                return gameplayScene;
            }
            else
            {
                foreach (var menuScene in MenuSceneDatabase.Values)
                {
                    if (menuScene.scene_name == current_scene_name)
                        return menuScene;
                }
            }

            return null;

        }
    }
}