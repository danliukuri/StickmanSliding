using Cysharp.Threading.Tasks;
using StickmanSliding.Data.Static.Configuration;
using StickmanSliding.Features.Player;
using StickmanSliding.Features.Track;
using StickmanSliding.Infrastructure.AssetLoading.Configuration;
using StickmanSliding.Infrastructure.ObjectCreation;
using StickmanSliding.Utilities.Patterns.State.Machines;
using StickmanSliding.Utilities.Patterns.State.Types;
using Zenject;

namespace StickmanSliding.Architecture.GameStates.Gameplay
{
    public class SetupGameplayState : IAsyncEnterableState
    {
        [Inject] private readonly IStateMachine _gameStateMachine;

        [Inject] private readonly IConfigLoader<TrackSpawnerConfig> _trackPartSpawnerConfigLoader;
        [Inject] private readonly IGameObjectFactory<TrackPart>     _trackPartFactory;
        [Inject] private readonly ITrackSpawner                     _trackSpawner;

        [Inject] private readonly IConfigLoader<PlayerConfig> _playerConfigLoader;
        [Inject] private readonly IGameObjectFactory<Player>  _playerFactory;
        [Inject] private readonly IPlayerProvider             _playerProvider;


        public async UniTask Enter()
        {
            await LoadAssets();
            await InitializeTrack();
            await InitializePlayer();
        }

        private UniTask LoadAssets() => UniTask.WhenAll(
            _trackPartSpawnerConfigLoader.Load(),
            _playerConfigLoader.Load()
        );

        private async UniTask InitializeTrack()
        {
            await _trackPartFactory.Initialize();
            _trackSpawner.Initialize();
            _trackSpawner.Spawn();
        }

        private async UniTask InitializePlayer()
        {
            await _playerFactory.Initialize();
            Player player = _playerFactory.Create();
            _playerProvider.Initialize(player);
        }
    }
}