using StickmanSliding.Features.Player;
using StickmanSliding.UI.Features.Mediation;
using StickmanSliding.Utilities.Patterns.State.Types;
using Zenject;

namespace StickmanSliding.Architecture.GameStates.Gameplay
{
    public class FinishGameplayState : IEnterableState
    {
        [Inject] private readonly IPlayerProvider _playerProvider;
        [Inject] private readonly IMediator       _uiMediator;

        public void Enter()
        {
            PlayerEntity player = _playerProvider.Player;

            if (player != default)
            {
                player.State.IsAlive.Value = false;
                player.Ragdoll.Thrower.ThrowInPlayerDirection();
            }

            _uiMediator.Notify(nameof(FinishGameplayState), nameof(Enter));
        }
    }
}