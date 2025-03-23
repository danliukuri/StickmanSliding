using UnityEngine;
using Zenject;

namespace StickmanSliding.Features.Player.GameHub
{
    /// <inheritdoc cref="Entity"/>
    public class PlayerGameHubEntity : Entity
    {
        [Inject] public IPlayerCharacterRotator CharacterRotator { get; }

        [field: SerializeField] public Transform Character { get; private set; }
    }
}