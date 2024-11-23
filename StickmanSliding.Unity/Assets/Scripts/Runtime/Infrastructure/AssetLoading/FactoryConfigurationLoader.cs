using System;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using StickmanSliding.Data.Static.Configuration.ObjectCreation;
using UnityEngine.AddressableAssets;
using Zenject;

namespace StickmanSliding.Infrastructure.AssetLoading
{
    public class FactoryConfigurationLoader : IFactoryConfigurationLoader
    {
        [Inject] private readonly IAssetLoader                     _assetLoader;
        [Inject] private readonly Dictionary<Type, AssetReference> _factoryConfigReferences;

        private Dictionary<Type, FactoryConfig> _factoryConfigs;

        public async UniTask<FactoryConfig> Load<TComponent>()
        {
            if (!_factoryConfigs.TryGetValue(typeof(TComponent), out FactoryConfig config))
            {
                config = await _assetLoader.Load<FactoryConfig>(_factoryConfigReferences[typeof(TComponent)]);
                _factoryConfigs.Add(typeof(TComponent), config);
            }

            return config;
        }

        public void Release<TComponent>() =>
            _assetLoader.Release(_factoryConfigReferences[typeof(TComponent)]);
    }
}