using UnityEngine;

namespace StickmanSliding.Data.Static.Configuration
{
    [CreateAssetMenu(fileName = nameof(PlayerGameHubConfig), menuName = "Configuration/Player/GameHub")]
    public class PlayerGameHubConfig : ScriptableObject
    {
        [field: SerializeField] public float TargetRotationSpeed { get; private set; }
        [field: SerializeField] public float MaxRotationSpeed    { get; private set; }
        [field: SerializeField] public float RotationSmoothTime  { get; private set; }
    }
}