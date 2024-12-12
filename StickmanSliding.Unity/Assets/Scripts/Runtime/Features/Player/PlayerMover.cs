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
        [Inject] private readonly Rigidbody                     _rigidbody;
        [Inject] private readonly IConfigProvider<PlayerConfig> _configProvider;
        [Inject] private readonly IMoveInputService             _moveInputService;

        private IDisposable _movingSubscription;

        public void StartMoving() => _movingSubscription =
            Observable.EveryUpdate(UnityFrameProvider.FixedUpdate).Subscribe(_ => Move()).AddTo(_rigidbody);

        public void StopMoving() => _movingSubscription.Dispose();

        private void Move()
        {
            Vector3 positionDelta = CalculateFrontMovement() + CalculateSideMovement();

            _rigidbody.MovePosition(_rigidbody.position + positionDelta * Time.fixedDeltaTime);
        }

        private Vector3 CalculateFrontMovement() => _configProvider.Config.Speed * _configProvider.Config.Direction;

        private Vector3 CalculateSideMovement() => _moveInputService.IsMoving()
            ? _moveInputService.GetMovement()   *
              _configProvider.Config.SidesSpeed *
              _configProvider.Config.SidesDirection
            : Vector3.zero;
    }
}