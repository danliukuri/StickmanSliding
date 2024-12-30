using System;
using R3;
using R3.Triggers;
using StickmanSliding.Features.CollectableCube;
using StickmanSliding.Features.Player;
using StickmanSliding.Features.Track;
using UnityEngine;
using Zenject;

namespace StickmanSliding.Features.ObstacleCube
{
    public class PlayerCubeDetachingSubscriber : IPlayerCubeDetachingSubscriber
    {
        [Inject] private readonly ICollectableCubesParentProvider _collectableCubesParentProvider;

        [Inject] private readonly ObstacleCubeEntity _obstacleCube;
        [Inject] private readonly Collider           _obstacleCubeCollider;

        private IDisposable _detachingSubscription;

        public void SubscribeToDetachPlayerCube() =>
            _detachingSubscription = _obstacleCubeCollider.OnCollisionEnterAsObservable()
                .Select(collision => collision.transform)
                .Where(transform => transform.GetComponentInParent<PlayerEntity>() != default)
                .Select(transform => transform.GetComponentInParent<CollectableCubeEntity>())
                .Where(playerCube => playerCube != default)
                .Subscribe(Detach);

        public void UnsubscribeToDetachPlayerCube() => _detachingSubscription.Dispose();

        private void Detach(CollectableCubeEntity playerCube)
        {
            playerCube.transform.SetParent(_collectableCubesParentProvider.DefaultParent);

            TrackPartEntity currentTrackPart = _obstacleCube.TrackPlacementState.OriginTrackPart;
            currentTrackPart.State.CollectableCubes.Add(playerCube.transform.position, playerCube);
        }
    }
}