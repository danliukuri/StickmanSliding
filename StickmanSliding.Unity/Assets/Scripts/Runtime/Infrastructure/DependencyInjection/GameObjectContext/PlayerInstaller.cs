using StickmanSliding.Features.Player;
using StickmanSliding.Features.Ragdoll;
using UnityEngine;
using Zenject;

namespace StickmanSliding.Infrastructure.DependencyInjection.GameObjectContext
{
    public class PlayerInstaller : MonoInstaller
    {
        [SerializeField] private PlayerEntity  player;
        [SerializeField] private RagdollEntity ragdoll;
        [SerializeField] private Animator      characterAnimator;
        [SerializeField] private Rigidbody     characterRigidbody;

        public override void InstallBindings()
        {
            BindMover();
            BindCubeSpawner();
            BindGroundedStateUpdater();
            BindCharacterAnimatorParametersChanger();
            BindRagdollServices();
        }

        private void BindCubeSpawner() =>
            Container.BindInterfacesTo<PlayerCubeSpawner>().AsSingle().WithArguments(player);

        private void BindMover() =>
            Container.BindInterfacesTo<PlayerMover>().AsSingle().WithArguments(transform);

        private void BindGroundedStateUpdater() =>
            Container.BindInterfacesTo<PlayerGroundedStateUpdater>().AsSingle()
                .WithArguments(characterRigidbody, player);

        private void BindCharacterAnimatorParametersChanger() =>
            Container.BindInterfacesTo<PlayerCharacterAnimatorParametersChanger>().AsSingle()
                .WithArguments(characterAnimator, player);

        private void BindRagdollServices()
        {
            Container.BindInterfacesTo<RagdollEnabler>().AsSingle().WithArguments(ragdoll);
            Container.BindInterfacesTo<RagdollThrower>().AsSingle().WithArguments(ragdoll);
        }
    }
}