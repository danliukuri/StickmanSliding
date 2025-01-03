using System;
using System.Collections.Generic;
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
        [Inject] private readonly IPlayerCubeDetacher             _playerCubeDetacher;

        private readonly Dictionary<Collider, IDisposable> _detachingSubscriptions = new();

        public void SubscribeToDetachPlayerCube(Collider collider, TrackPartEntity trackPart = default)
        {
            if (!_detachingSubscriptions.ContainsKey(collider))
                _detachingSubscriptions.Add(collider, collider.OnCollisionEnterAsObservable()
                    .Where(_playerCubeDetacher.IsCollisionFromDetachableDirection)
                    .Select(collision => collision.transform)
                    .Where(transform => transform.GetComponentInParent<PlayerEntity>() != default)
                    .Select(transform => transform.GetComponentInParent<CollectableCubeEntity>())
                    .Where(playerCube => playerCube != default)
                    .Subscribe(trackPart, _playerCubeDetacher.Detach));
        }

        public void UnsubscribeToDetachPlayerCube(Collider collider)
        {
            if (_detachingSubscriptions.Remove(collider, out IDisposable subscription))
                subscription.Dispose();
        }
    }
}