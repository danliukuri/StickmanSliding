using UnityEngine;

namespace StickmanSliding.Data.Static.Configuration
{
    [CreateAssetMenu(fileName = nameof(PlayerConfig), menuName = "Configuration/Player/Main")]
    public class PlayerConfig : ScriptableObject
    {
        [field: SerializeField] public float ForwardSpeed      { get; private set; }
        [field: SerializeField] public float LateralSpeed      { get; private set; }
        [field: SerializeField] public int   InitialCubesCount { get; private set; }
    }
}