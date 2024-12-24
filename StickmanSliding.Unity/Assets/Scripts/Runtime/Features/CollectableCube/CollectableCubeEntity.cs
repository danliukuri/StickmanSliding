using UnityEngine;

namespace StickmanSliding.Features.CollectableCube
{
    /// <summary>
    /// Representative of the entity which provides access to game object components, services, and state
    /// </summary>
    [SelectionBase]
    public class CollectableCubeEntity : MonoBehaviour
    {
        [field: SerializeField] public Collider Collider { get; private set; }
    }
}