using UnityEngine;
using Zenject;

namespace StickmanSliding.Features.Player
{
    /// <inheritdoc cref="Entity"/>
    public class PlayerEntity : Entity
    {
        [Inject] public IPlayerMover       Mover       { get; }
        [Inject] public IPlayerCubeSpawner CubeSpawner { get; }

        [field: SerializeField] public Transform Character   { get; private set; }
        [field: SerializeField] public Transform CubesParent { get; private set; }
    }
}