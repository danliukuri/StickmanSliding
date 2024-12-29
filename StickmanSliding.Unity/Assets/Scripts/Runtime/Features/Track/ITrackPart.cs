using UnityEngine;

namespace StickmanSliding.Features.Track
{
    public interface ITrackPart
    {
        Transform Transform { get; }
        Transform Body      { get; }
    }
}