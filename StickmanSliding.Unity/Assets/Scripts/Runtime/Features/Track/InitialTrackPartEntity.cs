using UnityEngine;

namespace StickmanSliding.Features.Track
{
    /// <summary>
    /// Representative of the entity which provides access to game object components, services, and state
    /// </summary>
    [SelectionBase]
    public class InitialTrackPartEntity : MonoBehaviour, ITrackPart
    {
        [field: SerializeField] public Transform Body { get; private set; }

        public Transform Transform => transform;
    }
}