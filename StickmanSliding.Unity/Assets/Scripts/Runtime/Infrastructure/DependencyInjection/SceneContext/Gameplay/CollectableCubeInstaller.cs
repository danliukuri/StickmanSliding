using StickmanSliding.Data.Static.Configuration;
using StickmanSliding.Features.CollectableCube;
using StickmanSliding.Infrastructure.AssetLoading.Configuration;
using StickmanSliding.Infrastructure.ObjectCreation;
using UnityEngine;
using UnityEngine.AddressableAssets;
using Zenject;

namespace StickmanSliding.Infrastructure.DependencyInjection.SceneContext.Gameplay
{
    public class CollectableCubeInstaller : MonoInstaller
    {
        [SerializeField] private AssetReferenceT<CollectableCubeSpawnerConfig> collectableCubeSpawnerConfig;
        [SerializeField] private AssetReferenceGameObject                      collectableCubePrefab;
        [SerializeField] private Transform                                     collectableCubeParent;

        public override void InstallBindings()
        {
            BindCollectableCubeFactory();
            BindCollectableCubeSpawnerConfigLoader();
            BindCollectableCubeSpawner();
        }

        private void BindCollectableCubeFactory() =>
            Container.BindInterfacesTo<PooledGameObjectFactory<CollectableCube>>().AsSingle()
                .WithArguments(collectableCubePrefab, collectableCubeParent);

        private void BindCollectableCubeSpawnerConfigLoader() =>
            Container.BindInterfacesTo<ConfigLoader<CollectableCubeSpawnerConfig>>().AsSingle()
                .WithArguments(collectableCubeSpawnerConfig);

        private void BindCollectableCubeSpawner() =>
            Container.BindInterfacesTo<CollectableCubeSpawner>().AsSingle();
    }
}