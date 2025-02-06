using System;
using R3;
using StickmanSliding.Data.Static.Configuration;
using StickmanSliding.Infrastructure.AssetLoading.Configuration;
using UnityEngine;
using Zenject;

namespace StickmanSliding.Features.Player
{
    public class PlayerGroundedStateUpdater : IPlayerGroundedStateUpdater
    {
        private const int MinNumberOfGroundColliders = 0, MaxNumberOfGroundColliders = 1;

        [Inject] private readonly IConfigProvider<PlayerConfig> _configProvider;
        [Inject] private readonly Rigidbody                     _rigidbody;
        [Inject] private readonly PlayerEntity                  _player;

        private readonly Collider[] _groundColliders = new Collider[MaxNumberOfGroundColliders];

        private IDisposable _updatingSubscription;

        public void StartUpdating() => _updatingSubscription =
            Observable.EveryUpdate(UnityFrameProvider.FixedUpdate).Subscribe(SetGroundedState).AddTo(_rigidbody);

        public void StopUpdating() => _updatingSubscription.Dispose();

        private void SetGroundedState(Unit _) =>
            _player.State.IsCharacterGrounded.Value = GetGroundColliders() > MinNumberOfGroundColliders;

        private int GetGroundColliders()
        {
            Vector3 groundCheckSpherePosition = _rigidbody.position;
            groundCheckSpherePosition.y += _configProvider.Config.GroundCheckSphereHeightDisplacement;

            return Physics.OverlapSphereNonAlloc(groundCheckSpherePosition,
                _configProvider.Config.RadiusOfGroundCheckSphere, _groundColliders, _configProvider.Config.GroundLayer);
        }
    }
}