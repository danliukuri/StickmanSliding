using UnityEngine;
using Zenject;
using static StickmanSliding.Data.Static.Constants.PlayerCharacterGameplayAnimatorController;

namespace StickmanSliding.Features.Player
{
    public class PlayerCharacterGameplayAnimationActivator : IPlayerCharacterGameplayAnimationActivator
    {
        [Inject] private readonly Animator _animator;

        public void Jump() => _animator.SetTrigger(AnimatorParameters.Jump);
    }
}