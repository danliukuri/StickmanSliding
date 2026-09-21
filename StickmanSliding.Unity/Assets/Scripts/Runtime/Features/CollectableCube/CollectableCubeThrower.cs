using System.Linq;
using StickmanSliding.Data.Static.Configuration;
using StickmanSliding.Features.ObstacleCube;
using StickmanSliding.Features.Player;
using StickmanSliding.Infrastructure.AssetLoading.Configuration;
using UnityEngine;
using Zenject;

namespace StickmanSliding.Features.CollectableCube
{
    public class CollectableCubeThrower : ICollectableCubeThrower
    {
        [Inject] private readonly IConfigProvider<PlayerCubeDetachingConfig> _configProvider;
        [Inject] private readonly IPlayerCubeDetacher                        _playerCubeDetacher;
        [Inject] private readonly IPlayerDirectionProvider                   _playerDirectionProvider;
        [Inject] private readonly PlayerEntity                               _player;

        public void ThrowAllInPlayerDirection()
        {
            foreach (CollectableCubeEntity cube in _player.State.CollectedCubes.ToArray())
                ThrowInDirection(cube, _playerDirectionProvider.Direction);
        }

        private void ThrowInDirection(CollectableCubeEntity cube, Vector3 direction)
        {
            _playerCubeDetacher.Detach(_player, cube, trackPart: default);
            cube.Rigidbody.AddForce(direction.normalized * _configProvider.Config.ThrowForce, ForceMode.Acceleration);
        }
    }
}
