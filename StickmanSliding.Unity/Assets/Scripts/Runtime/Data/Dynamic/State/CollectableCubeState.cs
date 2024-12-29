using StickmanSliding.Features.Track;
using UnityEngine;

namespace StickmanSliding.Data.Dynamic.State
{
    public class CollectableCubeState
    {
        public Vector3         OriginLocalPosition { get; set; }
        public TrackPartEntity OriginTrackPart     { get; set; }
    }
}