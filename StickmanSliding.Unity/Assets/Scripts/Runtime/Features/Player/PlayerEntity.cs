using UnityEngine;
using Zenject;

namespace StickmanSliding.Features.Player
{
    /// <summary>
    /// Representative of the entity which provides access to game object components, services, and state
    /// </summary>
    [SelectionBase]
    public class PlayerEntity : MonoBehaviour
    {
        [Inject] public IPlayerMover Mover { get; }
    }
}