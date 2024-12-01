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
        [SerializeField] private AssetReferenceGameObject      playerPrefab;
        [SerializeField] private AssetReferenceT<PlayerConfig> config;


        public override void InstallBindings()
        {
            BindFactory();
            BindConfigLoader();
        }

        private void BindFactory() =>
            Container.BindInterfacesTo<GameObjectFactory<Player>>().AsSingle().WithArguments(playerPrefab);


        private void BindConfigLoader() =>
            Container.BindInterfacesTo<ConfigLoader<PlayerConfig>>().AsSingle().WithArguments(config);
    }
}