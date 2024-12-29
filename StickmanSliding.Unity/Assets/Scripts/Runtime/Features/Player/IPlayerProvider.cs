namespace StickmanSliding.Features.Player
{
    public interface IPlayerProvider
    {
        PlayerEntity Player { get; }

        void Initialize(PlayerEntity player);
    }
}