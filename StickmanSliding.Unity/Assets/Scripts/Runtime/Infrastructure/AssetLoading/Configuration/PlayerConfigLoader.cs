using Cysharp.Threading.Tasks;
using StickmanSliding.Data.Static.Configuration;
using UnityEngine.AddressableAssets;
using Zenject;

namespace StickmanSliding.Infrastructure.AssetLoading.Configuration
{
    public class PlayerConfigLoader : IConfigLoader<PlayerConfig>, IConfigProvider<PlayerConfig>
    {
        [Inject] private readonly IAssetLoader   _assetLoader;
        [Inject] private readonly AssetReference _configReferences;

        public PlayerConfig Config { get; private set; }

        public async UniTask<PlayerConfig> Load() =>
            Config ??= await _assetLoader.Load<PlayerConfig>(_configReferences);

        public void Release()
        {
            Config = default;
            _assetLoader.Release(_configReferences);
        }
    }
}