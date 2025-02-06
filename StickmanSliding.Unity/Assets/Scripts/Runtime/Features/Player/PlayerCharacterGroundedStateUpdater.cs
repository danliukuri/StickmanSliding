using System;
using R3;
using UnityEngine;
using Zenject;

namespace StickmanSliding.Features.Player
{
    public class PlayerGroundedStateUpdater : IPlayerGroundedStateUpdater
    {
        private const int   NumberOfPlayerCharacterColliders = 1;
        private const int   MaxNumberOfGroundColliders       = NumberOfPlayerCharacterColliders + 1;
        private const float RadiusOfGroundCheckSphere        = 0.4f;

        [Inject] private readonly Rigidbody    _rigidbody;
        [Inject] private readonly PlayerEntity _player;

        private readonly Collider[] _groundColliders = new Collider[MaxNumberOfGroundColliders];

        private IDisposable _updatingSubscription;

        public void StartUpdating() => _updatingSubscription =
            Observable.EveryUpdate(UnityFrameProvider.FixedUpdate).Subscribe(SetGroundedState).AddTo(_rigidbody);

        public void StopUpdating() => _updatingSubscription.Dispose();

        private void SetGroundedState(Unit _) =>
            _player.State.IsCharacterGrounded.Value = GetGroundColliders() > NumberOfPlayerCharacterColliders;

        private int GetGroundColliders() =>
            Physics.OverlapSphereNonAlloc(_rigidbody.position, RadiusOfGroundCheckSphere, _groundColliders);
    }
}