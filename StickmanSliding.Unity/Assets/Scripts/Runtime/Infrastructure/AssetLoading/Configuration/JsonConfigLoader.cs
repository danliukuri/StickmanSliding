using Cysharp.Threading.Tasks;
using Newtonsoft.Json;
using UnityEngine;
using UnityEngine.AddressableAssets;
using Zenject;

namespace StickmanSliding.Infrastructure.AssetLoading.Configuration
{
    public class JsonConfigLoader<TConfig> : IConfigLoader<TConfig>, IConfigProvider<TConfig>
    {
        [Inject] private readonly IAssetLoader   _assetLoader;
        [Inject] private readonly AssetReference _configReferences;

        public TConfig Config { get; private set; }

        public async UniTask<TConfig> Load()
        {
            var jsonAsset = await _assetLoader.Load<TextAsset>(_configReferences);
            _assetLoader.Release(_configReferences);
            return Config ??= JsonConvert.DeserializeObject<TConfig>(jsonAsset.text);
        }

        public void Release() => Config = default;
    }
}