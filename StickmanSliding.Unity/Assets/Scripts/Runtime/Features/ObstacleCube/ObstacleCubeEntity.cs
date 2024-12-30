using StickmanSliding.Data.Dynamic.State;
using Zenject;

namespace StickmanSliding.Features.ObstacleCube
{
    /// <inheritdoc/>
    public class ObstacleCubeEntity : Entity
    {
        [Inject] public IPlayerCubeDetachingSubscriber PlayerCubeDetachingSubscriber { get; private set; }

        public TrackPlacementObjectState TrackPlacementState { get; } = new();
    }
}