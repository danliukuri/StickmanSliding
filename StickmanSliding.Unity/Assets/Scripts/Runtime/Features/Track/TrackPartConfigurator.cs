using System;
using StickmanSliding.Features.CollectableCube;
using StickmanSliding.Features.ObstacleCube;
using StickmanSliding.Features.WallObstacle;
using StickmanSliding.Infrastructure.ObjectCreation;
using UnityEngine;
using Zenject;

namespace StickmanSliding.Features.Track
{
    public class TrackPartConfigurator : IGameObjectConfigurator<TrackPartEntity>,
                                         IGameObjectResetter<TrackPartEntity>,
                                         ITrackPartConfigurator
    {
        [Inject] private readonly ITrackPartPlacer                     _trackPartPlacer;
        [Inject] private readonly IWallObstacleSpawner                 _wallObstacleSpawner;
        [Inject] private readonly ICollectableCubeSpawner              _collectableCubeSpawner;
        [Inject] private readonly ITrackPartSpawningSubscriber         _trackPartSpawningSubscriber;
        [Inject] private readonly ITrackPartPlayerDespawningSubscriber _trackPartPlayerDespawningSubscriber;
        [Inject] private readonly IPlayerCubeDetachingSubscriber       _playerCubeDetachingSubscriber;

        private Action<Collider> _spawnAction;
        private Action<Collider> _despawnAction;

        public void Initialize(Action<Collider> spawnAction, Action<Collider> despawnAction)
        {
            _despawnAction = despawnAction;
            _spawnAction   = spawnAction;
        }

        public void Dispose()
        {
            _trackPartSpawningSubscriber.Dispose();
            _trackPartPlayerDespawningSubscriber.Dispose();
        }

        public void Configure(TrackPartEntity trackPart)
        {
            _trackPartPlacer.Place(trackPart);
            _wallObstacleSpawner.Spawn(trackPart);
            _collectableCubeSpawner.Spawn(trackPart);

            _trackPartSpawningSubscriber.SubscribeToSpawnTriggerEnter(trackPart, _spawnAction);
            _trackPartSpawningSubscriber.SubscribeToDespawnTriggerEnter(trackPart, _despawnAction);
            _trackPartPlayerDespawningSubscriber.SubscribeToDespawnPlayerCubes(trackPart);
            _trackPartPlayerDespawningSubscriber.SubscribeToDespawnPlayerCharacter(trackPart);
            _playerCubeDetachingSubscriber.SubscribeToDetachPlayerCube(trackPart.PlayerCubesDetachCollider);
        }

        public void Reset(TrackPartEntity trackPart)
        {
            _wallObstacleSpawner.Despawn(trackPart);
            _collectableCubeSpawner.Despawn(trackPart);
        }
    }
}