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
        [Inject] public IPlayerMover       Mover       { get; }
        [Inject] public IPlayerCubeSpawner CubeSpawner { get; }

        [field: SerializeField] public Transform Character   { get; private set; }
        [field: SerializeField] public Transform CubesParent { get; private set; }
    }
}