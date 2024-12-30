using StickmanSliding.Features.ObstacleCube;
using UnityEngine;
using Zenject;

namespace StickmanSliding.Infrastructure.DependencyInjection.GameObjectContext
{
    public class ObstacleCubeInstaller : MonoInstaller
    {
        [SerializeField] private new Collider collider;

        public override void InstallBindings() => BindPlayerCubeDetachingSubscriber();

        private void BindPlayerCubeDetachingSubscriber() =>
            Container.BindInterfacesTo<PlayerCubeDetachingSubscriber>().AsSingle().WithArguments(collider);
    }
}