using StickmanSliding.Features.WallObstacle;
using StickmanSliding.Infrastructure.ObjectCreation;
using UnityEngine;
using UnityEngine.AddressableAssets;
using Zenject;

namespace StickmanSliding.Infrastructure.DependencyInjection.SceneContext.Gameplay
{
    public class WallObstacleInstaller : MonoInstaller
    {
        [SerializeField] private AssetReferenceGameObject obstacleCubePrefab;
        [SerializeField] private Transform                wallObstaclesParent;

        public override void InstallBindings()
        {
            BindObstacleCubeFactory();
            BindWallObstacleSpawner();
        }

        private void BindObstacleCubeFactory() =>
            Container.BindInterfacesTo<PooledGameObjectFactory<ObstacleCubeEntity>>().AsSingle()
                .WithArguments(obstacleCubePrefab, wallObstaclesParent);

        private void BindWallObstacleSpawner() =>
            Container.BindInterfacesTo<WallObstacleSpawner>().AsSingle();
    }
}