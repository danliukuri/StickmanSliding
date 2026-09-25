using UnityEngine;

namespace StickmanSliding.Data.Static.Configuration
{
    [CreateAssetMenu(fileName = nameof(PlayerCubeDetachingConfig), menuName = "Configuration/Player/CubeDetaching")]
    public class PlayerCubeDetachingConfig : ScriptableObject
    {
        [field: SerializeField] public float MaxStepHeight { get; private set; }
        [field: SerializeField] public float ThrowForce    { get; private set; }
    }
}
