using System;
using R3;
using StickmanSliding.Data.Static.Configuration;
using StickmanSliding.Infrastructure.AssetLoading.Configuration;
using UnityEngine;
using Zenject;

namespace StickmanSliding.Features.Player
{
    public class PlayerMover : IPlayerMover
    {
        [Inject] private Rigidbody                     _rigidbody;
        [Inject] private IConfigProvider<PlayerConfig> _configProvider;

        private IDisposable _movingSubscription;

        public void StartMoving() => _movingSubscription =
            Observable.EveryUpdate(UnityFrameProvider.FixedUpdate).Subscribe(_ => Move()).AddTo(_rigidbody);

        public void StopMoving() => _movingSubscription.Dispose();

        private void Move() => _rigidbody.MovePosition(_rigidbody.position +
                                                       _configProvider.Config.Direction * _configProvider.Config.Speed *
                                                       Time.fixedDeltaTime);
    }
}