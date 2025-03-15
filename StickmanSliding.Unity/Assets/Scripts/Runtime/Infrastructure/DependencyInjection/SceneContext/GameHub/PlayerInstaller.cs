using StickmanSliding.Features.Player.GameHub;
using StickmanSliding.Infrastructure.ObjectCreation;
using UnityEngine;
using UnityEngine.AddressableAssets;
using Zenject;

namespace StickmanSliding.Infrastructure.DependencyInjection.SceneContext.GameHub
{
    public class PlayerInstaller : MonoInstaller
    {
        [SerializeField] private AssetReferenceGameObject playerPrefab;
        [SerializeField] private Transform                playerSpawnPoint;

        public override void InstallBindings() => BindFactory();

        private void BindFactory() =>
            Container.BindInterfacesTo<GameObjectFactory<PlayerGameHubEntity>>().AsSingle()
                .WithArguments(playerPrefab, playerSpawnPoint.position, playerSpawnPoint.rotation, playerSpawnPoint);
    }
}