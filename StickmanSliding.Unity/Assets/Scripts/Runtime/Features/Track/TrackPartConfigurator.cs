using System;
using StickmanSliding.Features.CollectableCube;
using StickmanSliding.Infrastructure.ObjectCreation;
using UnityEngine;
using Zenject;

namespace StickmanSliding.Features.Track
{
    public class TrackPartConfigurator : IGameObjectConfigurator<TrackPartEntity>,
                                         IGameObjectResetter<TrackPartEntity>,
                                         ITrackPartConfigurator
    {
        [Inject] private readonly ITrackPartPlacer             _trackPartPlacer;
        [Inject] private readonly ICollectableCubeSpawner      _collectableCubeSpawner;
        [Inject] private readonly ITrackPartSpawningSubscriber _trackPartSpawningSubscriber;

        private Action<Collider> _spawnAction;
        private Action<Collider> _despawnAction;

        public void Initialize(Action<Collider> spawnAction, Action<Collider> despawnAction)
        {
            _despawnAction = despawnAction;
            _spawnAction   = spawnAction;
        }

        public void Dispose() => _trackPartSpawningSubscriber.Dispose();

        public void Configure(TrackPartEntity trackPart)
        {
            _trackPartPlacer.Place(trackPart);
            _collectableCubeSpawner.Spawn(trackPart);

            _trackPartSpawningSubscriber.SubscribeToSpawnTriggerEnter(trackPart, _spawnAction);
            _trackPartSpawningSubscriber.SubscribeToDespawnTriggerEnter(trackPart, _despawnAction);
        }

        public void Reset(TrackPartEntity trackPart) => _collectableCubeSpawner.Despawn(trackPart);
    }
}