using System;
using UnityEngine;

namespace StickmanSliding.Data.Static.Configuration.ObjectsCreation
{
    [Serializable]
    public class PoolConfig
    {
        [field: SerializeField, Min(default)] public int StartCount    { get; private set; }
        [field: SerializeField, Min(default)] public int StartCapacity { get; private set; }
        [field: SerializeField, Min(default)] public int MaxSize       { get; private set; }

        [field: SerializeField] public bool ThrowErrorIfItemAlreadyInPoolWhenRelease { get; private set; }
    }
}