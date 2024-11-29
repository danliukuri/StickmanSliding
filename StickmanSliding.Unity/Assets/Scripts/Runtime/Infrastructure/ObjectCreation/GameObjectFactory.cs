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

        public GameObject Prefab { get; private set; }

        public async UniTask Initialize() => Prefab = await _assetLoader.Load<GameObject>(_prefabReference);

        public void Dispose() => _assetLoader.Release(_prefabReference);

        public TComponent Create()
        {
            TComponent component = CreateActualObject();
            Configure(component);
            return component;
        }

        public void Destroy(TComponent component)
        {
            if (component != default)
                Object.Destroy(component.gameObject);
        }

        private TComponent CreateActualObject()
        {
            if (Prefab != default)
                using (Prefab.AsInactive())
                    return _instantiator.InstantiatePrefabForComponent<TComponent>(Prefab,
                        _position, _rotation, _parent);

            throw new InvalidOperationException($"Prefab {Prefab.name} is not loaded!\n" +
                                                $"Make sure factory {GetType().Name} is initialized.");
        }

        private void Configure(TComponent component)
        {
            _configurator?.Configure(component);
            component.gameObject.SetActive(true);
        }
    }
}