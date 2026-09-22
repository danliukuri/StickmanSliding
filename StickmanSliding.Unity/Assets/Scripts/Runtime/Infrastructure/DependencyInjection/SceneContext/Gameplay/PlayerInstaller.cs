using StickmanSliding.Data.Static.Configuration;
using StickmanSliding.Features.Player;
using StickmanSliding.Infrastructure.AssetLoading.Configuration;
using StickmanSliding.Infrastructure.ObjectCreation;
using UnityEngine;
using UnityEngine.AddressableAssets;
using Zenject;

namespace StickmanSliding.Infrastructure.DependencyInjection.SceneContext.Gameplay
{
    public class PlayerInstaller : MonoInstaller
    {
        [SerializeField] private AssetReferenceGameObject             playerPrefab;
        [SerializeField] private AssetReferenceT<PlayerConfig>        config;
        [SerializeField] private AssetReferenceT<TimeDependentConfig> timeDependentConfig;
        [SerializeField] private AssetReferenceT<RagdollConfig>       ragdollConfig;

        public override void InstallBindings()
        {
            BindFactory();
            BindConfigLoaders();
            BindProvider();
            BindTimeDependentConfigLoader();
            BindCollectedCubesMonitor();
        }

        private void BindFactory() =>
            Container.BindInterfacesTo<GameObjectFactory<PlayerEntity>>().AsSingle().WithArguments(playerPrefab);

        private void BindConfigLoaders()
        {
            Container.BindInterfacesTo<ConfigLoader<PlayerConfig>>().AsSingle().WithArguments(config);
            Container.BindInterfacesTo<ConfigLoader<RagdollConfig>>().AsSingle().WithArguments(ragdollConfig);
        }

        private void BindProvider() => Container.BindInterfacesTo<PlayerProvider>().AsSingle();

        private void BindTimeDependentConfigLoader() =>
            Container.BindInterfacesTo<ConfigLoader<TimeDependentConfig>>().AsSingle()
                .WithArguments(timeDependentConfig);

        private void BindCollectedCubesMonitor() =>
            Container.BindInterfacesTo<PlayerCollectedCubesMonitor>().AsSingle();
    }
}