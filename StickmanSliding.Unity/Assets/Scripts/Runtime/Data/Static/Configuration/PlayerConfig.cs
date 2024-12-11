using UnityEngine;

namespace StickmanSliding.Data.Static.Configuration
{
    [CreateAssetMenu(fileName = nameof(PlayerConfig), menuName = "Configuration/Player")]
    public class PlayerConfig : ScriptableObject
    {
        [field: SerializeField] public float   Speed     { get; set; }
        [field: SerializeField] public Vector3 Direction { get; set; }
    }
}