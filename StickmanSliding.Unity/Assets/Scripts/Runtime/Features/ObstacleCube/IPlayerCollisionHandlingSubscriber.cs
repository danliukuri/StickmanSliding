using StickmanSliding.Features.Track;
using UnityEngine;

namespace StickmanSliding.Features.ObstacleCube
{
    public interface IPlayerCollisionHandlingSubscriber
    {
        void Subscribe(Collider collider, TrackPartEntity trackPart = default);

        void Unsubscribe(Collider collider);
    }
}