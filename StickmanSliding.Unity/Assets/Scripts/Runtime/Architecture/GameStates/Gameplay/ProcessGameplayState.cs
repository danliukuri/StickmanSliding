using StickmanSliding.Features.Player;
using StickmanSliding.Utilities.Patterns.State.Types;

namespace StickmanSliding.Architecture.GameStates.Gameplay
{
    public class ProcessGameplayState : IEnterableState<Player>
    {
        public void Enter(Player player) => player.Mover.StartMoving();
    }
}