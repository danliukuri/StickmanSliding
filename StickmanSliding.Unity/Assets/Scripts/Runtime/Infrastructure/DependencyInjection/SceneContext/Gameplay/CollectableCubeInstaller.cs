using StickmanSliding.Features.CollectableCube;
using StickmanSliding.Infrastructure.ObjectCreation;
using UnityEngine;
using UnityEngine.AddressableAssets;
using Zenject;

namespace StickmanSliding.Infrastructure.DependencyInjection.SceneContext.Gameplay
{
    public class CollectableCubeInstaller : MonoInstaller
    {
        [SerializeField] private AssetReferenceGameObject prefab;
        [SerializeField] private Transform                defaultParent;

        public override void InstallBindings()
        {
            BindFactory();
            BindResetter();
            BindSpawner();
            BindParentProvider();
        }

        private void BindFactory() =>
            Container.BindInterfacesTo<PooledGameObjectFactory<CollectableCubeEntity>>().AsSingle()
                .WithArguments(prefab, defaultParent);

        private void BindResetter() =>
            Container.BindInterfacesTo<CollectableCubeResetter>().AsSingle();

        private void BindSpawner() =>
            Container.BindInterfacesTo<CollectableCubeSpawner>().AsSingle();

        private void BindParentProvider() =>
            Container.BindInterfacesTo<CollectableCubesParentProvider>().AsSingle()
                .WithArguments(defaultParent);
    }
}