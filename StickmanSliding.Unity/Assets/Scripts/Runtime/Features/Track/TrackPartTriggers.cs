using System;
using UnityEngine;

namespace StickmanSliding.Features.Track
{
    [Serializable]
    public class TrackPartTriggers
    {
        [field: SerializeField] public Collider SpawnNewTrackPart    { get; private set; }
        [field: SerializeField] public Collider DespawnLastTrackPart { get; private set; }
    }
}