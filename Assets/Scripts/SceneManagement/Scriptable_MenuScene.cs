using UnityEngine;
using System.Collections;

namespace Survival2D.SceneManagement
{
    public enum MenuType
    {
        MainMenu    = 0,
        PauseMenu   = 1,
        Example001  = 999
    }

    [CreateAssetMenu(fileName = "MenuScene", menuName = "Custom/Scene/Menu")]
    public class Scriptable_MenuScene : Scriptable_IGameScene
    {
        // Use this for initialization
        [Header("Menu Fields")]
        public MenuType type;

    }
}