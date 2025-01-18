using StickmanSliding.Features.Track;
using UnityEngine;

namespace StickmanSliding.Features.ObstacleCube
{
    public interface IPlayerCubeDetachingSubscriber
    {
        void SubscribeToDetachPlayerCube(Collider collider, TrackPartEntity trackPart = default);

        void UnsubscribeToDetachPlayerCube(Collider collider);
    }
}