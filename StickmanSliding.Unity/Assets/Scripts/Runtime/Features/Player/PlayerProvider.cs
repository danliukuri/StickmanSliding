namespace StickmanSliding.Features.Player
{
    public class PlayerProvider : IPlayerProvider
    {
        public PlayerEntity Player { get; private set; }

        public void Initialize(PlayerEntity player) => Player = player;
    }
}