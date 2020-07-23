using UnityEngine.Events;

using Survival2D.Entities.Player;

namespace Survival2D.Input
{
    public class InputClientManager : IPlayerBehaviourListener<InputClient>
    {
        public InputClient CurrentClient { get { return Behaviour; } }
        public UnityEvent onClientInicialized { get; private set; } = new UnityEvent();

        public static InputClientManager Instance { get; private set; } = null;

        protected override void Awake()
        {
            Instance = this;
            base.Awake();
        }


        protected override void InicializeBehaviour()
        {
            onClientInicialized.Invoke();
        }
    }
}