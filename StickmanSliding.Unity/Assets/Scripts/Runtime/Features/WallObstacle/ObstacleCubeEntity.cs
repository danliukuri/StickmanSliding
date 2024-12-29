using StickmanSliding.Data.Dynamic.State;

namespace StickmanSliding.Features.WallObstacle
{
    /// <inheritdoc/>
    public class ObstacleCubeEntity : Entity
    {
        public TrackPlacementObjectState TrackPlacementState { get; } = new();
    }
}