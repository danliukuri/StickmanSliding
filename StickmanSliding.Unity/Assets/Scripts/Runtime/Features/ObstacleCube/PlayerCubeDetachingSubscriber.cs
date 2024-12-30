using System;
using R3;
using R3.Triggers;
using StickmanSliding.Features.CollectableCube;
using StickmanSliding.Features.Player;
using UnityEngine;
using Zenject;

namespace StickmanSliding.Features.ObstacleCube
{
    public class PlayerCubeDetachingSubscriber : IPlayerCubeDetachingSubscriber
    {
        [Inject] private readonly Collider _obstacleCubeCollider;

        private IDisposable _detachingSubscription;

        public void SubscribeToDetachPlayerCube() =>
            _detachingSubscription = _obstacleCubeCollider.OnCollisionEnterAsObservable()
                .Select(collision => collision.transform)
                .Where(transform => transform.GetComponentInParent<PlayerEntity>() != default)
                .Select(transform => transform.GetComponentInParent<CollectableCubeEntity>())
                .Where(playerCube => playerCube != default)
                .Subscribe(Detach);

        public void UnsubscribeToDetachPlayerCube() => _detachingSubscription.Dispose();

        private void Detach(CollectableCubeEntity playerCube) => playerCube.transform.SetParent(default);
    }
}