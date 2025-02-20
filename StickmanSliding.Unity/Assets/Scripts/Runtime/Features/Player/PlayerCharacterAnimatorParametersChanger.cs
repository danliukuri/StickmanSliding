using System;
using R3;
using UnityEngine;
using Zenject;
using static StickmanSliding.Data.Static.Constants.PlayerCharacterGameplayAnimator;

namespace StickmanSliding.Features.Player
{
    public class PlayerCharacterAnimatorParametersChanger : IPlayerCharacterAnimatorParametersChanger
    {
        [Inject] private readonly Animator     _animator;
        [Inject] private readonly PlayerEntity _player;

        private IDisposable _groundedStateUpdatingSubscription;

        public void SetJumpTrigger() => _animator.SetTrigger(Parameters.Jump);

        public void StartUpdatingGroundedState() =>
            _groundedStateUpdatingSubscription = _player.State.IsCharacterGrounded.Subscribe(SetGroundedState);

        public void StopUpdatingGroundedState() => _groundedStateUpdatingSubscription.Dispose();

        private void SetGroundedState(bool value) => _animator.SetBool(Parameters.IsGrounded, value);
    }
}