using UnityEngine;
using Zenject;

namespace StickmanSliding.Features.Player
{
    [SelectionBase]
    public class Player : MonoBehaviour
    {
        [Inject] public IPlayerMover Mover { get; }
    }
}