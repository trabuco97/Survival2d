using UnityEngine;
using System.Collections;

namespace Survival2D.Input
{
    public class InputClientManager : ISingletonBehaviour<InputClientManager>
    {
        private int current_selected = 0;
        private ClientInput[] scene_client_input = null;

        public ClientInput CurrentClient { get { return scene_client_input[current_selected]; } }

        protected override void Awake()
        {
            base.Awake();
            GetSceneInputs();
        }
        private void GetSceneInputs()
        {
            scene_client_input = FindObjectsOfType<ClientInput>();
        }


        protected override void SetInstace()
        {
            instance = this;
        }
    }
}