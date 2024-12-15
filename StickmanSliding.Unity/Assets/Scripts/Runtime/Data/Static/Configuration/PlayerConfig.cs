using UnityEngine;

namespace StickmanSliding.Data.Static.Configuration
{
    [CreateAssetMenu(fileName = nameof(PlayerConfig), menuName = "Configuration/Player")]
    public class PlayerConfig : ScriptableObject
    {
        [field: SerializeField] public float ForwardSpeed { get; set; }
        [field: SerializeField] public float LateralSpeed { get; set; }
    }
}