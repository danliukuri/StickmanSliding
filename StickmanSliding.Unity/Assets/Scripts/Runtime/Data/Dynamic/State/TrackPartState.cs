using System.Collections.Generic;
using StickmanSliding.Features.CollectableCube;
using UnityEngine;

namespace StickmanSliding.Data.Dynamic.State
{
    public class TrackPartState
    {
        public Dictionary<Vector3, CollectableCube> CollectableCubes { get; } = new();
    }
}