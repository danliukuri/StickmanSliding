using StickmanSliding.Data.Dynamic.State;
using UnityEngine;

namespace StickmanSliding.Features.Track
{
    /// <inheritdoc cref="Entity"/>
    public class TrackPartEntity : Entity, ITrackPart
    {
        [field: SerializeField] public Transform         Body                      { get; private set; }
        [field: SerializeField] public TrackPartTriggers Triggers                  { get; private set; }
        [field: SerializeField] public Collider          PlayerDespawnTrigger      { get; private set; }
        [field: SerializeField] public Collider          PlayerCubesDetachCollider { get; private set; }

        public Transform Transform => transform;

        public TrackPartState State { get; } = new();
    }
}