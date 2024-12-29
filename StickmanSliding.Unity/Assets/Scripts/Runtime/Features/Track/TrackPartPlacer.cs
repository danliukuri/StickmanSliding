using StickmanSliding.Utilities.Extensions;
using UnityEngine;

namespace StickmanSliding.Features.Track
{
    public class TrackPartPlacer : ITrackPartPlacer
    {
        public Vector3 SpawnPosition { get; private set; }

        public void Initialize(Vector3 spawnOrigin) => SpawnPosition = spawnOrigin;

        public void Place(ITrackPart trackPart)
        {
            SpawnPosition                += trackPart.Body.HalfLengthVector();
            trackPart.Transform.position =  SpawnPosition;
            SpawnPosition                += trackPart.Body.HalfLengthVector();
        }
    }
}