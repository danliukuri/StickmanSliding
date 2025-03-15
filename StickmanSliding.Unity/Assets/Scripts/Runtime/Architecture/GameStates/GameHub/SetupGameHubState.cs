using Cysharp.Threading.Tasks;
using StickmanSliding.Features.Background;
using StickmanSliding.Features.Player.GameHub;
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

        [Inject] private readonly IGameObjectFactory<PlayerGameHubEntity> _playerFactory;

        public async UniTask Enter()
        {
            await InitializeServices();

            PlacePlayer();
        }

        private UniTask InitializeServices()
        {
            _backgroundColorChanger.Initialize(_randomizer.NextHue());

            return _playerFactory.Initialize();
        }

        private void PlacePlayer() => _playerFactory.Create();
    }
}