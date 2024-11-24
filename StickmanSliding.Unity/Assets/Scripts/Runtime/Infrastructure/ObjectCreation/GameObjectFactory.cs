using System;
using Cysharp.Threading.Tasks;
using StickmanSliding.Data.Static.Configuration.ObjectCreation;
using StickmanSliding.Infrastructure.AssetLoading;
using StickmanSliding.Utilities.Extensions.Wrappers;
using UnityEngine;
using UnityEngine.Pool;
using Zenject;
using Object = UnityEngine.Object;

namespace StickmanSliding.Infrastructure.ObjectCreation
{
    public class GameObjectFactory<TComponent> : IGameObjectFactory<TComponent> where TComponent : Component
    {
        [Inject]         private readonly IFactoryConfigurationLoader         _factoryConfigurationLoader;
        [Inject]         private readonly IAssetLoader                        _assetLoader;
        [Inject]         private readonly IInstantiator                       _instantiator;
        [InjectOptional] private readonly IGameObjectConfigurator<TComponent> _configurator;
        [InjectOptional] private readonly IGameObjectResetter<TComponent>     _resetter;

        private ObjectPool<TComponent> _pool;
        private TComponent             _prefab;

        public void Dispose() => _pool.Clear();

        public async UniTask Initialize()
        {
            FactoryConfig factoryConfig = await _factoryConfigurationLoader.Load<TComponent>();
            _prefab = await _assetLoader.Load<TComponent>(factoryConfig.Prefab);
            CreatePool(factoryConfig.Pool);
            InitializePool(factoryConfig.Pool);
            _factoryConfigurationLoader.Release<TComponent>();
        }

        public TComponent Create() => _pool.Get();

        public void Release(TComponent component) => _pool.Release(component);

        private void CreatePool(PoolConfig poolConfig) =>
            _pool = new ObjectPool<TComponent>(CreateActualObject, Configure, Reset, Destroy,
                poolConfig.ThrowErrorIfItemAlreadyInPoolWhenRelease, poolConfig.StartCapacity, poolConfig.MaxSize);

        private void InitializePool(PoolConfig poolConfig)
        {
            for (int i = default; i < poolConfig.StartCount; i++)
                _pool.Release(CreateActualObject());
        }

        private TComponent CreateActualObject()
        {
            if (_prefab != default)
                using (_prefab.AsInactive())
                    return _instantiator.InstantiatePrefabForComponent<TComponent>(_prefab);

            throw new InvalidOperationException(
                $"Prefab {_prefab.name} is not loaded!\n" +
                $"Make sure factory {typeof(GameObjectFactory<TComponent>).Name} is initialized.");
        }

        private void Configure(TComponent component)
        {
            _configurator?.Configure(component);
            component.gameObject.SetActive(true);
        }

        private void Reset(TComponent component)
        {
            _resetter?.Reset(component);
            component.gameObject.SetActive(false);
        }

        private void Destroy(TComponent component) => Object.Destroy(component.gameObject);
    }
}