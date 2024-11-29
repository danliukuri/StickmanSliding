using UnityEngine;

namespace StickmanSliding.Data.Dynamic
{
    public class TrackPartSpawnerState
    {
        public Vector3 CurrentSpawnPosition { get; set; }

        public TrackPartSpawnerState(Vector3 currentSpawnPosition) => CurrentSpawnPosition = currentSpawnPosition;
    }
}