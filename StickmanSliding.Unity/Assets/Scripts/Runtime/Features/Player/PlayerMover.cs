using System;
using R3;
using StickmanSliding.Data.Static.Configuration;
using StickmanSliding.Infrastructure.AssetLoading.Configuration;
using StickmanSliding.Infrastructure.InputServices;
using UnityEngine;
using Zenject;

namespace StickmanSliding.Features.Player
{
    public class PlayerMover : IPlayerMover
    {
        [Inject] private readonly IConfigProvider<PlayerConfig> _configProvider;
        [Inject] private readonly IMoveInputService             _moveInputService;

        [Inject] private readonly Transform _transform;
        [Inject] private readonly Rigidbody _rigidbody;

        private IDisposable _movingSubscription;

        public void StartMoving() => _movingSubscription =
            Observable.EveryUpdate(UnityFrameProvider.FixedUpdate).Subscribe(_ => Move()).AddTo(_rigidbody);

        public void StopMoving() => _movingSubscription.Dispose();

        private void Move()
        {
            Vector3 velocityChange = CalculateVelocityChange();

            if (velocityChange != Vector3.zero)
                _rigidbody.AddForce(velocityChange, ForceMode.VelocityChange);
        }

        private Vector3 CalculateVelocityChange()
        {
            Vector3 localCurrentVelocity = _transform.InverseTransformDirection(_rigidbody.linearVelocity);

            Vector3 localTargetVelocity = new(
                _moveInputService.GetMovement() * _configProvider.Config.LateralSpeed,
                localCurrentVelocity.y,
                _configProvider.Config.ForwardSpeed
            );

            Vector3 localVelocityChange = localTargetVelocity - localCurrentVelocity;

            return _transform.TransformDirection(localVelocityChange);
        }
    }
}