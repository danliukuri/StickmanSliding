using System.Collections.Generic;
using System.Linq;
using Cysharp.Threading.Tasks;
using StickmanSliding.Data.Dynamic;
using StickmanSliding.Data.Static.Configuration;
using StickmanSliding.Infrastructure.AssetLoading.Configuration;
using StickmanSliding.Infrastructure.ObjectCreation;
using UnityEngine;
using Zenject;

namespace StickmanSliding.Features.Track
{
    public class TrackPartSpawner : IStateProvider<TrackPartSpawnerState>, ITrackPartSpawner
    {
        [Inject] private readonly IGameObjectFactory<TrackPart>         _trackPartFactory;
        [Inject] private readonly IConfigLoader<TrackPartSpawnerConfig> _configLoader;

        private Vector3 _spawnOffset;

        public TrackPartSpawnerState State { get; private set; }

        public async UniTask Initialize()
        {
            TrackPartSpawnerConfig config = await _configLoader.Load();
            _spawnOffset = Vector3.Scale(config.TrackDirection, _trackPartFactory.Prefab.transform.lossyScale);
            var initialNumberOfTrackParts = (int)(config.TrackLength / _spawnOffset.magnitude);
            State = new TrackPartSpawnerState(config.SpawnOrigin);
            _configLoader.Release();
            
            Spawn(initialNumberOfTrackParts);
        }

        public TrackPart Spawn()
        {
            TrackPart trackPart = _trackPartFactory.Create();
            State.CurrentSpawnPosition += _spawnOffset;
            return trackPart;
        }

        public List<TrackPart> Spawn(int count) => Enumerable.Range(default, count).Select(index => Spawn()).ToList();
    }
}