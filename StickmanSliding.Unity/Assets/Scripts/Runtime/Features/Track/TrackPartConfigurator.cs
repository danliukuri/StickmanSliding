using StickmanSliding.Data.Dynamic;
using StickmanSliding.Infrastructure.ObjectCreation;
using Zenject;

namespace StickmanSliding.Features.Track
{
    public class TrackPartConfigurator : IGameObjectConfigurator<TrackPart>
    {
        [Inject] private readonly IStateProvider<TrackPartSpawnerState> _spawnerStateProvider;

        public void Configure(TrackPart trackPart) =>
            trackPart.transform.position = _spawnerStateProvider.State.CurrentSpawnPosition;
    }
}