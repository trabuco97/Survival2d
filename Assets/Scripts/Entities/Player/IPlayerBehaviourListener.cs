using UnityEngine;

namespace Survival2D.Entities.Player
{
    // All behaviours that require the player gameobject derive from this class
    public abstract class IPlayerListener : MonoBehaviour
    {
        [SerializeField] private PlayerSceneBinder player_scene_binder = null;
        protected GameObject Player { get; private set; } = null;

        protected virtual void Awake()
        {
#if UNITY_EDITOR
            if (player_scene_binder == null)
            {
                Debug.LogWarning($"{nameof(player_scene_binder)} is not assigned to {nameof(IPlayerListener)} of {gameObject.GetFullName()}");
            }
#endif
            player_scene_binder.OnPlayerBinded += CallbackPlayerBinded;
        }

        protected virtual void OnDestroy()
        {
            player_scene_binder.OnPlayerBinded -= CallbackPlayerBinded;
        }

        protected abstract void InitializeBehaviour();

        private void CallbackPlayerBinded(EntityEventArgs args)
        {
            Player = args.EntityObject;
            InitializeBehaviour();
        }

    }

    // All behaviours in the scene that requieres components from the player derive from this class
    public abstract class IPlayerBehaviourListener<T> : MonoBehaviour where T : MonoBehaviour
    {
        [SerializeField] private PlayerSceneBinder player_scene_binder = null;

        /// <summary>
        /// the behaviour is inicialized when the player is bind
        /// </summary>
        protected T Behaviour { get; private set; } = null;

        protected virtual void Awake()
        {
#if UNITY_EDITOR
            if (player_scene_binder == null)
            {
                Debug.LogWarning($"{nameof(player_scene_binder)} is not assigned to {nameof(IPlayerBehaviourListener<T>)} of {name}");
            }
#endif
            player_scene_binder.OnPlayerBinded += CallbackPlayerBinded;
        }

        protected virtual void OnDestroy()
        {
            player_scene_binder.OnPlayerBinded -= CallbackPlayerBinded;
        }

        protected abstract void InitializeBehaviour();

        private T GetBehaviour()
        {
            if (player_scene_binder.RequestBehaviour(out T behaviour))
            {

                return behaviour;
            }
            else
            {
#if UNITY_EDITOR
                Debug.Log($"this concrete implementation of IPlayerBehaviourListener request a behaviour that the player doesnt have , <{nameof(T)}>");
#endif
                return null;
            }
        }

        private void CallbackPlayerBinded(EntityEventArgs args)
        {
            Behaviour = GetBehaviour();
            InitializeBehaviour();
        }
    }
    // used in behaviour for two behaviours
    public abstract class IPlayerBehaviourListener_TwoArgs<T1, T2> : MonoBehaviour where T1 : MonoBehaviour where T2 : MonoBehaviour
    {
        [SerializeField] private PlayerSceneBinder player_scene_binder = null;

        /// <summary>
        /// the behaviour is inicialized when the player is bind
        /// </summary>
        protected T1 Behaviour1 { get; private set; } = null;
        protected T2 Behaviour2 { get; private set; } = null;

        protected virtual void Awake()
        {
#if UNITY_EDITOR
            if (player_scene_binder == null)
            {
                Debug.LogWarning($"{nameof(player_scene_binder)} is not assigned to {nameof(IPlayerBehaviourListener_TwoArgs<T1, T2>)} of {name}");
            }
#endif
            player_scene_binder.OnPlayerBinded += CallbackPlayerBinded;
        }

        protected virtual void OnDestroy()
        {
            player_scene_binder.OnPlayerBinded -= CallbackPlayerBinded;
        }

        protected abstract void InitializeBehaviour();

        private TArgs GetBehaviour<TArgs>() where TArgs : MonoBehaviour
        {
            if (player_scene_binder.RequestBehaviour(out TArgs behaviour))
            {
                return behaviour;
            }
            else
            {
#if UNITY_EDITOR
                Debug.Log($"this concrete implementation of IPlayerBehaviourListener request a behaviour that the player doesnt have , <{nameof(TArgs)}>");
#endif
                return null;
            }
        }

        private void CallbackPlayerBinded(EntityEventArgs args)
        {
            Behaviour1 = GetBehaviour<T1>();
            Behaviour2 = GetBehaviour<T2>();
            InitializeBehaviour();
        }
    }
}