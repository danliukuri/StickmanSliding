using StickmanSliding.Features.Player;
using StickmanSliding.Infrastructure.ObjectCreation;
using UnityEngine;
using UnityEngine.AddressableAssets;
using Zenject;

namespace StickmanSliding.Infrastructure.DependencyInjection.SceneContext.Gameplay
{
    public class PlayerInstaller : MonoInstaller
    {
        [SerializeField] private AssetReferenceGameObject playerPrefab;

        public override void InstallBindings() => BindPlayerFactory();

        private void BindPlayerFactory() =>
            Container.BindInterfacesTo<GameObjectFactory<Player>>().AsSingle().WithArguments(playerPrefab);
    }
}