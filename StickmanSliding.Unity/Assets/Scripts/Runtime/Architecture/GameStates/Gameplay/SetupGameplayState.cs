using Cysharp.Threading.Tasks;
using StickmanSliding.Data.Static.Configuration;
using StickmanSliding.Features.CollectableCube;
using StickmanSliding.Features.ObstacleCube;
using StickmanSliding.Features.Player;
using StickmanSliding.Features.Track;
using StickmanSliding.Infrastructure.AssetLoading.Configuration;
using StickmanSliding.Infrastructure.InputServices;
using StickmanSliding.Infrastructure.ObjectCreation;
using StickmanSliding.Utilities.Patterns.State.Machines;
using StickmanSliding.Utilities.Patterns.State.Types;
using Zenject;

namespace StickmanSliding.Architecture.GameStates.Gameplay
{
    public class SetupGameplayState : IAsyncEnterableState
    {
        [Inject] private readonly IStateMachine _gameStateMachine;

        [Inject] private readonly IConfigLoader<TrackSpawnerConfig>          _trackPartSpawnerConfigLoader;
        [Inject] private readonly IGameObjectFactory<InitialTrackPartEntity> _initialTrackPartFactory;
        [Inject] private readonly IGameObjectFactory<TrackPartEntity>        _trackPartFactory;
        [Inject] private readonly ITrackSpawner                              _trackSpawner;

        [Inject] private readonly IConfigLoader<PlayerConfig>              _playerConfigLoader;
        [Inject] private readonly IConfigLoader<PlayerCubeDetachingConfig> _playerCubeDetachingConfigLoader;
        [Inject] private readonly IConfigLoader<TimeDependentConfig>       _timeDependentConfigLoader;
        [Inject] private readonly IGameObjectFactory<PlayerEntity>         _playerFactory;
        [Inject] private readonly IPlayerProvider                          _playerProvider;

        [Inject] private readonly IConfigLoader<WallObstacleSpawnerConfig> _wallObstacleSpawnerConfigLoader;
        [Inject] private readonly IConfigLoader<CubeObstacleConfig>        _cubeObstacleConfigLoader;
        [Inject] private readonly IGameObjectFactory<ObstacleCubeEntity>   _obstacleCubeFactory;

        [Inject] private readonly IGameObjectFactory<CollectableCubeEntity> _collectableCubeFactory;

        [Inject] private readonly IMoveInputService _moveInputService;

        public async UniTask Enter()
        {
            await LoadAssets();
            await InitializeServices();

            PlaceTrack();
            PlacePlayer();
        }

        private UniTask LoadAssets() => UniTask.WhenAll(
            _trackPartSpawnerConfigLoader.Load(),
            _playerConfigLoader.Load(),
            _playerCubeDetachingConfigLoader.Load(),
            _timeDependentConfigLoader.Load(),
            _wallObstacleSpawnerConfigLoader.Load(),
            _cubeObstacleConfigLoader.Load()
        );

        private UniTask InitializeServices() => UniTask.WhenAll(
            _initialTrackPartFactory.Initialize(),
            _trackPartFactory.Initialize(),
            _playerFactory.Initialize(),
            _obstacleCubeFactory.Initialize(),
            _collectableCubeFactory.Initialize(),
            _moveInputService.Initialize()
        );

        private void PlaceTrack()
        {
            _trackSpawner.Initialize();
            _trackSpawner.Spawn();
        }

        private void PlacePlayer()
        {
            PlayerEntity player = _playerFactory.Create();
            _playerProvider.Initialize(player);
            player.CubeSpawner.SpawnInitial();
        }
    }
}