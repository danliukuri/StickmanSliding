using StickmanSliding.Features.Player;
using UnityEngine;
using Zenject;

namespace StickmanSliding.Infrastructure.DependencyInjection.GameObjectContext
{
    public class PlayerInstaller : MonoInstaller
    {
        [SerializeField] private new Rigidbody rigidbody;

        public override void InstallBindings() => BindMover();

        private void BindMover() => Container.BindInterfacesTo<PlayerMover>().AsSingle()
            .WithArguments(transform, rigidbody);
    }
}