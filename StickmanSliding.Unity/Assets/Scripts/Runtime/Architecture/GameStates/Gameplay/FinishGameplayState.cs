using StickmanSliding.Utilities.Patterns.State.Types;
using UnityEngine;

namespace StickmanSliding.Architecture.GameStates.Gameplay
{
    public class FinishGameplayState : IEnterableState
    {
        public void Enter() => Debug.Log($"{nameof(FinishGameplayState)}.{nameof(Enter)}");
    }
}