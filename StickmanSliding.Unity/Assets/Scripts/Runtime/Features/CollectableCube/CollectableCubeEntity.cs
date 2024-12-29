using StickmanSliding.Data.Dynamic.State;
using UnityEngine;
using Zenject;

namespace StickmanSliding.Features.CollectableCube
{
    /// <summary>
    /// Representative of the entity which provides access to game object components, services, and state
    /// </summary>
    [SelectionBase]
    public class CollectableCubeEntity : MonoBehaviour
    {
        [Inject] public ICubeCollectingSubscriber CollectingSubscriber { get; private set; }

        [field: SerializeField] public Rigidbody Rigidbody { get; private set; }
        [field: SerializeField] public Collider  Collider  { get; private set; }

        public CollectableCubeState State { get; } = new();
    }
}