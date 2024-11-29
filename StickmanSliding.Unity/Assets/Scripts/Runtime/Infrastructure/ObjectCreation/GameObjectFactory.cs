using System;
using Cysharp.Threading.Tasks;
using StickmanSliding.Data.Static.Configuration.ObjectCreation;
using StickmanSliding.Infrastructure.AssetLoading;
using StickmanSliding.Infrastructure.AssetLoading.Configuration;
using StickmanSliding.Utilities.Extensions.Wrappers;
using UnityEngine;
using UnityEngine.Pool;
using Zenject;
using Object = UnityEngine.Object;

namespace StickmanSliding.Infrastructure.ObjectCreation
{
    public class GameObjectFactory<TComponent> : IGameObjectFactory<TComponent> where TComponent : Component
    {
        [Inject]         private readonly IFactoryConfigLoader                _factoryConfigLoader;
        [Inject]         private readonly IAssetLoader                        _assetLoader;
        [Inject]         private readonly IInstantiator                       _instantiator;
        [InjectOptional] private readonly IGameObjectConfigurator<TComponent> _configurator;
        [InjectOptional] private readonly IGameObjectResetter<TComponent>     _resetter;

        private ObjectPool<TComponent> _pool;

        public GameObject Prefab { get; private set; }

        public async UniTask Initialize()
        {
            FactoryConfig factoryConfig = await _factoryConfigLoader.Load<TComponent>();
            Prefab = await _assetLoader.Load<GameObject>(factoryConfig.Prefab);
            CreatePool(factoryConfig.Pool);
            InitializePool(factoryConfig.Pool);
            _factoryConfigLoader.Release<TComponent>();
        }

        public void Dispose() => _pool.Clear();

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
            if (Prefab != default)
                using (Prefab.AsInactive())
                    return _instantiator.InstantiatePrefabForComponent<TComponent>(Prefab);

            throw new InvalidOperationException(
                $"Prefab {Prefab.name} is not loaded!\n" +
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

        private void Destroy(TComponent component)
        {
            if (component != default)
                Object.Destroy(component.gameObject);
        }
    }
}