using StickmanSliding.Data.Static.Configuration;
using StickmanSliding.Features.ObstacleCube;
using StickmanSliding.Features.WallObstacle;
using StickmanSliding.Infrastructure.AssetLoading.Configuration;
using StickmanSliding.Infrastructure.ObjectCreation;
using UnityEngine;
using UnityEngine.AddressableAssets;
using Zenject;

namespace StickmanSliding.Infrastructure.DependencyInjection.SceneContext.Gameplay
{
    public class WallObstacleInstaller : MonoInstaller
    {
        [SerializeField] private AssetReferenceGameObject                   obstacleCubePrefab;
        [SerializeField] private Transform                                  wallObstaclesParent;
        [SerializeField] private AssetReferenceT<PlayerCubeDetachingConfig> playerCubeDetachingConfig;
        [SerializeField] private AssetReferenceT<TextAsset>                 wallObstacleSpawnerConfig;
        [SerializeField] private AssetReferenceT<CubeObstacleConfig>        cubeObstacleConfig;

        public override void InstallBindings()
        {
            BindObstacleCubeFactory();
            BindWallObstacleSpawner();
            BindPlayerCubeDetachingConfigLoader();
            BindPlayerCubeDetacher();
            BindPlayerCubeDetachingSubscriber();
            BindWallObstacleSpawnerConfigLoader();
            BindCubeObstacleConfigLoader();
        }

        private void BindObstacleCubeFactory() =>
            Container.BindInterfacesTo<PooledGameObjectFactory<ObstacleCubeEntity>>().AsSingle()
                .WithArguments(obstacleCubePrefab, wallObstaclesParent);

        private void BindWallObstacleSpawner() =>
            Container.BindInterfacesTo<WallObstacleSpawner>().AsSingle();

        private void BindPlayerCubeDetachingConfigLoader() =>
            Container.BindInterfacesTo<ConfigLoader<PlayerCubeDetachingConfig>>().AsSingle()
                .WithArguments(playerCubeDetachingConfig);

        private void BindPlayerCubeDetacher() =>
            Container.BindInterfacesTo<PlayerCubeDetacher>().AsSingle();

        private void BindPlayerCubeDetachingSubscriber() =>
            Container.BindInterfacesTo<PlayerCubeDetachingSubscriber>().AsSingle();

        private void BindWallObstacleSpawnerConfigLoader() =>
            Container.BindInterfacesTo<JsonConfigLoader<WallObstacleSpawnerConfig>>().AsSingle()
                .WithArguments(wallObstacleSpawnerConfig);

        private void BindCubeObstacleConfigLoader() =>
            Container.BindInterfacesTo<ConfigLoader<CubeObstacleConfig>>().AsSingle()
                .WithArguments(cubeObstacleConfig);
    }
}