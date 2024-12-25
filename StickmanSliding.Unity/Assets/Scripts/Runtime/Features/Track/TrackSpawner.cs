using StickmanSliding.Data.Static.Configuration;
using StickmanSliding.Infrastructure.AssetLoading.Configuration;
using Zenject;

namespace StickmanSliding.Features.Track
{
    public class TrackSpawner : ITrackSpawner
    {
        [Inject] private readonly IConfigProvider<TrackSpawnerConfig> _configProvider;
        [Inject] private readonly ITrackPartSpawner                   _trackPartSpawner;
        [Inject] private readonly ITrackPartPlacer                    _trackPartPlacer;

        public void Initialize()
        {
            _trackPartSpawner.Initialize();
            _trackPartPlacer.Initialize(_configProvider.Config.SpawnOrigin);
        }

        public void Dispose() => _trackPartSpawner.Dispose();

        public void Spawn()
        {
            _trackPartSpawner.SpawnInitial();

            while (CalculateTrackLength() < _configProvider.Config.Length)
                _trackPartSpawner.Spawn();
        }

        private float CalculateTrackLength() =>
            (_trackPartPlacer.SpawnPosition - _configProvider.Config.SpawnOrigin).magnitude;
    }
}