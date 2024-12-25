using System;
using UnityEngine;

namespace StickmanSliding.Features.Track
{
    public interface ITrackPartConfigurator : IDisposable
    {
        void Initialize(Action<Collider> spawnAction, Action<Collider> despawnAction);
    }
}