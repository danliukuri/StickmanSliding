﻿using StickmanSliding.Data.Dynamic.State;
using UnityEngine;

namespace StickmanSliding.Features.Track
{
    /// <summary>
    /// Representative of the entity which provides access to game object components, services, and state
    /// </summary>
    [SelectionBase]
    public class TrackPartEntity : MonoBehaviour, ITrackPart
    {
        [field: SerializeField] public Transform         Body     { get; private set; }
        [field: SerializeField] public TrackPartTriggers Triggers { get; private set; }

        public Transform Transform => transform;

        public TrackPartState State { get; } = new();
    }
}