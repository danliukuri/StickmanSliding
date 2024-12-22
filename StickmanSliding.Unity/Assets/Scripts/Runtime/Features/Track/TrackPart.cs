using StickmanSliding.Data.Dynamic.State;
using UnityEngine;

namespace StickmanSliding.Features.Track
{
    [SelectionBase]
    public class TrackPart : MonoBehaviour
    {
        [field: SerializeField] public Transform         Body     { get; private set; }
        [field: SerializeField] public TrackPartTriggers Triggers { get; private set; }

        public TrackPartState State { get; } = new();
    }
}