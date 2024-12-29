using StickmanSliding.Features.Track;

namespace StickmanSliding.Features.WallObstacle
{
    public interface IWallObstacleSpawner
    {
        ObstacleCubeEntity Spawn(TrackPartEntity trackPart);

        void Despawn(TrackPartEntity trackPart);
    }
}