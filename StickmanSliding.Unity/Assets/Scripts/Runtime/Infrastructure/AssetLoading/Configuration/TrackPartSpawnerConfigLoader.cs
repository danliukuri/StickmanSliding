using Cysharp.Threading.Tasks;
using StickmanSliding.Data.Static.Configuration;
using UnityEngine.AddressableAssets;
using Zenject;

namespace StickmanSliding.Infrastructure.AssetLoading.Configuration
{
    public class TrackPartSpawnerConfigLoader : IConfigLoader<TrackPartSpawnerConfig>
    {
        [Inject] private readonly IAssetLoader   _assetLoader;
        [Inject] private readonly AssetReference _configReferences;

        private TrackPartSpawnerConfig _trackPartSpawnerConfig;

        public async UniTask<TrackPartSpawnerConfig> Load() =>
            _trackPartSpawnerConfig ??= await _assetLoader.Load<TrackPartSpawnerConfig>(_configReferences);

        public void Release()
        {
            _trackPartSpawnerConfig = default;
            _assetLoader.Release(_configReferences);
        }
    }
}