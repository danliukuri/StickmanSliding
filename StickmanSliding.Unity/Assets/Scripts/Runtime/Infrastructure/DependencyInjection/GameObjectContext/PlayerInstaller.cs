using StickmanSliding.Features.Player;
using UnityEngine;
using Zenject;

namespace StickmanSliding.Infrastructure.DependencyInjection.GameObjectContext
{
    public class PlayerInstaller : MonoInstaller
    {
        [SerializeField] private new Rigidbody rigidbody;

        public override void InstallBindings()
        {
            BindRigidbody();

            BindMover();
        }

        private void BindRigidbody() => Container.Bind<Rigidbody>().FromInstance(rigidbody).AsSingle();

        private void BindMover() => Container.BindInterfacesTo<PlayerMover>().AsSingle();
    }
}