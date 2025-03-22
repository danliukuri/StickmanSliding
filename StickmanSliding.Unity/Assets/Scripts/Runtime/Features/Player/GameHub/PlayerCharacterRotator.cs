using System;
using R3;
using StickmanSliding.Data.Static.Configuration;
using StickmanSliding.Infrastructure.AssetLoading.Configuration;
using StickmanSliding.Infrastructure.InputServices;
using UnityEngine;
using Zenject;

namespace StickmanSliding.Features.Player.GameHub
{
    public class PlayerCharacterRotator : IPlayerCharacterRotator
    {
        [Inject] private readonly IConfigProvider<PlayerGameHubConfig> _configProvider;
        [Inject] private readonly IRotateInputService                  _rotateInputService;

        [Inject] private readonly PlayerGameHubEntity _player;

        private IDisposable _rotatingSubscription;

        private float _currentRotation;
        private float _currentRotationChangingVelocity;

        public void StartRotating() => _rotatingSubscription = Observable.EveryUpdate(UnityFrameProvider.Update)
            .Select(_ => Time.deltaTime).Subscribe(Rotate).AddTo(_player);

        public void StopRotating() => _rotatingSubscription?.Dispose();

        private void Rotate(float deltaTime)
        {
            float targetRotation = _rotateInputService.GetRotation() * _configProvider.Config.TargetRotationSpeed;

            float smoothTime = Mathf.Abs(targetRotation) > Mathf.Abs(_currentRotation)
                ? _configProvider.Config.RotationSpeedAcceleratingTime
                : _configProvider.Config.RotationSpeedDeceleratingTime;

            _currentRotation =
                Mathf.SmoothDamp(_currentRotation, targetRotation, ref _currentRotationChangingVelocity, smoothTime);

            _player.Character.Rotate(xAngle: default, _currentRotation * deltaTime, zAngle: default);
        }
    }
}