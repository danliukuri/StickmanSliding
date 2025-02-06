using UnityEngine;

namespace StickmanSliding.Data.Static.Constants
{
    public static class PlayerCharacterGameplayAnimator
    {
        public static class Parameters
        {
            public static int Jump       { get; } = Animator.StringToHash(nameof(Jump));
            public static int IsGrounded { get; } = Animator.StringToHash(nameof(IsGrounded));
        }
    }
}