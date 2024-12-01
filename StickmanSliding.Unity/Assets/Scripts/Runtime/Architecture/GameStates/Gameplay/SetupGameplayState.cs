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
        [Inject] private readonly IStateMachine                 _gameStateMachine;
        [Inject] private readonly IGameObjectFactory<TrackPart> _trackPartFactory;
        [Inject] private readonly ITrackPartSpawner             _trackPartSpawner;
        [Inject] private readonly IGameObjectFactory<Player>    _playerFactory;
        [Inject] private readonly IConfigLoader<PlayerConfig>   _playerConfigLoader;

        public async UniTask Enter()
        {
            await InitializeTrack();
            Player player = await InitializePlayer();

            _gameStateMachine.ChangeState<ProcessGameplayState, Player>(player).Forget();
        }

        private UniTask InitializeTrack() => _trackPartFactory.Initialize().ContinueWith(_trackPartSpawner.Initialize);

        private async UniTask<Player> InitializePlayer()
        {
            await _playerFactory.Initialize();
            Player player = _playerFactory.Create();
            await player.Initialize();
            return player;
        }
    }
}