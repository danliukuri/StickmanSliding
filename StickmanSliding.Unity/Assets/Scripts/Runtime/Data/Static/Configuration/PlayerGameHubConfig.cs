using UnityEngine;

namespace StickmanSliding.Data.Static.Configuration
{
    [CreateAssetMenu(fileName = nameof(PlayerGameHubConfig), menuName = "Configuration/Player/GameHub")]
    public class PlayerGameHubConfig : ScriptableObject
    {
        [field: SerializeField] public float TargetRotationSpeed           { get; private set; }
        [field: SerializeField] public float RotationSpeedAcceleratingTime { get; private set; }
        [field: SerializeField] public float RotationSpeedDeceleratingTime { get; private set; }
    }
}