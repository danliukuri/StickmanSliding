using UnityEngine;
using UnityEngine.AddressableAssets;

namespace StickmanSliding.Data.Static.Configuration.ObjectCreation
{
    [CreateAssetMenu(fileName = nameof(FactoryConfig), menuName = "Configuration/Factory")]
    public class FactoryConfig : ScriptableObject
    {
        [field: SerializeField] public AssetReference Prefab { get; private set; }
        [field: SerializeField] public PoolConfig     Pool   { get; private set; }
    }
}