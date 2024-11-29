using UnityEngine;

namespace StickmanSliding.Data.Static.Configuration
{
    [CreateAssetMenu(fileName = nameof(TrackPartSpawnerConfig), menuName = "Configuration/TrackPartSpawner")]
    public class TrackPartSpawnerConfig : ScriptableObject
    {
        [field: SerializeField] public int     TrackLength    { get; private set; }
        [field: SerializeField] public Vector3 TrackDirection { get; private set; }
        [field: SerializeField] public Vector3 SpawnOrigin    { get; private set; }
    }
}