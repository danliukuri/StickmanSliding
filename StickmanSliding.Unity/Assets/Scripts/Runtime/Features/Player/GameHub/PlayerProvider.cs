namespace StickmanSliding.Features.Player.GameHub
{
    public class PlayerProvider : IPlayerProvider
    {
        public PlayerGameHubEntity Player { get; private set; }

        public void Initialize(PlayerGameHubEntity player) => Player = player;
    }
}