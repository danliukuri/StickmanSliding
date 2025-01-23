using System.Collections.Generic;
using StickmanSliding.Features.ObstacleCube;
using StickmanSliding.Features.Track;

namespace StickmanSliding.Features.WallObstacle
{
    public interface IWallObstacleSpawner
    {
        List<ObstacleCubeEntity> Spawn(TrackPartEntity trackPart);

        void Despawn(TrackPartEntity trackPart);
    }
}