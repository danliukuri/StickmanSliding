using StickmanSliding.Features.Player;
using UnityEngine;
using Zenject;

namespace StickmanSliding.Infrastructure.DependencyInjection.GameObjectContext
{
    public class PlayerInstaller : MonoInstaller
    {
        [SerializeField] private     PlayerEntity player;
        [SerializeField] private new Rigidbody    rigidbody;
        [SerializeField] private new Collider     collider;

        public override void InstallBindings()
        {
            BindMover();
            BindCubeSpawner();
        }

        private void BindCubeSpawner() => Container.BindInterfacesTo<PlayerCubeSpawner>().AsSingle()
            .WithArguments(player);

        private void BindMover() => Container.BindInterfacesTo<PlayerMover>().AsSingle()
            .WithArguments(transform, rigidbody);
    }
}