using UnityEngine;

namespace StickmanSliding.Data.Static.Configuration
{
    [CreateAssetMenu(fileName = nameof(PlayerCubeDetachingConfig), menuName = "Configuration/Player/CubeDetaching")]
    public class PlayerCubeDetachingConfig : ScriptableObject
    {
        [field: SerializeField] public float   MaxDetachAngle         { get; private set; }
        [field: SerializeField] public Vector3 NotDetachableDirection { get; private set; }
    }
}