using UnityEngine;

namespace StickmanSliding.Features.Track
{
    /// <inheritdoc cref="Entity"/>
    public class InitialTrackPartEntity : Entity, ITrackPart
    {
        [field: SerializeField] public Transform Body { get; private set; }

        public Transform Transform => transform;
    }
}