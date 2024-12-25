using System.Collections.Generic;
using System.Linq;
using StickmanSliding.Data.Static.Configuration;
using StickmanSliding.Features.CollectableCube;
using StickmanSliding.Infrastructure.AssetLoading.Configuration;
using StickmanSliding.Infrastructure.ObjectCreation;
using StickmanSliding.Utilities.Extensions;
using UnityEngine;
using Zenject;

namespace StickmanSliding.Features.Track
{
    public class TrackSpawner : ITrackSpawner
    {
        [Inject] private readonly IGameObjectFactory<TrackPartEntity> _trackPartFactory;
        [Inject] private readonly IConfigProvider<TrackSpawnerConfig> _configProvider;
        [Inject] private readonly ICollectableCubeSpawner             _collectableCubeSpawner;
        [Inject] private readonly ITrackPartSpawningSubscriber        _trackPartSpawningSubscriber;

        private readonly Queue<TrackPartEntity> _trackParts = new();

        private Vector3 _trackPartSpawnOffset;
        private Vector3 _trackPartSpawnPosition;
        private int     _initialNumberOfTrackParts;

        public void Initialize()
        {
            _trackPartSpawnOffset      = _trackPartFactory.Prefab.Body.LengthVector();
            _trackPartSpawnPosition    = _configProvider.Config.SpawnOrigin;
            _initialNumberOfTrackParts = (int)(_configProvider.Config.Length / _trackPartFactory.Prefab.Body.Length());
        }

        public void Dispose()
        {
            _trackPartFactory.Dispose();
            _trackPartSpawningSubscriber.Dispose();
        }

        public void Spawn()
        {
            SpawnTrackParts(_initialNumberOfTrackParts);

            _trackPartSpawningSubscriber.UnsubscribeToSpawnTriggerEnter(_trackParts.Peek());
            _trackPartSpawningSubscriber.UnsubscribeToDespawnTriggerEnter(_trackParts.Peek());
        }

        public void Despawn()
        {
            foreach (TrackPartEntity trackPart in _trackParts)
            {
                _trackPartSpawningSubscriber.UnsubscribeToSpawnTriggerEnter(trackPart);
                _trackPartSpawningSubscriber.UnsubscribeToDespawnTriggerEnter(trackPart);
                Despawn(trackPart);
            }
        }

        private TrackPartEntity SpawnTrackPart()
        {
            TrackPartEntity trackPart = _trackPartFactory.Create();
            trackPart.transform.position =  _trackPartSpawnPosition;
            _trackPartSpawnPosition      += _trackPartSpawnOffset;

            _trackPartSpawningSubscriber.SubscribeToSpawnTriggerEnter(trackPart, _ => SpawnTrackPart());
            _trackPartSpawningSubscriber.SubscribeToDespawnTriggerEnter(trackPart, _ => DespawnLastTrackPart());
            _trackParts.Enqueue(trackPart);

            _collectableCubeSpawner.Spawn(trackPart);

            return trackPart;
        }

        private List<TrackPartEntity> SpawnTrackParts(int count) =>
            Enumerable.Range(start: default, count).Select(index => SpawnTrackPart()).ToList();

        private void Despawn(TrackPartEntity trackPart)
        {
            _collectableCubeSpawner.Despawn(trackPart);
            _trackPartFactory.Release(trackPart);
        }

        private void DespawnLastTrackPart() => Despawn(_trackParts.Dequeue());
    }
}