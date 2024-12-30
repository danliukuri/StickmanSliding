namespace StickmanSliding.Features.ObstacleCube
{
    public interface IPlayerCubeDetachingSubscriber
    {
        void SubscribeToDetachPlayerCube();
        void UnsubscribeToDetachPlayerCube();
    }
}