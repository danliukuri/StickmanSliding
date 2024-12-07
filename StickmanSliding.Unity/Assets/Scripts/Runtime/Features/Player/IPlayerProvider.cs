namespace StickmanSliding.Features.Player
{
    public interface IPlayerProvider
    {
        Player Player { get; }

        void Initialize(Player player);
    }
}