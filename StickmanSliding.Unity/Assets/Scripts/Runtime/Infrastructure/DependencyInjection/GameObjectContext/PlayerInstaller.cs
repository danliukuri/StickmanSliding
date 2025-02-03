using StickmanSliding.Features.Player;
using UnityEngine;
using Zenject;

namespace StickmanSliding.Infrastructure.DependencyInjection.GameObjectContext
{
    public class PlayerInstaller : MonoInstaller
    {
        [SerializeField] private     PlayerEntity player;
        [SerializeField] private new Collider     collider;
        [SerializeField] private     Animator     characterGameplayAnimator;

        public override void InstallBindings()
        {
            BindMover();
            BindCubeSpawner();
            BindCharacterGameplayAnimationActivator();
        }

        private void BindCubeSpawner() => Container.BindInterfacesTo<PlayerCubeSpawner>().AsSingle()
            .WithArguments(player);

        private void BindMover() => Container.BindInterfacesTo<PlayerMover>().AsSingle().WithArguments(transform);

        private void BindCharacterGameplayAnimationActivator() =>
            Container.BindInterfacesTo<PlayerCharacterGameplayAnimationActivator>().AsSingle()
                .WithArguments(characterGameplayAnimator);
    }
}