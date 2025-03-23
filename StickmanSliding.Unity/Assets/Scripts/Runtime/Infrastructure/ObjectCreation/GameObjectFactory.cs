using System;
using Cysharp.Threading.Tasks;
using StickmanSliding.Infrastructure.AssetLoading;
using StickmanSliding.Utilities.Extensions.Wrappers;
using UnityEngine;
using UnityEngine.AddressableAssets;
using Zenject;
using Object = UnityEngine.Object;

namespace StickmanSliding.Infrastructure.ObjectCreation
{
    public class GameObjectFactory<TComponent> : IGameObjectFactory<TComponent> where TComponent : Component
    {
        [Inject]         private readonly IAssetLoader                        _assetLoader;
        [Inject]         private readonly IInstantiator                       _instantiator;
        [InjectOptional] private readonly IGameObjectConfigurator<TComponent> _configurator;
        [InjectOptional] private readonly IGameObjectResetter<TComponent>     _resetter;

        [Inject]         private readonly AssetReferenceGameObject _prefabReference;
        [InjectOptional] private readonly Vector3                  _position;
        [InjectOptional] private readonly Quaternion               _rotation = Quaternion.identity;
        [InjectOptional] private readonly Transform                _parent;

        private GameObject _prefab;

        public TComponent Prefab { get; private set; }

        public virtual async UniTask Initialize()
        {
            _prefab = await _assetLoader.Load<GameObject>(_prefabReference);
            Prefab  = _prefab.GetComponent<TComponent>();
        }

        public virtual void Dispose() => _assetLoader.Release(_prefabReference);

        public virtual TComponent Create()
        {
            TComponent component = CreateActualObject();
            Configure(component);
            return component;
        }

        public virtual void Release(TComponent component)
        {
            if (component != default)
                Object.Destroy(component.gameObject);
        }

        protected TComponent CreateActualObject()
        {
            if (_prefab == default)
                throw new InvalidOperationException($"Prefab for {typeof(TComponent).Name} is not loaded!\n" +
                                                    $"Make sure factory {GetType().Name} is initialized.");
            using (_prefab.AsInactive())
                return _instantiator.InstantiatePrefabForComponent<TComponent>(_prefab, _position, _rotation, _parent);
        }

        protected void Configure(TComponent component)
        {
            _configurator?.Configure(component);
            component.gameObject.SetActive(true);
        }
    }
}