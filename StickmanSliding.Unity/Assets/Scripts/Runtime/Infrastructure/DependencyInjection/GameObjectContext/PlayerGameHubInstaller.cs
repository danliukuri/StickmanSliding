using StickmanSliding.Features.Player.GameHub;
using UnityEngine;
using Zenject;

namespace StickmanSliding.Infrastructure.DependencyInjection.GameObjectContext
{
    public class PlayerGameHubInstaller : MonoInstaller
    {
        [SerializeField] private PlayerGameHubEntity player;

        public override void InstallBindings() => BindCharacterRotator();

        private void BindCharacterRotator() =>
            Container.BindInterfacesTo<PlayerCharacterRotator>().AsSingle().WithArguments(player);
    }
}