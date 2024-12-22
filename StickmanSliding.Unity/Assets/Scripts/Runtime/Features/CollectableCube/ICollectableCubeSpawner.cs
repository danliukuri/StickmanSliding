using StickmanSliding.Features.Track;

namespace StickmanSliding.Features.CollectableCube
{
    public interface ICollectableCubeSpawner
    {
        void Spawn(TrackPart trackPart);

        void Despawn(TrackPart trackPart);
    }
}