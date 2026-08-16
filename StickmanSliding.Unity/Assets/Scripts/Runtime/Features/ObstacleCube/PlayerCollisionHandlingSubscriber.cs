using System;
using System.Collections.Generic;
using R3;
using R3.Triggers;
using StickmanSliding.Features.Player;
using StickmanSliding.Features.Track;
using UnityEngine;
using Zenject;

namespace StickmanSliding.Features.ObstacleCube
{
    public class PlayerCollisionHandlingSubscriber : IPlayerCollisionHandlingSubscriber
    {
        [Inject] private readonly IPlayerCollisionHandler _playerCollisionHandler;
        [Inject] private readonly IPlayerCubeDetacher     _playerCubeDetacher;

        private readonly Dictionary<Collider, IDisposable> _collisionSubscriptions = new();

        public void Subscribe(Collider collider, TrackPartEntity trackPart = default)
        {
            if (!_collisionSubscriptions.ContainsKey(collider))
                _collisionSubscriptions.Add(collider, collider.OnCollisionEnterAsObservable()
                    .Select(collision =>
                        (Entity: collision.transform.GetComponentInParent<PlayerEntity>(), Collision: collision))
                    .Where(player => player.Entity != default)
                    .Subscribe(trackPart, _playerCollisionHandler.HandleEnter));
        }

        public void Unsubscribe(Collider collider)
        {
            if (_collisionSubscriptions.Remove(collider, out IDisposable subscription))
                subscription.Dispose();
        }
    }
}