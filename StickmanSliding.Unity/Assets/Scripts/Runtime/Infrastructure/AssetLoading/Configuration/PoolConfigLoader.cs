using System;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using StickmanSliding.Data.Static.Configuration;
using UnityEngine.AddressableAssets;
using Zenject;

namespace StickmanSliding.Infrastructure.AssetLoading.Configuration
{
    public class PoolConfigLoader : IPoolConfigLoader
    {
        [Inject] private readonly IAssetLoader                     _assetLoader;
        [Inject] private readonly Dictionary<Type, AssetReference> _factoryConfigReferences;

        private readonly Dictionary<Type, PoolConfig> _factoryConfigs = new();

        public async UniTask<PoolConfig> Load<TComponent>()
        {
            if (!_factoryConfigs.TryGetValue(typeof(TComponent), out PoolConfig config))
            {
                config = await _assetLoader.Load<PoolConfig>(_factoryConfigReferences[typeof(TComponent)]);
                _factoryConfigs.Add(typeof(TComponent), config);
            }

            return config;
        }

        public void Release<TComponent>()
        {
            if (_factoryConfigReferences.Remove(typeof(TComponent), out AssetReference assetReference))
                _assetLoader.Release(assetReference);
        }
    }
}