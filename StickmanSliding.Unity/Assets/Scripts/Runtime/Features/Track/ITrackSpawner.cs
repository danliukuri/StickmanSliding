using System;

namespace StickmanSliding.Features.Track
{
    public interface ITrackSpawner : IDisposable
    {
        void Initialize();
        void Spawn();
        void Despawn();
    }
}