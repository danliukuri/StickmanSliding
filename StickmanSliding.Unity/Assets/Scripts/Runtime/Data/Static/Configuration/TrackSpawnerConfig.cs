using UnityEngine;

namespace StickmanSliding.Data.Static.Configuration
{
    [CreateAssetMenu(fileName = nameof(TrackSpawnerConfig), menuName = "Configuration/TrackSpawner")]
    public class TrackSpawnerConfig : ScriptableObject
    {
        [field: SerializeField] public int     Length      { get; private set; }
        [field: SerializeField] public Vector3 Direction   { get; private set; }
        [field: SerializeField] public Vector3 SpawnOrigin { get; private set; }
    }
}