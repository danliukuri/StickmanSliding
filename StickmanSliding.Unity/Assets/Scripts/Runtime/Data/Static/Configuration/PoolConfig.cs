using UnityEngine;

namespace StickmanSliding.Data.Static.Configuration
{
    [CreateAssetMenu(fileName = nameof(PoolConfig), menuName = "Configuration/Pool")]
    public class PoolConfig : ScriptableObject
    {
        [field: SerializeField, Min(default)] public int StartCount    { get; private set; }
        [field: SerializeField, Min(default)] public int StartCapacity { get; private set; }
        [field: SerializeField, Min(default)] public int MaxSize       { get; private set; }

        [field: SerializeField] public bool ThrowErrorIfItemAlreadyInPoolWhenRelease { get; private set; }
    }
}