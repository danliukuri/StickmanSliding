namespace StickmanSliding.Features.Player.GameHub
{
    public interface IPlayerProvider
    {
        PlayerGameHubEntity Player { get; }

        void Initialize(PlayerGameHubEntity player);
    }
}