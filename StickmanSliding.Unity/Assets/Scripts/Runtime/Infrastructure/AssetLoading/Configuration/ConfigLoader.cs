using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.AddressableAssets;
using Zenject;

namespace StickmanSliding.Infrastructure.AssetLoading.Configuration
{
    public class ConfigLoader<TConfig> : IConfigLoader<TConfig>, IConfigProvider<TConfig>
        where TConfig : ScriptableObject
    {
        [Inject] private readonly IAssetLoader   _assetLoader;
        [Inject] private readonly AssetReference _configReferences;

        public TConfig Config { get; private set; }

        public async UniTask<TConfig> Load() => Config ??= await _assetLoader.Load<TConfig>(_configReferences);

        public void Release()
        {
            Config = default;
            _assetLoader.Release(_configReferences);
        }
    }
}