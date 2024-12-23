﻿using System;
using System.Collections.Generic;
using System.Linq;
using R3;
using R3.Triggers;
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

        private readonly Queue<TrackPartEntity>                   _trackParts                        = new();
        private readonly Dictionary<TrackPartEntity, IDisposable> _trackPartsRespawningSubscriptions = new();

        private Vector3 _trackPartSpawnOffset;
        private Vector3 _trackPartSpawnPosition;
        private int     _initialNumberOfTrackParts;

        public void Initialize()
        {
            _trackPartSpawnOffset      = _trackPartFactory.Prefab.Body.forward * _trackPartFactory.Prefab.Body.Length();
            _trackPartSpawnPosition    = _configProvider.Config.SpawnOrigin;
            _initialNumberOfTrackParts = (int)(_configProvider.Config.Length / _trackPartFactory.Prefab.Body.Length());
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
            foreach (TrackPartEntity trackPart in _trackParts)
            {
                UnsubscribeToRespawningTrackParts(trackPart);
                Despawn(trackPart);
            }
        }

        private TrackPartEntity SpawnTrackPart()
        {
            TrackPartEntity trackPart = _trackPartFactory.Create();
            trackPart.transform.position =  _trackPartSpawnPosition;
            _trackPartSpawnPosition      += _trackPartSpawnOffset;

            SubscribeToRespawningTrackParts(trackPart);
            _trackParts.Enqueue(trackPart);

            _collectableCubeSpawner.Spawn(trackPart);

            return trackPart;
        }

        private List<TrackPartEntity> SpawnTrackParts(int count) =>
            Enumerable.Range(start: default, count).Select(index => SpawnTrackPart()).ToList();

        private void SubscribeToRespawningTrackParts(TrackPartEntity trackPart)
        {
            if (!_trackPartsRespawningSubscriptions.ContainsKey(trackPart))
                _trackPartsRespawningSubscriptions.Add(trackPart, Disposable.Combine(
                    trackPart.Triggers.SpawnNewTrackPart.OnTriggerEnterAsObservable()
                        .Subscribe(_ => SpawnTrackPart()),
                    trackPart.Triggers.DestroyLastTrackPart.OnTriggerEnterAsObservable()
                        .Subscribe(_ => DespawnLastTrackPart())
                ));
        }

        private void UnsubscribeToRespawningTrackParts(TrackPartEntity trackPart)
        {
            if (_trackPartsRespawningSubscriptions.Remove(trackPart, out IDisposable subscription))
                subscription.Dispose();
        }

        private void Despawn(TrackPartEntity trackPart)
        {
            _collectableCubeSpawner.Despawn(trackPart);
            _trackPartFactory.Release(trackPart);
        }

        private void DespawnLastTrackPart() => Despawn(_trackParts.Dequeue());
    }
}