using StickmanSliding.Data.Static.Configuration;
using StickmanSliding.Features.Player.GameHub;
using StickmanSliding.Infrastructure.AssetLoading.Configuration;
using StickmanSliding.Infrastructure.ObjectCreation;
using UnityEngine;
using UnityEngine.AddressableAssets;
using Zenject;

namespace StickmanSliding.Infrastructure.DependencyInjection.SceneContext.GameHub
{
    public class PlayerInstaller : MonoInstaller
    {
        [SerializeField] private AssetReferenceGameObject             playerPrefab;
        [SerializeField] private AssetReferenceT<PlayerGameHubConfig> config;
        [SerializeField] private Transform                            playerSpawnPoint;

        public override void InstallBindings()
        {
            BindFactory();
            BindConfigLoader();
            BindProvider();
        }

        private void BindFactory() =>
            Container.BindInterfacesTo<GameObjectFactory<PlayerGameHubEntity>>().AsSingle()
                .WithArguments(playerPrefab, playerSpawnPoint.position, playerSpawnPoint.rotation, playerSpawnPoint);

        private void BindConfigLoader() =>
            Container.BindInterfacesTo<ConfigLoader<PlayerGameHubConfig>>().AsSingle().WithArguments(config);

        private void BindProvider() => Container.BindInterfacesTo<PlayerProvider>().AsSingle();
    }
}