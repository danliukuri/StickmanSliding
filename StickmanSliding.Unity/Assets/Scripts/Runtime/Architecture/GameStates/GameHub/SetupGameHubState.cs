using Cysharp.Threading.Tasks;
using StickmanSliding.Data.Static.Configuration;
using StickmanSliding.Features.Background;
using StickmanSliding.Features.Player.GameHub;
using StickmanSliding.Infrastructure.AssetLoading.Configuration;
using StickmanSliding.Infrastructure.InputServices;
using StickmanSliding.Infrastructure.ObjectCreation;
using StickmanSliding.Infrastructure.Randomization;
using StickmanSliding.Utilities.Patterns.State.Types;
using Zenject;

namespace StickmanSliding.Architecture.GameStates.GameHub
{
    public class SetupGameHubState : IAsyncEnterableState
    {
        [Inject] private readonly IBackgroundColorChanger _backgroundColorChanger;
        [Inject] private readonly IRandomizer             _randomizer;

        [Inject] private readonly IConfigLoader<PlayerGameHubConfig>      _playerConfigLoader;
        [Inject] private readonly IGameObjectFactory<PlayerGameHubEntity> _playerFactory;
        [Inject] private readonly IPlayerProvider                         _playerProvider;

        [Inject] private readonly IRotateInputService _rotateInputService;

        public async UniTask Enter()
        {
            await LoadAssets();
            await InitializeServices();

            PlacePlayer();
        }

        private UniTask LoadAssets() => _playerConfigLoader.Load();

        private UniTask InitializeServices()
        {
            _backgroundColorChanger.Initialize(_randomizer.NextHue());

            return UniTask.WhenAll(
                _playerFactory.Initialize(),
                _rotateInputService.Initialize()
            );
        }

        private void PlacePlayer() => _playerProvider.Initialize(_playerFactory.Create());
    }
}