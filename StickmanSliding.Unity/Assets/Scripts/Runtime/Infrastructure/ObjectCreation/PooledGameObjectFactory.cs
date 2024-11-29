using Cysharp.Threading.Tasks;
using StickmanSliding.Data.Static.Configuration;
using StickmanSliding.Infrastructure.AssetLoading.Configuration;
using UnityEngine;
using UnityEngine.Pool;
using Zenject;

namespace StickmanSliding.Infrastructure.ObjectCreation
{
    public class PooledGameObjectFactory<TComponent> : GameObjectFactory<TComponent> where TComponent : Component
    {
        [Inject]         private readonly IPoolConfigLoader               _poolConfigLoader;
        [InjectOptional] private readonly IGameObjectResetter<TComponent> _resetter;

        private ObjectPool<TComponent> _pool;

        public override async UniTask Initialize()
        {
            await base.Initialize();
            PoolConfig poolConfig = await _poolConfigLoader.Load<TComponent>();
            CreatePool(poolConfig);
            InitializePool(poolConfig);
            _poolConfigLoader.Release<TComponent>();
        }

        public override void Dispose()
        {
            base.Dispose();
            _pool.Clear();
        }

        public override TComponent Create() => _pool.Get();

        public override void Release(TComponent component) => _pool.Release(component);

        private void CreatePool(PoolConfig poolConfig) =>
            _pool = new ObjectPool<TComponent>(CreateActualObject, Configure, Reset, base.Release,
                poolConfig.ThrowErrorIfItemAlreadyInPoolWhenRelease, poolConfig.StartCapacity, poolConfig.MaxSize);

        private void InitializePool(PoolConfig poolConfig)
        {
            for (int i = default; i < poolConfig.StartCount; i++)
                _pool.Release(CreateActualObject());
        }

        private void Reset(TComponent component)
        {
            _resetter?.Reset(component);
            component.gameObject.SetActive(false);
        }
    }
}