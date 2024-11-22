using UnityEngine;

namespace StickmanSliding.Data.Static.Configuration.ObjectsCreation
{
    [CreateAssetMenu(fileName = nameof(FactoryConfig), menuName = "Configuration/Factory")]
    public class FactoryConfig : ScriptableObject
    {
        [field: SerializeField] public Component  Prefab { get; private set; }
        [field: SerializeField] public PoolConfig Pool   { get; private set; }
    }
}