using StickmanSliding.Features.Player;
using StickmanSliding.Utilities.Patterns.State.Types;
using Zenject;

namespace StickmanSliding.Architecture.GameStates.Gameplay
{
    public class ProcessGameplayState : IEnterableState, IExitableState
    {
        [Inject] private IPlayerProvider _playerProvider;

        public void Enter() => _playerProvider.Player.Mover.StartMoving();
        public void Exit()  => _playerProvider.Player.Mover.StopMoving();
    }
}