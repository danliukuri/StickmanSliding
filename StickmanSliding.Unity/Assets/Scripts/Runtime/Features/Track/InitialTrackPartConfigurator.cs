using StickmanSliding.Infrastructure.ObjectCreation;
using Zenject;

namespace StickmanSliding.Features.Track
{
    public class InitialTrackPartConfigurator : IGameObjectConfigurator<InitialTrackPartEntity>
    {
        [Inject] private readonly ITrackPartPlacer _trackPartPlacer;

        public void Configure(InitialTrackPartEntity trackPart) => _trackPartPlacer.Place(trackPart);
    }
}