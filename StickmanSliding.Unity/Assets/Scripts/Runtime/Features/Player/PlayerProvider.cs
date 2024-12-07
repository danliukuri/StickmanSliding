namespace StickmanSliding.Features.Player
{
    public class PlayerProvider : IPlayerProvider
    {
        public Player Player { get; private set; }

        public void Initialize(Player player) => Player = player;
    }
}