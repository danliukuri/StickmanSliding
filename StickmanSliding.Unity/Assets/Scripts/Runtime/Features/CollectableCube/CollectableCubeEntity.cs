using StickmanSliding.Data.Dynamic.State;
using UnityEngine;
using Zenject;

namespace StickmanSliding.Features.CollectableCube
{
    /// <inheritdoc/>
    public class CollectableCubeEntity : Entity
    {
        [Inject] public ICubeCollectingSubscriber CollectingSubscriber { get; private set; }

        [field: SerializeField] public Rigidbody Rigidbody      { get; private set; }
        [field: SerializeField] public Collider  Collider       { get; private set; }
        [field: SerializeField] public Collider  CollectTrigger { get; private set; }

        public TrackPlacementObjectState TrackPlacementState { get; } = new();
    }
}