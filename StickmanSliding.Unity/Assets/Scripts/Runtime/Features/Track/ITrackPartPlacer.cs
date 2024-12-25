using UnityEngine;

namespace StickmanSliding.Features.Track
{
    public interface ITrackPartPlacer
    {
        Vector3 SpawnPosition { get; }

        void Initialize(Vector3 spawnOrigin);

        void Place(ITrackPart trackPart);
    }
}