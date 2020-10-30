using System;

namespace Survival2D.SceneManagement
{
    [Serializable]
    public class GameplaySceneData
    {
        public string scene_name;
        public int[] specific_subscenes;
        public int[] general_subscenes;
    }
}