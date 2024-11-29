using Cysharp.Threading.Tasks;
using StickmanSliding.Features.Track;
using StickmanSliding.Infrastructure.ObjectCreation;
using StickmanSliding.Utilities.Patterns.State.Types;
using Zenject;

namespace StickmanSliding.Architecture.GameStates.Gameplay
{
    public class SetupGameplayState : IAsyncEnterableState
    {
        [Inject] private readonly IPooledGameObjectFactory<TrackPart> _trackPartFactory;
        [Inject] private readonly ITrackPartSpawner                   _trackPartSpawner;

        public async UniTask Enter()
        {
            await _trackPartFactory.Initialize();
            await _trackPartSpawner.Initialize();
        }
    }
}