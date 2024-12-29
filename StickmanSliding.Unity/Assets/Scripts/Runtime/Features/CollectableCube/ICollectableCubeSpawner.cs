using StickmanSliding.Features.Track;

namespace StickmanSliding.Features.CollectableCube
{
    public interface ICollectableCubeSpawner
    {
        void Spawn(TrackPartEntity trackPart);

        void Despawn(TrackPartEntity trackPart);

        void Despawn(CollectableCubeEntity cube);
    }
}