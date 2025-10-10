namespace StickmanSliding.Features.Player
{
    public interface IPlayerCollectedCubesMonitor
    {
        void SubscribeToMonitor();
        void UnsubscribeToMonitor();
    }
}