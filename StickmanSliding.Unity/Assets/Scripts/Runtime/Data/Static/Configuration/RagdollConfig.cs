using UnityEngine;

namespace StickmanSliding.Data.Static.Configuration
{
    [CreateAssetMenu(fileName = nameof(RagdollConfig), menuName = "Configuration/Player/Ragdoll")]
    public class RagdollConfig : ScriptableObject
    {
        [field: SerializeField] public float ThrowForce { get; private set; }
    }
}