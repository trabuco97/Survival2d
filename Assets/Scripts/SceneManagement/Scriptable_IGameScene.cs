using System.Collections.Generic;

using UnityEngine;

namespace Survival2D.SceneManagement
{
    // TODO: Expand as development progress
    // Name of file same as SceneToLoad
    public abstract class Scriptable_IGameScene : ScriptableObject
    {
        public string scene_name;
        [TextArea] public string description;

        [Header("Music propierties")]
        public List<AudioClip> music_playlist;      // TODO: change when working with audio management


    }
}