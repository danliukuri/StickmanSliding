using System;
using System.Collections.Generic;
using System.Linq;
using R3;
using R3.Triggers;
using StickmanSliding.Data.Static.Configuration;
using StickmanSliding.Infrastructure.AssetLoading.Configuration;
using StickmanSliding.Infrastructure.ObjectCreation;
using UnityEngine;
using Zenject;

namespace StickmanSliding.Features.Track
{
    public class TrackSpawner : ITrackSpawner
    {
        [Inject] private readonly IGameObjectFactory<TrackPart>       _trackPartFactory;
        [Inject] private readonly IConfigProvider<TrackSpawnerConfig> _configProvider;

        private readonly Queue<TrackPart>                   _trackParts                        = new();
        private readonly Dictionary<TrackPart, IDisposable> _trackPartsRespawningSubscriptions = new();

        private Vector3 _trackPartSpawnOffset;
        private Vector3 _trackPartSpawnPosition;
        private int     _initialNumberOfTrackParts;

        public void Initialize()
        {
            _trackPartSpawnOffset =
                Vector3.Scale(_configProvider.Config.Direction, _trackPartFactory.Prefab.Body.lossyScale);

            _trackPartSpawnPosition = _configProvider.Config.SpawnOrigin;

            float trackPartLength =
                Vector3.Dot(_configProvider.Config.Direction, _trackPartFactory.Prefab.Body.lossyScale);
            _initialNumberOfTrackParts = (int)(_configProvider.Config.Length / trackPartLength);
        }

        public void Dispose()
        {
            _trackPartFactory.Dispose();
            foreach (IDisposable subscription in _trackPartsRespawningSubscriptions.Values)
                subscription.Dispose();
            _trackPartsRespawningSubscriptions.Clear();
        }

        public void Spawn()
        {
            SpawnTrackParts(_initialNumberOfTrackParts);
            UnsubscribeToRespawningTrackParts(_trackParts.Peek());
        }

        public void Despawn()
        {
            foreach (TrackPart trackPart in _trackParts)
            {
                UnsubscribeToRespawningTrackParts(trackPart);
                Despawn(trackPart);
            }
        }

        private TrackPart SpawnTrackPart()
        {
            TrackPart trackPart = _trackPartFactory.Create();
            trackPart.transform.position =  _trackPartSpawnPosition;
            _trackPartSpawnPosition      += _trackPartSpawnOffset;

            SubscribeToRespawningTrackParts(trackPart);
            _trackParts.Enqueue(trackPart);

            return trackPart;
        }

        private List<TrackPart> SpawnTrackParts(int count) =>
            Enumerable.Range(default, count).Select(index => SpawnTrackPart()).ToList();

        private void SubscribeToRespawningTrackParts(TrackPart trackPart)
        {
            if (!_trackPartsRespawningSubscriptions.ContainsKey(trackPart))
                _trackPartsRespawningSubscriptions.Add(trackPart, Disposable.Combine(
                    trackPart.Triggers.SpawnNewTrackPart.OnTriggerEnterAsObservable()
                        .Subscribe(_ => SpawnTrackPart()),
                    trackPart.Triggers.DestroyLastTrackPart.OnTriggerEnterAsObservable()
                        .Subscribe(_ => DespawnLastTrackPart())
                ));
        }

        private void UnsubscribeToRespawningTrackParts(TrackPart trackPart)
        {
            if (_trackPartsRespawningSubscriptions.Remove(trackPart, out IDisposable subscription))
                subscription.Dispose();
        }

        private void Despawn(TrackPart trackPart) => _trackPartFactory.Release(trackPart);

        private void DespawnLastTrackPart() => Despawn(_trackParts.Dequeue());
    }
}