using System;
using System.Collections.Generic;
using R3;
using R3.Triggers;
using UnityEngine;

namespace StickmanSliding.Features.Track
{
    public class TrackPartSpawningSubscriber : ITrackPartSpawningSubscriber
    {
        private readonly Dictionary<TrackPartEntity, IDisposable> _trackPartsSpawningSubscriptions   = new();
        private readonly Dictionary<TrackPartEntity, IDisposable> _trackPartsDespawningSubscriptions = new();

        public void Dispose()
        {
            foreach (IDisposable subscription in _trackPartsSpawningSubscriptions.Values)
                subscription.Dispose();
            _trackPartsSpawningSubscriptions.Clear();

            foreach (IDisposable subscription in _trackPartsDespawningSubscriptions.Values)
                subscription.Dispose();
            _trackPartsDespawningSubscriptions.Clear();
        }

        public void SubscribeToSpawnTriggerEnter(TrackPartEntity trackPart, Action<Collider> action)
        {
            if (!_trackPartsSpawningSubscriptions.ContainsKey(trackPart))
                _trackPartsSpawningSubscriptions.Add(trackPart,
                    trackPart.Triggers.SpawnNewTrackPart.OnTriggerEnterAsObservable().Subscribe(action));
        }

        public void SubscribeToDespawnTriggerEnter(TrackPartEntity trackPart, Action<Collider> action)
        {
            if (!_trackPartsDespawningSubscriptions.ContainsKey(trackPart))
                _trackPartsDespawningSubscriptions.Add(trackPart,
                    trackPart.Triggers.DestroyLastTrackPart.OnTriggerEnterAsObservable().Subscribe(action));
        }

        public void UnsubscribeToSpawnTriggerEnter(TrackPartEntity trackPart)
        {
            if (_trackPartsSpawningSubscriptions.Remove(trackPart, out IDisposable subscription))
                subscription.Dispose();
        }

        public void UnsubscribeToDespawnTriggerEnter(TrackPartEntity trackPart)
        {
            if (_trackPartsDespawningSubscriptions.Remove(trackPart, out IDisposable subscription))
                subscription.Dispose();
        }
    }
}