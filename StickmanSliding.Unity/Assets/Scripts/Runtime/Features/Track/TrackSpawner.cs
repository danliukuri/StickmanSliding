using System.Collections.Generic;
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
        [Inject] private readonly IConfigProvider<TrackSpawnerConfig> _configProvider;

        [Inject] private readonly IGameObjectFactory<TrackPartEntity>        _trackPartFactory;
        [Inject] private readonly IGameObjectFactory<InitialTrackPartEntity> _initialTrackPartFactory;

        [Inject] private readonly ITrackPartSpawningSubscriber _trackPartSpawningSubscriber;
        [Inject] private readonly ICollectableCubeSpawner      _collectableCubeSpawner;

        private readonly Queue<ITrackPart> _trackParts = new();

        private Vector3 _trackSpawnPosition;
        private Vector3 _trackPartSpawnPosition;

        public void Initialize() => _trackSpawnPosition = _configProvider.Config.SpawnOrigin;

        public void Dispose()
        {
            Despawn();
            _initialTrackPartFactory.Dispose();
            _trackPartFactory.Dispose();
        }

        public void Spawn()
        {
            SpawnInitialTrackPart();
            while (_trackPartSpawnPosition.magnitude - _trackSpawnPosition.magnitude < _configProvider.Config.Length)
                SpawnTrackPart();
        }

        public void Despawn()
        {
            _trackPartSpawningSubscriber.Dispose();
            foreach (ITrackPart trackPart in _trackParts)
                Despawn(trackPart);
        }

        private InitialTrackPartEntity SpawnInitialTrackPart()
        {
            InitialTrackPartEntity trackPart = _initialTrackPartFactory.Create();
            PlaceTrackPart(trackPart);
            _trackParts.Enqueue(trackPart);
            return trackPart;
        }

        private TrackPartEntity SpawnTrackPart()
        {
            TrackPartEntity trackPart = _trackPartFactory.Create();
            PlaceTrackPart(trackPart);
            _collectableCubeSpawner.Spawn(trackPart);

            _trackPartSpawningSubscriber.SubscribeToSpawnTriggerEnter(trackPart, _ => SpawnTrackPart());
            _trackPartSpawningSubscriber.SubscribeToDespawnTriggerEnter(trackPart, _ => DespawnLastTrackPart());
            _trackParts.Enqueue(trackPart);

            return trackPart;
        }

        private void Despawn(InitialTrackPartEntity trackPart) => _initialTrackPartFactory.Release(trackPart);

        private void Despawn(TrackPartEntity trackPart)
        {
            _collectableCubeSpawner.Despawn(trackPart);
            _trackPartFactory.Release(trackPart);
        }

        private void Despawn(ITrackPart trackPart)
        {
            switch (trackPart)
            {
                case InitialTrackPartEntity initialTrackPart:
                    Despawn(initialTrackPart);
                    break;
                case TrackPartEntity trackPartEntity:
                    Despawn(trackPartEntity);
                    break;
            }
        }

        private void DespawnLastTrackPart() => Despawn(_trackParts.Dequeue());

        private void PlaceTrackPart(ITrackPart trackPart)
        {
            _trackPartSpawnPosition      += trackPart.Body.HalfLengthVector();
            trackPart.Transform.position =  _trackSpawnPosition + _trackPartSpawnPosition;
            _trackPartSpawnPosition      += trackPart.Body.HalfLengthVector();
        }
    }
}