namespace StickmanSliding.Features.CollectableCube
{
    public interface ICubeCollectingSubscriber
    {
        void SubscribeToCollectByPlayer();
        void UnsubscribeToCollectByPlayer();
    }
}