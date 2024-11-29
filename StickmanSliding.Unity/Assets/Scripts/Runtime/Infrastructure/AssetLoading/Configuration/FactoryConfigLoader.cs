using System;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using StickmanSliding.Data.Static.Configuration.ObjectCreation;
using UnityEngine.AddressableAssets;
using Zenject;

namespace StickmanSliding.Infrastructure.AssetLoading.Configuration
{
    public class FactoryConfigLoader : IFactoryConfigLoader
    {
        [Inject] private readonly IAssetLoader                     _assetLoader;
        [Inject] private readonly Dictionary<Type, AssetReference> _factoryConfigReferences;

        private readonly Dictionary<Type, FactoryConfig> _factoryConfigs = new();

        public async UniTask<FactoryConfig> Load<TComponent>()
        {
            if (!_factoryConfigs.TryGetValue(typeof(TComponent), out FactoryConfig config))
            {
                config = await _assetLoader.Load<FactoryConfig>(_factoryConfigReferences[typeof(TComponent)]);
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