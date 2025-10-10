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
                    .Select(collision => (Entity: collision.transform.GetComponentInParent<PlayerEntity>(),
                        Cube: collision.transform.GetComponentInParent<CollectableCubeEntity>()))
                    .Where(player => player.Entity != default && player.Cube != default)
                    .Subscribe(trackPart,
                        (player, track) => _playerCubeDetacher.Detach(player.Entity, player.Cube, track)));
        }

        public void UnsubscribeToDetachPlayerCube(Collider collider)
        {
            if (_detachingSubscriptions.Remove(collider, out IDisposable subscription))
                subscription.Dispose();
        }
    }
}