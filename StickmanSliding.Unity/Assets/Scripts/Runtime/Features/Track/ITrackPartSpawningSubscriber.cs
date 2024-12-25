using System;
using UnityEngine;

namespace StickmanSliding.Features.Track
{
    public interface ITrackPartSpawningSubscriber : IDisposable
    {
        void SubscribeToSpawnTriggerEnter(TrackPartEntity trackPart, Action<Collider> action);

        void SubscribeToDespawnTriggerEnter(TrackPartEntity trackPart, Action<Collider> action);

        void UnsubscribeToSpawnTriggerEnter(TrackPartEntity trackPart);

        void UnsubscribeToDespawnTriggerEnter(TrackPartEntity trackPart);
    }
}