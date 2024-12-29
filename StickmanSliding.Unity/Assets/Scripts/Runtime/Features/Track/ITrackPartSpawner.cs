using System;

namespace StickmanSliding.Features.Track
{
    public interface ITrackPartSpawner : IDisposable
    {
        void Initialize();

        InitialTrackPartEntity SpawnInitial();
        TrackPartEntity        Spawn();

        void Despawn(ITrackPart trackPart);
        void DespawnLast();
    }
}