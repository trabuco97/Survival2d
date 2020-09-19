using System;

namespace Survival2D.SceneManagement
{

    public delegate void SceneMethod(SceneEventArgs args);


    public class SceneEventArgs : EventArgs
    {
        public Scriptable_IGameScene SceneData { get; private set; } = null;

        public SceneEventArgs(Scriptable_IGameScene scene_data)
        {
            SceneData = scene_data;
        }
    }
}