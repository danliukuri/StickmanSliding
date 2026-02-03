using UnityEngine;

namespace StickmanSliding.Features.Player
{
    public interface IPlayerDirectionProvider
    {
        Vector3 Direction { get; }
    }
}