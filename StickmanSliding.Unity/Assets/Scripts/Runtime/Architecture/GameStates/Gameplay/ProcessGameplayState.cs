using StickmanSliding.Features.Player;
using StickmanSliding.Utilities.Patterns.State.Types;

namespace StickmanSliding.Architecture.GameStates.Gameplay
{
    public class ProcessGameplayState : IEnterableState<Player>, IExitableState
    {
        private Player _player;

        public void Enter(Player player) => (_player = player).Mover.StartMoving();

        public void Exit() => _player.Mover.StopMoving();
    }
}