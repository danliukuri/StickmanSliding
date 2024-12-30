using System.Collections.Generic;
using StickmanSliding.Features.CollectableCube;
using StickmanSliding.Features.ObstacleCube;
using UnityEngine;

namespace StickmanSliding.Data.Dynamic.State
{
    public class TrackPartState
    {
        public Dictionary<Vector3, CollectableCubeEntity> CollectableCubes { get; } = new();
        public Dictionary<Vector3, ObstacleCubeEntity>    ObstacleCubes    { get; } = new();
    }
}