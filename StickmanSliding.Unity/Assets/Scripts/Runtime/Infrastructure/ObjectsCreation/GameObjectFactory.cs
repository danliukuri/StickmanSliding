using StickmanSliding.Data.Static.Configuration.ObjectsCreation;
using StickmanSliding.Infrastructure.AssetManagement;
using StickmanSliding.Utilities.Extensions.Wrappers;
using UnityEngine;
using UnityEngine.Pool;
using Zenject;

namespace StickmanSliding.Infrastructure.ObjectsCreation
{
    public class GameObjectFactory<TComponent> : IGameObjectFactory<TComponent> where TComponent : Component
    {
        [Inject]         private readonly IConfigurationProvider              _configurationProvider;
        [Inject]         private readonly IInstantiator                       _instantiator;
        [InjectOptional] private readonly IGameObjectConfigurator<TComponent> _configurator;
        [InjectOptional] private readonly IGameObjectResetter<TComponent>     _resetter;

        private ObjectPool<TComponent> _pool;

        public void Initialize()
        {
            CreatePool();
            InitializePool();
        }

        public void Dispose() => _pool.Clear();

        public TComponent Create()                      => _pool.Get();
        public void       Release(TComponent component) => _pool.Release(component);

        private void CreatePool()
        {
            PoolConfig poolConfig = _configurationProvider.GetFactoryConfig<TComponent>().Pool;
            _pool = new ObjectPool<TComponent>(CreateActualObject, Configure, Reset, Destroy,
                poolConfig.ThrowErrorIfItemAlreadyInPoolWhenRelease, poolConfig.StartCapacity, poolConfig.MaxSize);
        }

        private void InitializePool()
        {
            for (int i = default; i < _configurationProvider.GetFactoryConfig<TComponent>().Pool.StartCount; i++)
                _pool.Release(CreateActualObject());
        }

        private TComponent CreateActualObject()
        {
            Component prefab = _configurationProvider.GetFactoryConfig<TComponent>().Prefab;

            using (prefab.AsInactive())
                return _instantiator.InstantiatePrefabForComponent<TComponent>(prefab);
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