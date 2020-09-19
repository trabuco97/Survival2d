using UnityEngine;
using Cinemachine;

using Survival2D.Entities.Player;

namespace Survival2D.Cam
{
    public class PlayerCamera : IPlayerListener
    {
        [SerializeField] private CinemachineVirtualCamera player_camera = null;

        protected override void InitializeBehaviour()
        {
            player_camera.Follow = PlayerEntity.transform;
        }
    }
}