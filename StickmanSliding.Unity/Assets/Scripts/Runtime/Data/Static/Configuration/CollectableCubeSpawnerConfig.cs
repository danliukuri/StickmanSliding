using StickmanSliding.Utilities.Wrappers;
using UnityEngine;

namespace StickmanSliding.Data.Static.Configuration
{
    [CreateAssetMenu(fileName = nameof(CollectableCubeSpawnerConfig),
        menuName = "Configuration/Spawner/CollectableCubes")]
    public class CollectableCubeSpawnerConfig : ScriptableObject
    {
        [field: SerializeField] public Range<int> CubesRangeToSpawn { get; private set; }
    }
}