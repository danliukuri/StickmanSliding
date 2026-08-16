using System;
using StickmanSliding.Architecture.GameStates.Gameplay;
using StickmanSliding.Features.CollectableCube;
using StickmanSliding.Features.Player;
using StickmanSliding.Features.Track;
using Zenject;

namespace StickmanSliding.Features.ObstacleCube
{
    public class PlayerCollisionHandler : IPlayerCollisionHandler, IGameplayFinishingInformer
    {
        public event Action GameplayFinished;

        [Inject] private readonly IPlayerCubeDetacher _playerCubeDetacher;

        public void HandleEnter((PlayerEntity Entity, UnityEngine.Collision Collision) player, TrackPartEntity track)
        {
            var cube = player.Collision.transform.GetComponentInParent<CollectableCubeEntity>();

            if (cube == default)
                GameplayFinished?.Invoke();
            else if (_playerCubeDetacher.IsCollisionFromDetachableDirection(player.Collision))
                _playerCubeDetacher.Detach(player.Entity, cube, track);
        }
    }
}