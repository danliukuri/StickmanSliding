using System;
using UnityEngine;

namespace StickmanSliding.Features.Track
{
    [Serializable]
    public class TrackPartTriggers
    {
        [field: SerializeField] public BoxCollider SpawnNewTrackPart    { get; private set; }
        [field: SerializeField] public BoxCollider DestroyLastTrackPart { get; private set; }
    }
}