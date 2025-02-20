using StickmanSliding.Data.Dynamic.State;
using UnityEngine;
using Zenject;

namespace StickmanSliding.Features.ObstacleCube
{
    /// <inheritdoc/>
    public class ObstacleCubeEntity : Entity
    {
        [Inject] public IPlayerCubeDetachingSubscriber PlayerCubeDetachingSubscriber { get; private set; }

        [field: SerializeField] public Renderer Renderer                  { get; private set; }
        [field: SerializeField] public Collider PlayerCubesDetachCollider { get; private set; }

        public TrackPlacementObjectState TrackPlacementState { get; } = new();
    }
}