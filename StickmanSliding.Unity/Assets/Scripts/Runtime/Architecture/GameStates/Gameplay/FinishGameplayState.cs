using System.Collections.Generic;
using System.Linq;
using StickmanSliding.Features.CollectableCube;
using StickmanSliding.Features.Player;
using StickmanSliding.Features.Track;
using StickmanSliding.UI.Features.Mediation;
using StickmanSliding.Utilities.Patterns.State.Types;
using Zenject;

namespace StickmanSliding.Architecture.GameStates.Gameplay
{
    public class FinishGameplayState : IEnterableState
    {
        [Inject] private readonly IPlayerProvider                     _playerProvider;
        [Inject] private readonly IMediator                           _uiMediator;
        [Inject] private readonly ITrackPartProvider                  _trackPartProvider;
        [Inject] private readonly ICollectableCubePhysicsConfigurator _physicsConfigurator;

        public void Enter()
        {
            ThrowPlayerAndCubesIfAny();

            _uiMediator.Notify(nameof(FinishGameplayState), nameof(Enter));
        }

        private void ThrowPlayerAndCubesIfAny()
        {
            PlayerEntity player = _playerProvider.Player;

            if (player == default)
                return;

            player.State.IsAlive.Value = false;

            ConfigureWorldCollectiblesAsObstacles();

            player.CubeThrower.ThrowAllInPlayerDirection();
            player.Ragdoll.Thrower.ThrowInPlayerDirection();
        }

        private void ConfigureWorldCollectiblesAsObstacles()
        {
            IEnumerable<CollectableCubeEntity> stationaryCubes = _trackPartProvider.TrackParts
                .SelectMany(trackPart => trackPart.State.CollectableCubes.Values)
                .Where(_physicsConfigurator.IsConfiguredAsWorldCollectible);

            foreach (CollectableCubeEntity cube in stationaryCubes)
                _physicsConfigurator.ConfigureAsObstacle(cube);
        }
    }
}