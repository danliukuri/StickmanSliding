using Cysharp.Threading.Tasks;
using StickmanSliding.Features.Player;
using StickmanSliding.Features.Track;
using StickmanSliding.Infrastructure.ObjectCreation;
using StickmanSliding.Utilities.Patterns.State.Types;
using Zenject;

namespace StickmanSliding.Architecture.GameStates.Gameplay
{
    public class SetupGameplayState : IAsyncEnterableState
    {
        [Inject] private readonly IGameObjectFactory<TrackPart> _trackPartFactory;
        [Inject] private readonly ITrackPartSpawner             _trackPartSpawner;
        [Inject] private readonly IGameObjectFactory<Player>    _playerFactory;

        public async UniTask Enter()
        {
            await _trackPartFactory.Initialize();
            await _trackPartSpawner.Initialize();
            await _playerFactory.Initialize();

            _playerFactory.Create();
        }
    }
}