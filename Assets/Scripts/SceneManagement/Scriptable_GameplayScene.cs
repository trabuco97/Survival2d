using UnityEngine;
using System.Collections;

namespace Survival2D.SceneManagement
{
    // scene naming convention, for .unity files
    // <> = scene_name
    // <>_root = first scene_toload
    // <>_sub_00 = subscene to load

    [CreateAssetMenu(fileName = "GameplayScene", menuName = "Custom/Scene/Gameplay")]
    public class Scriptable_GameplayScene : Scriptable_IGameScene
    {

    }
}